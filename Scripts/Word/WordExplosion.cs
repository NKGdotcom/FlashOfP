using UnityEngine;
using System;
/// <summary>
/// 床を爆発させる
/// </summary>
public class WordExplosion : BaseWord
{
    public bool IsExplosionTrigger { get; private set; } = false;
    //アニメーションを再生
    public override void WordEffect()
    {
        SoundManager.Instance.PlaySE(SESource.EXPLOSION);
        wordAnimator.ExplosionAnimation();
        IsExplosionTrigger = true;
        FinishActionEvent();
    }
    public override void ResetWord()
    {
        base.ResetWord();
        IsExplosionTrigger = false;
    }
}
