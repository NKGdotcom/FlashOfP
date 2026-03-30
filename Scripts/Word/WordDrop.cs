using UnityEngine;
using System;

/// <summary>
/// 物を落とす
/// </summary>
public class WordDrop : BaseWord
{
    [SerializeField] private StageDropObject stageDropObj;
    private void Awake()
    {
        if(stageDropObj == null) { Debug.LogError("stageDropObjが参照されていません"); return; }
    }

    //アニメーションを再生
    public override void WordEffect()
    {
        base.WordEffect();
        SoundManager.Instance.PlaySE(SESource.DROP);
        wordAnimator.DropAnimation();
        stageDropObj.DropAllObject();
        FinishActionEvent();
    }
    public override void ResetWord()
    {
        base.ResetWord();
        stageDropObj.ResetPos();
    }
}