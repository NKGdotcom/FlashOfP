using System.Linq;
using UnityEngine;

/// <summary>
/// プレイヤーの「爆発」アクションを管理するクラス
/// </summary>
public class PlayerExplosion : MonoBehaviour
{
    [Header("コンポーネント参照")]
    [Tooltip("爆発時の吹き飛ばしを処理するクラス")]
    [SerializeField] private PlayerRbMover playerRbMover;
    [Tooltip("爆発のトリガーとなる単語（Word）の配列")]
    [SerializeField] private WordExplosion[] wordExplosions;

    [Header("エフェクト")]
    [Tooltip("爆発時に再生するパーティクルなどのエフェクト")]
    [SerializeField] private GameObject explosionEffect;
    //爆発パラメータ
    private float explosionPower;

    private void Awake()
    {
        if (playerRbMover == null) { Debug.LogError("playerRbMoverが参照されていません"); return; }
        if(explosionEffect == null) { Debug.LogError("explosionEffectが参照されていません"); return; }
        if (wordExplosions == null) { Debug.LogError("wordExplosionsが参照されていません"); return; }
    }

    /// <summary>
    /// PlayerDataから爆発の基本威力を受け取りセット
    /// </summary>
    /// <param name="_data"></param>
    public void SetUp(PlayerData _data)
    {
        explosionPower = _data.ExplosionPower;
    }

    /// <summary>
    /// 爆発処理を実行
    /// </summary>
    /// <param name="_player"></param>
    public void Explosion(bool _isFlip, Collision2D _collision)
    {
        //爆発条件 配列の中に1つでも爆発トリガーがONのWordが存在するか
        bool _canExplode = wordExplosions.Any(w => w != null && w.IsExplosionTrigger);

        if (!_canExplode) return;

        //衝突したアイテムを非表示
        _collision.gameObject.SetActive(false);

        //爆発音を鳴らす
        SoundManager.Instance.PlaySE(SESource.EXPLOSION);

        //Flipの状態に応じて、力のかかる向きを変える
        float _currentPower = _isFlip ? -explosionPower : explosionPower;

        //エフェクトを再生
        explosionEffect.SetActive(false);
        explosionEffect.transform.position = transform.position;
        explosionEffect.SetActive(true);

        //物理挙動側に計算した力を渡して吹き飛ばす
        playerRbMover.ExplosionRb(_currentPower);
    }

    /// <summary>
    /// 爆発に関連するWordの状態を初期状態にリセットする
    /// </summary>
    public void ResetExplosion()
    {
        foreach (var _word in wordExplosions)
        {
            if (_word != null)
            {
                _word.ResetWord();
            }
        }
    }
}
