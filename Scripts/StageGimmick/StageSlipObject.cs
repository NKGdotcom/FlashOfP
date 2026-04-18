using UnityEngine;
/// <summary>
/// スリップで凍らせるオブジェクト
/// </summary>
public class StageSlipObject : MonoBehaviour
{
    [Header("ギミック対象")]
    [Tooltip("摩擦度を変更したいオブジェクト")]
    [SerializeField] private BoxCollider2D slipObject;
    [Tooltip("摩擦ありのPhysicsMaterial")]
    [SerializeField] private PhysicsMaterial2D heaveyMaterial;
    [Tooltip("摩擦なしでつるつるのPhysicsMaterial")]
    [SerializeField] private PhysicsMaterial2D slipMaterial;
    //見た目変更用に変える
    private SpriteRenderer spriteRenderer;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Awake()
    {
        if(slipObject == null) { Debug.LogError("slipObjectが参照されていません"); return; }
        if (heaveyMaterial == null) { Debug.LogError("heavyMaterialが参照されていません"); return; }
        if (slipMaterial == null) { Debug.LogError("slipMaterialが参照されていません"); return; }

        spriteRenderer = slipObject.GetComponent<SpriteRenderer>();
    }

    private void OnEnable()
    {
        ResetFloor();
    }

    /// <summary>
    /// 床が滑るようになる
    /// </summary>
    public void SlipFloor()
    {
        slipObject.sharedMaterial = slipMaterial;
        spriteRenderer.color = Color.cyan;
    }

    /// <summary>
    /// 床の状態を滑る前の元の状態に戻す
    /// </summary>
    public void ResetFloor()
    {
        slipObject.sharedMaterial = heaveyMaterial;
        spriteRenderer.color = Color.white;
    }
}
