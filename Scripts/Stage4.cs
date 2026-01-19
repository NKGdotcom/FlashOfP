using UnityEngine;

public class Stage4 : MonoBehaviour
{
    [SerializeField] private Clear clear;
    [SerializeField] private int explosionNum;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.TryGetComponent<PlayerP>(out var _player))
        {
            if (_player.ExplosionCount >= explosionNum)
            {
                clear.Stage4PerfectClear = true;
            }
            clear.Stage4Clear = true;
        }
    }
}
