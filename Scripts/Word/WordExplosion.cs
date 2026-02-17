using UnityEngine;
using System;

public class WordExplosion : BaseWord
{
    [SerializeField] private Animator explosionAnimator;
    private const string EXPLOSION_STRING = "Explosion";

    //アニメーションを再生
    public override void WordEffect(GameObject _word)
    {
        if (explosionAnimator == null) { Debug.LogWarning("アニメーターが接続されていません"); return;}

        explosionAnimator.SetTrigger(EXPLOSION_STRING);
        FinishActionEvent(); //爆発ができるように
    }
}
