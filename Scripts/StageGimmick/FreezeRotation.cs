using UnityEngine;

public class FreezeRotation : MonoBehaviour
{
    // 固定したい角度（基本は回転なし＝Quaternion.identity）
    private Quaternion fixedRotation;

    void Awake()
    {
        fixedRotation = transform.rotation;
    }

    void LateUpdate()
    {
        transform.rotation = fixedRotation;
    }
}