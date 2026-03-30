using UnityEngine;
using UnityEngine.UI;
/// <summary>
/// Stage 4 PerfectCondition
/// </summary>
public class Stage4Condition : BasePerfectCondition
{
    [SerializeField] private Image cameraImage;
    [SerializeField] private ResetStage resetStage;
    private int targetActiveNum = 0;

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
