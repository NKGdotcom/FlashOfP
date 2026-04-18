using UnityEngine;
/// <summary>
/// ステージ上の指定されたオブジェクト群を落下させるギミック管理クラス
/// </summary>
public class StageDropObject : MonoBehaviour
{
    [Header("落下ギミック設定")]
    [Tooltip("Dropの効果で落下させるオブジェクト")]
    [SerializeField] private Rigidbody2D[] dropRbLists;
    private Vector2[] originPos;

    private void Awake()
    {
        if(dropRbLists == null) { Debug.LogError("dropRbListsが参照されていません"); return; }
        originPos = new Vector2[dropRbLists.Length];

        for (int i = 0; i < dropRbLists.Length; i++)
        {
            originPos[i] = dropRbLists[i].transform.position;
        }
    }

    private void OnEnable()
    {
        ResetPos();
    }

    /// <summary>
    /// オブジェクトをDropの効果が発動したら全て落とす
    /// </summary>
    public void DropAllObject()
    {
        for (int i = 0; i < dropRbLists.Length; i++)
        {
            dropRbLists[i].bodyType = RigidbodyType2D.Dynamic;
        }
    }

    /// <summary>
    /// 初期位置に戻す
    /// </summary>
    public void ResetPos()
    {
        for (int i = 0; i < dropRbLists.Length; i++)
        {
            dropRbLists[i].transform.position = originPos[i];
            dropRbLists[i].transform.rotation = Quaternion.Euler(0, 0, 0);
            dropRbLists[i].bodyType = RigidbodyType2D.Kinematic;
        }
    }
}
