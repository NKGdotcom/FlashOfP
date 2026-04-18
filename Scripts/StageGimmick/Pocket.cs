using UnityEngine;

/// <summary>
/// ポップコーンステージで使用
/// プレイヤーが放つポップコーンをポケットの中に入れたらオブジェクトを表示
/// </summary>
public class Pocket : MonoBehaviour
{
    [Header("ギミック参照")]
    [Tooltip("ゴールへの道を表示")]
    [SerializeField] private GameObject displayObj;

    private void Awake()
    {
        if(displayObj == null) { Debug.LogError("displayObjが参照されていません"); return; }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.TryGetComponent<Popcorn>(out var popcorn))
        {
            displayObj.SetActive(true);
        }
    }
}
