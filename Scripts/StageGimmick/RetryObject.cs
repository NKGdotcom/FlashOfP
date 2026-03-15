using System;
using UnityEngine;
/// <summary>
/// リトライが必要とされるオブジェクト
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
