using UnityEngine;
using UnityEngine.UI;
/// <summary>
/// ステージ4の条件付きクリアの条件
/// </summary>
public class Stage4Condition : BasePerfectCondition
{
    [Header("条件判定用ギミック")]
    [Tooltip("お邪魔となる写真を設定したか")]
    [SerializeField] private Image cameraImage;
    [Tooltip("爆発で壊し、現在残っているオブジェクトの数")]
    [SerializeField] private ExplosionResetStage resetStage;
    private int targetActiveNum = 0;

    private void Awake()
    {
        if(cameraImage == null) { Debug.LogError("cameraImageが参照されていません"); return; }
        if(resetStage == null) { Debug.LogError("resetStageが参照されていません"); return; }
    }
    /// <summary>
    /// 写真を残しつつ、爆発を起こし、足場をすべて無くした場合
    /// </summary>
    /// <returns></returns>
    public override bool IsPerfect()
    {
        bool _isCameraActive = cameraImage.gameObject.activeSelf;
        cameraImage.gameObject.SetActive(false);
        if (resetStage == null)
        {
            Debug.LogWarning("ResetStage がセットされていません！");
            return false;
        }

        return (resetStage.GetActiveExplosionCount() <= targetActiveNum) && _isCameraActive;
    }
}
