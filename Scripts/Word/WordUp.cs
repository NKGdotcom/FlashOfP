using UnityEngine;
using System;
/// <summary>
/// 上にゆっくり浮かぶ形をとる
/// </summary>
public class WordUp : BaseWord
{
    public bool IsUp { get; private set; } = false;

    // アニメーションを再生
    public override void WordEffect()
    {
        base.WordEffect();

        IsUp = true;

        SoundManager.Instance.PlaySE(SESource.UP);
        wordAnimator.UpAnimation();
        FinishActionEvent();
    }
    public override void ResetWord()
    {
        base.ResetWord();
        IsUp = false;
    }
}
