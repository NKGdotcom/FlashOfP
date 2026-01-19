using UnityEngine;

public class Stage2 : MonoBehaviour
{
    [SerializeField] private Clear clear;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.TryGetComponent<PlayerP>(out var _player))
        {
            if(_player.IsJump && _player.IsFlip)
            {
                clear.Stage2PerfectClear = true;
            }
            clear.Stage2Clear = true;
        }
    }
}
