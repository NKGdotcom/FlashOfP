using System;
using UnityEngine;
/// <summary>
/// プレイヤーを重力を反転
/// </summary>
public class WordFlip : BaseWord
{
    //アニメーションを再生
    public override void WordEffect()
    {
        base.WordEffect();
        SoundManager.Instance.PlaySE(SESource.FLIP);
        wordAnimator.FlipAnimation();
        FinishActionEvent();
    }
    public override void ResetWord()
    {
        base.ResetWord();
    }
}
