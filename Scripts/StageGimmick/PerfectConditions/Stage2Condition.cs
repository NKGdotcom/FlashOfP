using UnityEngine;
/// <summary>
/// ステージ2の条件付きクリアの条件
/// </summary>
public class Stage2Condition : BasePerfectCondition
{
    [Header("条件判定用ギミック")]
    [Tooltip("ステージ2にあるFlipWord")]
    [SerializeField] private WordFlip wordFlip;
    [Tooltip("ステージ2にあるJumpWord")]
    [SerializeField] private WordJump wordJump;

    //それぞれが完了したかどうかを記録するフラグ
    private bool isFlipComplete = false;
    private bool isJumpComplete = false;

    private void Awake()
    {
        if(wordFlip == null) { Debug.LogError("wordFlipが参照されていません"); return; }
        if(wordJump == null) { Debug.LogError("wordJumpが参照されていません"); return; }
    }

    private void OnEnable()
    {
        isFlipComplete = false;
        isJumpComplete = false;

        wordFlip.WordComplete += FlipComplete;
        wordJump.WordComplete += JumpComplete;
        wordFlip.WordReset += ResetComplete;
        wordJump.WordReset += ResetComplete;
    }

    private void OnDisable()
    {
        wordFlip.WordComplete -= FlipComplete;
        wordJump.WordComplete -= JumpComplete;
        wordFlip.WordReset -= ResetComplete;
        wordJump.WordReset -= ResetComplete;
    }

    /// <summary>
    /// Flipを完成させていたら条件達成
    /// </summary>
    private void FlipComplete()
    {
        isFlipComplete = true;
    }

    /// <summary>
    /// Jumpを完成させていたら条件達成
    /// </summary>
    private void JumpComplete()
    {
        isJumpComplete = true;
    }

    /// <summary>
    /// ステージを再起する際にいったんクリア条件をリセット
    /// </summary>
    private void ResetComplete()
    {
        isFlipComplete = false;
        isJumpComplete = false;
    }

    /// <summary>
    /// FlipとJumpをどちらも完成させたらクリア
    /// </summary>
    /// <returns></returns>
    public override bool IsPerfect()
    {
        return isFlipComplete && isJumpComplete;
    }
}
