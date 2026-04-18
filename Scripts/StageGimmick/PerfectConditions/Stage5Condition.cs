using UnityEngine;
/// <summary>
///ステージ5の条件付きクリアの条件
/// </summary>
public class Stage5Condition : BasePerfectCondition
{
    [Header("条件判定用ギミック")]
    [Tooltip("ステージ5にあるUpWord")]
    [SerializeField] private WordUp wordUp;

    private void Awake()
    {
        if(wordUp == null) { Debug.LogError("wordUpが参照されていません"); return; }
    }

    /// <summary>
    /// プレイヤーで浮遊の機能が発動しているかどうかをチェック
    /// </summary>
    /// <returns></returns>
    public override bool IsPerfect()
    {
        if (wordUp == null)
        {
            Debug.LogWarning("wordUpがセットされていません！");
            return false;
        }

        return !wordUp.IsUp;
    }
}
