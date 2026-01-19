using UnityEngine;
using UnityEngine.Rendering;

public class PlayerUp : MonoBehaviour
{
    [SerializeField] private float upSpeed = 1f;

    //è„Ç…ïÇÇ©Ç‘
    public void Floating(PlayerP _player)
    {
        if (_player.IsUp)
        {
            _player.PlayerRb.linearVelocity = new Vector2(_player.PlayerRb.linearVelocity.x, upSpeed);
        }
    }
}
