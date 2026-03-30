using System.Linq;
using UnityEngine;

/// <summary>
/// プレイヤーの爆発の処理
/// </summary>
public class PlayerExplosion : MonoBehaviour
{
    //---物理移動---
    [SerializeField] private PlayerRbMover playerRbMover;
    [SerializeField] private WordExplosion[] wordExplosions;
    [SerializeField] private GameObject explosionEffect;
    //---爆発パラメーター
    private float explosionPower = 8;

    private void Awake()
    {
        if (playerRbMover == null) { Debug.LogError("playerRbMoverが参照されていません"); return; }
        if (wordExplosions == null) { Debug.LogError("wordExplosionsが参照されていません"); return; }
    }
    public void SetUp(PlayerData _data)
    {
        explosionPower = _data.explosionPower;
    }
    /// <summary>
    /// 爆発
    /// </summary>
    /// <param name="_player"></param>
    public void Explosion(PlayerController _player, Collision2D _collider)
    {
        bool _canExplode = wordExplosions.Any(w => w != null && w.IsExplosionTrigger);

        if (!_canExplode)
        {
            return;
        
        }
        _collider.gameObject.SetActive(false);

        SoundManager.Instance.PlaySE(SESource.EXPLOSION);

        float _power = explosionPower;

        //ひっくり返っていたら力の加わり方も逆になる
        if (!_player.IsFlip) _power = explosionPower;
        else _power = -explosionPower;

        explosionEffect.SetActive(false);
        explosionEffect.transform.position = transform.position;
        explosionEffect.SetActive(true);

        playerRbMover.ExplosionRb(_power);
    }

    public void ResetExplosion()
    {
        foreach (var wordExplosion in wordExplosions)
        {
            if (wordExplosion != null)
            {
                wordExplosion.ResetWord();
            }
        }
    }
}
