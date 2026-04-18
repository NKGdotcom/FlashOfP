using Cysharp.Threading.Tasks;
using System;
using System.Collections;
using System.Linq;
using System.Threading;
using UnityEngine;
/// <summary>
/// プレイヤーの「ポップコーン」アクションを管理するクラス
/// </summary>
public class PlayerPopcorn : MonoBehaviour
{
    [Header("発射ギミック設定")]
    [Tooltip("発射のトリガーとなる単語（Word）の配列")]
    [SerializeField] private WordPopcorn[] wordPopcorns;
    [Tooltip("ポップコーンを発射する位置")]
    [SerializeField] private Transform spawnPoint;
    

    //ポップコーン発射のパラメータ
    private GameObject popcornPrefab;
    private float minForce = 5f;
    private float maxForce = 8f;
    private float shotInterval = 1f;
    private float spreadAmount = 0.2f;
    private float destroyInterval = 5f;
    public int ShotNum { get; private set; }
    
    private CancellationTokenSource abilityCts;

    private void Awake()
    {
        if(spawnPoint == null) { Debug.LogError("spawnPointが参照されていません"); return; }
        if(wordPopcorns == null) { Debug.LogError("wordPopcornsが参照されていません"); return; }
    }

    private void OnEnable()
    {
        ShotNum = 0;
        //オブジェクトが有効になったとき、古いトークンがあれば破棄
        if (abilityCts != null)
        {
            abilityCts.Cancel();
            abilityCts.Dispose();
        }
        abilityCts = new CancellationTokenSource();
        //非同期の自動ポップコーン発射ループを開始
        AutoShotPopcornAsync(abilityCts.Token).Forget();
    }

    private void OnDisable()
    {
        //オブジェクトが無効化された時、実行中の非同期ループを強制終了
        if (abilityCts != null)
        {
            abilityCts.Cancel();
            abilityCts.Dispose();
            abilityCts = null;
        }
    }

    /// <summary>
    /// PlayerDataからパラメータをセット
    /// </summary>
    /// <param name="_data"></param>
    public void SetParameter(PlayerData _data)
    {
        popcornPrefab = _data.PopcornPrefab;
        minForce = _data.MinPower;
        maxForce = _data.MaxPower;
        shotInterval = _data.ShotInterval;
        spreadAmount = _data.SpreadAmount;
        destroyInterval = _data.DestroyInterval;
    }

    /// <summary>
    /// ポップコーンの効果をリセットする
    /// </summary>
    public void ResetPopcorn()
    {
        foreach (var _word in wordPopcorns)
        {
            if (_word != null)
            {
                _word.ResetWord();
            }
        }
    }

    /// <summary>
    /// ポップコーンを一定間隔で発射し続ける
    /// </summary>
    /// <param name="_token"></param>
    /// <returns></returns>
    public async UniTask AutoShotPopcornAsync(CancellationToken _token)
    {
        while (true)
        {
            //wordPopcornのトリガーがONになるまで待機
            await UniTask.WaitUntil(() =>wordPopcorns.Any(w => w != null && w.IsPopcornTrigger), cancellationToken: _token);
            //ポップコーンの発射
            PopcornShot();
            //次の発射までインターバル待機
            await UniTask.Delay(TimeSpan.FromSeconds(shotInterval), cancellationToken: _token);
        }
    }

    /// <summary>
    /// ポップコーンを生成し、力を加える
    /// </summary>
    private void PopcornShot()
    {
        if (popcornPrefab == null) return;

        SoundManager.Instance.PlaySE(SESource.POPCORN);
        ShotNum++;

        GameObject _popcorn = Instantiate(popcornPrefab, spawnPoint.position, Quaternion.identity);

        if (_popcorn.TryGetComponent<Rigidbody2D>(out var _popcornRb))
        {
            //飛ばす方向を決める
            Vector3 _randomSpread = UnityEngine.Random.insideUnitSphere * spreadAmount;
            Vector3 _launchDirection = (transform.up + _randomSpread).normalized;

            //常に上方向へ飛ばす
            if (_launchDirection.y < 0) _launchDirection.y *= -1;

            //ランダムの力で弾を発射
            float _launchForce = UnityEngine.Random.Range(minForce, maxForce);
            _popcornRb.AddForce(_launchDirection * _launchForce, ForceMode2D.Impulse);
        }

        Destroy(_popcorn, destroyInterval);
    }
}
