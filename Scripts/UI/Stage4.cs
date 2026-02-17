using UnityEngine;

public class Stage4 : MonoBehaviour
{
    [SerializeField] private Clear clear;
    [SerializeField] private int explosionNum;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.TryGetComponent<PlayerP>(out var _player))
        {
            if (IsAllExplosion(_player))
            {
                clear.Stage4PerfectClear = true;
            }
            clear.Stage4Clear = true;
        }
    }

    //全てのオブジェクトを爆発したら
    private bool IsAllExplosion(PlayerP _player)
    {
        return _player.ExplosionCount >= explosionNum;
    }
}
