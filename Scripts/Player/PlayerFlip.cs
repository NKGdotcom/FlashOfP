using UnityEngine;

public class PlayerFlip : MonoBehaviour
{
    public void GravityChange(Rigidbody2D _playerRb)
    {
        _playerRb.gravityScale = -1f; //d—Í‚ğ‹t‚É
    }
}
