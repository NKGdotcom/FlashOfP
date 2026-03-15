using UnityEngine;
using System;
/// <summary>
/// プレイヤーがジャンプする
/// </summary>
public class WordJump :BaseCondition, IWord
{
    //---ジャンプアニメーション---
    [SerializeField] private WordAnimationController wordAnimator;
    //---完了---
    public event Action WordComplete;

    public bool IsJumpTrigger { get; set; } = false;
    private void Awake()
    {
        if (wordAnimator == null) { Debug.LogWarning("wordAnimatorが参照されていません"); return; }
    }
    private void OnEnable()
    {
        IsJumpTrigger = false;
    }
    //アニメーションを再生
    public void WordEffect()
    {
        wordAnimator.JumpAnimation();
        isFinish = true;
        IsJumpTrigger = true;
        WordComplete?.Invoke();
    }
    public void ResetWord()
    {
        if (IsJumpTrigger)
        {
            wordAnimator.EndAnimation();
            isFinish = false;
            IsJumpTrigger = false;
        }
    }
}
