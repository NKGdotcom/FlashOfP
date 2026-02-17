using UnityEngine;
using System;

public class WordSlip : BaseWord
{
    [SerializeField] private Animator slipAnimator;
    private const string SLIP_STRING = "Slip";

    //アニメーションを再生
    public override void WordEffect(GameObject _word)
    {
        if (slipAnimator == null) { Debug.LogWarning("アニメーターが接続されていません");  return; }

        SoundManager.Instance.PlaySE(SESource.slip);
        slipAnimator.SetTrigger(SLIP_STRING);
        FinishActionEvent(); //オブジェクトが滑るように
    }
}
