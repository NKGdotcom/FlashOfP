using NUnit.Framework;
using Unity.Burst;
using UnityEngine;
/// <summary>
/// ドロップで落とすオブジェクト
/// </summary>
public class StageDropObject : MonoBehaviour
{
    //---落とすオブジェクト---
    [SerializeField] private Rigidbody2D[] dropRbLists;
    private Vector2[] originPos;

    private void Awake()
    {
        originPos = new Vector2[dropRbLists.Length];

        for (int i = 0; i < dropRbLists.Length; i++)
        {
            originPos[i] = dropRbLists[i].transform.position;
        }
    }
    /// <summary>
    /// オブジェクトを能力ですべて落とす
    /// </summary>
    public void DropAllObject()
    {
        for (int i = 0; i < dropRbLists.Length; i++)
        {
            dropRbLists[i].bodyType = RigidbodyType2D.Dynamic;
        }
    }
    private void OnEnable()
    {
        ResetPos();
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
