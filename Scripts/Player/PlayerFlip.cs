using UnityEngine;

public class PlayerFlip : MonoBehaviour
{
    //d—Í‚ğ‹t‚É
    public void ChangeGravity(Rigidbody2D _playerRb)
    {
        _playerRb.gravityScale = -1f; 
    }

    //d—Í‚ğŒ³‚É–ß‚·
    public void ResetGravity(Rigidbody2D _playerRb)
    {
        _playerRb.gravityScale = 1f; 
    }
}
