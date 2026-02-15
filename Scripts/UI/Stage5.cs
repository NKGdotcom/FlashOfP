using UnityEngine;

public class Stage5 : MonoBehaviour
{
    [SerializeField] private Clear clear;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.TryGetComponent<PlayerP>(out var _player))
        {
            if (!_player.IsUp)
            {
                clear.Stage5PerfectClear = true;
            }
            clear.Stage5Clear = true;
        }
    }
}
