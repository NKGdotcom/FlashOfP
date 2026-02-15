using UnityEngine;

public class PlayerExplosion : MonoBehaviour
{
    [SerializeField] private GameObject explosionEffect;
    [SerializeField] private float explosionPower = 8;

    //”š”­
    public void Explosion(PlayerP _player)
    {
        SoundManager.Instance.PlaySE(SESource.explosion);
        float _power = explosionPower;

        if (!_player.IsFlip)
        {
            _power = explosionPower;
        }
        else
        {
            _power = -explosionPower;
        }

        explosionEffect.SetActive(false);
        explosionEffect.transform.position = transform.position;
        explosionEffect.SetActive(true);

        _player.PlayerRb.linearVelocity = new Vector2(_player.PlayerRb.linearVelocity.x, _power);
    }
}
