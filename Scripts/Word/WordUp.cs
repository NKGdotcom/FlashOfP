using UnityEngine;
using System;

public class WordUp : BaseWord
{ 
    [SerializeField] private Animator upAnimator;
    private const string UP_STRING = "Up";

    //アニメーションを再生
    public override void WordEffect(GameObject _word)
    {
        if (upAnimator == null) { Debug.LogWarning("アニメーターが接続されていません"); return; }

        upAnimator.SetTrigger(UP_STRING);
        FinishActionEvent(); //浮かぶ
    }
}
