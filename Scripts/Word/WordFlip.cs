using System;
using UnityEngine;

public class WordFlip : BaseWord
{
    [SerializeField] private Animator flipAnimator;
    private const string SLIP_STRING = "Flip";

    //アニメーションを再生
    public override void WordEffect(GameObject _word)
    {
        if (flipAnimator == null) { Debug.LogWarning("アニメーターが接続されていません"); return; }

        flipAnimator.SetTrigger(SLIP_STRING);
        FinishActionEvent(); //オブジェクトが逆向きに
    }
}
