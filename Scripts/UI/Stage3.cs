using UnityEngine;

public class Stage3 : MonoBehaviour
{
    [SerializeField] private Clear clear;
    private int perfectScoreShotNum = 10;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.TryGetComponent<PlayerP>(out var _player))
        {
            if (_player.ShotCount <= perfectScoreShotNum)
            {
                clear.Stage3PerfectClear = true;
            }
            clear.Stage3Clear = true;
        }
    }
}
