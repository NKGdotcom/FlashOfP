using UnityEngine;
using System;

public class WordDrop : BaseWord
{
    [SerializeField] private Animator dropAnimator;
    private const string DROP_STRING = "Drop";

    //アニメーションを再生
    public override void WordEffect(GameObject _word)
    {
        if (dropAnimator == null) { Debug.LogWarning("アニメーターが接続されていません"); return; }

        SoundManager.Instance.PlaySE(SESource.drop);
        dropAnimator.SetTrigger(DROP_STRING);
        FinishActionEvent(); //PObjがジャンプできるように
    }
}
