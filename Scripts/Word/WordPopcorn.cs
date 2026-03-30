using UnityEngine;
using System;

public class WordPopcorn : BaseWord
{
    [SerializeField] private Animator popcornAnimator;
    private const string POPCORN_STRING = "PopCorn";

    //アニメーションを再生
    public override void WordEffect(GameObject _word)
    {
        if (popcornAnimator == null) { Debug.LogWarning("アニメーターが接続されていません"); return; }

        popcornAnimator.SetTrigger(POPCORN_STRING);
        FinishActionEvent(); //ポップコーンの球を発射
    }
}
