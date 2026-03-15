using UnityEngine;
/// <summary>
/// スリップで凍らせるオブジェクト
/// </summary>
public class StageSlipObject : MonoBehaviour
{
    //---凍らせるオブジェクト---
    [SerializeField] private BoxCollider2D slipObject;
    [SerializeField] private PhysicsMaterial2D heaveyMaterial;
    [SerializeField] private PhysicsMaterial2D slipMaterial;
    private SpriteRenderer spriteRenderer;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Awake()
    {
        spriteRenderer = slipObject.GetComponent<SpriteRenderer>();
    }
    private void OnEnable()
    {
        ResetFloor();
    }
    /// <summary>
    /// 床が滑るように
    /// </summary>
    public void SlipFloor()
    {
        slipObject.sharedMaterial = slipMaterial;
        spriteRenderer.color = Color.cyan;
    }
    /// <summary>
    /// 床の状態を元に戻す
    /// </summary>
    public void ResetFloor()
    {
        slipObject.sharedMaterial = heaveyMaterial;
        spriteRenderer.color = Color.white;
    }
}
