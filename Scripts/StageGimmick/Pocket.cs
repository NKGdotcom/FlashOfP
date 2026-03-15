using UnityEngine;

/// <summary>
/// ポップコーンステージで使用
/// ポップコーンに触れた場合何か処理を実行
/// </summary>
public class Pocket : MonoBehaviour
{
    [SerializeField] private GameObject displayObj;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.TryGetComponent<Popcorn>(out var popcorn))
        {
            displayObj.SetActive(true);
        }
    }
}
