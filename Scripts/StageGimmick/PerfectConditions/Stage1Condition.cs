using UnityEngine;

/// <summary>
/// ステージ1の条件付きクリアの条件
/// </summary>
public class Stage1Condition : BasePerfectCondition
{
    [Header("条件判定用ギミック")]
    [Tooltip("条件付きクリアのために設定時間を設ける")]
    [SerializeField] private float perfectTime = 6f;
    private float timer = 0f;

    private void OnEnable()
    {
        timer = 0;
    }
    private void Update()
    {
        timer += Time.deltaTime;
    }
    /// <summary>
    /// ステージ1では制限時間内にゴールできれば条件付きクリア
    /// </summary>
    /// <returns></returns>
    public override bool IsPerfect()
    {
        return perfectTime > timer;
    }
}
