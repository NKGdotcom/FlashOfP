using TMPro;
using UnityEngine;

public class StageSlipObject : MonoBehaviour
{
    [SerializeField] private WordSlip wordSlip;
    [SerializeField] private BoxCollider2D slipObject;
    [SerializeField] private PhysicsMaterial2D heaveyMaterial;
    [SerializeField] private PhysicsMaterial2D slipMaterial;
    private SpriteRenderer spriteRenderer;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Awake()
    {
        spriteRenderer = slipObject.GetComponent<SpriteRenderer>();
        wordSlip.FinishAction += SlipFloor;
    }

    //è∞Ç™ääÇÈÇÊÇ§Ç…
    private void SlipFloor()
    {
        slipObject.sharedMaterial = slipMaterial;
        spriteRenderer.color = Color.cyan;
    }

    private void OnEnable()
    {
        ResetFloor();
    }
    //è∞ÇÃèÛë‘Çå≥Ç…ñﬂÇ∑
    private void ResetFloor()
    {
        slipObject.sharedMaterial = heaveyMaterial;
        spriteRenderer.color = Color.white;
    }
}
