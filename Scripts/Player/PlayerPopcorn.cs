using Cysharp.Threading.Tasks;
using System;
using System.Collections;
using System.Linq;
using System.Threading;
using UnityEngine;
/// <summary>
/// ポップコーンの処理
/// </summary>
public class PlayerPopcorn : MonoBehaviour
{
    private GameObject popcornPrefab;
    [SerializeField] private Transform spawnPoint;
    [SerializeField] private WordPopcorn[] wordPopcorns;
    //---飛ばす力---
    private float minForce = 5f;
    private float maxForce = 8f;
    //---発射間隔---
    private float shotInterval = 1f;
    private float spreadAmount = 0.2f;
    private float destroyInterval = 5f;
    //---放った回数---
    public int ShotNum => shotNum;
    private int shotNum = 0;
    private CancellationTokenSource abilityCts;

    private void OnEnable()
    {
        shotNum = 0;
        if (abilityCts != null)
        {
            abilityCts.Cancel();
            abilityCts.Dispose();
        }
        abilityCts = new CancellationTokenSource();
        AutoShotPopcornAsync(abilityCts.Token).Forget();
    }
    public void SetParameter(PlayerData _data)
    {
        popcornPrefab = _data.popcornPrefab;
        minForce = _data.minPower;
        maxForce = _data.maxPower;
        shotInterval = _data.shotInterval;
        spreadAmount = _data.spreadAmount;
        destroyInterval = _data.destroyInterval;
    }
    /// <summary>
    /// ポップコーンを投げる準備
    /// </summary>
    /// <param name="_token"></param>
    /// <returns></returns>
    public async UniTask AutoShotPopcornAsync(CancellationToken _token)
    {
        while (true)
        {
            await UniTask.WaitUntil(() =>wordPopcorns.Any(w => w != null && w.IsPopcornTrigger), cancellationToken: _token);

            PopcornShot();

            await UniTask.Delay(TimeSpan.FromSeconds(shotInterval), cancellationToken: _token);
        }
    }

    /// <summary>
    /// ポップコーンを放つ
    /// </summary>
    private void PopcornShot()
    {
        SoundManager.Instance.PlaySE(SESource.POPCORN);

        shotNum++;

        GameObject _popcorn = Instantiate(popcornPrefab, spawnPoint.position, Quaternion.identity);
        Rigidbody2D _popcornRb = _popcorn.GetComponent<Rigidbody2D>();

        Vector3 _randomSpread = UnityEngine.Random.insideUnitSphere * spreadAmount;
        Vector3 _launchDirection = (transform.up + _randomSpread).normalized;

        if (_launchDirection.y < 0) _launchDirection.y *= -1;

        float _launchForce = UnityEngine.Random.Range(minForce, maxForce);

        _popcornRb.AddForce(_launchDirection * _launchForce, ForceMode2D.Impulse);

        Destroy(_popcorn, destroyInterval);
    }
    private void OnDisable()
    {
        if (abilityCts != null)
        {
            abilityCts.Cancel();
            abilityCts.Dispose();
            abilityCts = null;
        }
    }
}
