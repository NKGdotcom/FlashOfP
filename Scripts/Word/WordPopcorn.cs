using UnityEngine;
using System;

/// <summary>
/// ポップコーンを発射
/// </summary>
public class WordPopcorn : BaseWord
{
    public bool IsPopcornTrigger { get; set; } = false;

    //アニメーションを再生
    public override void WordEffect()
    {
        base.WordEffect();
        SoundManager.Instance.PlaySE(SESource.POPCORN);
        wordAnimator.PopcornAnimation();
        IsPopcornTrigger = true;
        FinishActionEvent();
    }
    public override void ResetWord()
    {
        base.ResetWord();
        IsPopcornTrigger = false;
    }
}