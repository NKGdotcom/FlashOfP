using NUnit.Framework;
using Unity.Burst;
using UnityEngine;

public class StageDropObject : MonoBehaviour
{
    [SerializeField] private Rigidbody2D[] dropRbLists;
    [SerializeField] private Vector2[] originPos;
    [SerializeField] private WordDrop wordDrop;

    private void Awake()
    {
        originPos = new Vector2[dropRbLists.Length];

        if (wordDrop != null)
        {
            wordDrop.FinishAction += DropAllObject;
        }
        for (int i = 0; i < dropRbLists.Length; i++)
        {
            originPos[i] = dropRbLists[i].transform.position;
        }
    }

    //オブジェクトを能力ですべて落とす
    private void DropAllObject()
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

    //初期位置に戻す
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
