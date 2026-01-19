using TMPro;
using UnityEngine;

public class StageSlipObject : MonoBehaviour
{
    [SerializeField] private WordSlip wordSlip;
    [SerializeField] private BoxCollider2D slipObject;
    [SerializeField] private PhysicsMaterial2D physicsMaterial;
    private SpriteRenderer spriteRenderer;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        spriteRenderer = slipObject.GetComponent<SpriteRenderer>();
        wordSlip.FinishAction += SlipFloor;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void SlipFloor()
    {
        slipObject.sharedMaterial = physicsMaterial;
        spriteRenderer.color = Color.cyan;
    }
}
