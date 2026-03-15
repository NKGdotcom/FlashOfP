using UnityEngine;
using System;
/// <summary>
/// 物を滑らせる
/// </summary>
public class WordSlip : BaseWord
{
    [SerializeField] private StageSlipObject stageSlipObject;
    //アニメーションを再生
    public override void WordEffect()
    {
        base.WordEffect();
        SoundManager.Instance.PlaySE(SESource.SLIP);
        wordAnimator.SlipAnimation();
        stageSlipObject.SlipFloor();
        FinishActionEvent();
    }
    public override void ResetWord()
    {
        base.ResetWord();
        stageSlipObject.ResetFloor();
    }
}
