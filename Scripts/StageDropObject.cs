using NUnit.Framework;
using Unity.Burst;
using UnityEngine;

public class StageDropObject : MonoBehaviour
{
    [SerializeField] private Rigidbody2D[] dropRbLists;
    [SerializeField] private Vector2[] originPos;
    [SerializeField] private WordDrop wordDrop;

    private void Start()
    {
        if(wordDrop != null)
        {
            wordDrop.FinishAction += DropAllObject;
        }
    }

    private void OnEnable()
    {
        originPos = new Vector2[dropRbLists.Length];
        for (int i =0; i < dropRbLists.Length; i++)
        {
            originPos[i] = dropRbLists[i].transform.position;
        }
    }

    private void DropAllObject()
    {
        for (int i = 0; i < dropRbLists.Length; i++)
        {
            dropRbLists[i].bodyType = RigidbodyType2D.Dynamic;
        }
    }
}
