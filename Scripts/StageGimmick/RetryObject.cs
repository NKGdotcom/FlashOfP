using System;
using UnityEngine;
/// <summary>
/// ‰æ–ÊŠO‚É—‚¿‚½Û‚ÉƒŠƒgƒ‰ƒC‚ÅŒ³‚É–ß‚·
/// </summary>
public class RetryObject : MonoBehaviour
{
    public event Action OnRetry;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.TryGetComponent<PlayerController>(out var _player))
        {
            OnRetry?.Invoke();
        }
    }
}
