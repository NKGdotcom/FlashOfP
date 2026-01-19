using UnityEngine;
using System;

public class WordJump : ConditionBase, IWord
{
    [SerializeField] private Animator jumpAnimator;
    private const string JUMP_STRING = "Jump";

    private bool isComplete = false;
    public event Action FinishAction;


    //アニメーションを再生
    public void WordEffect(GameObject _word)
    {
        if (jumpAnimator == null)
        {
            Debug.LogWarning("アニメーターが接続されていません");
            return;
        }

        jumpAnimator.SetTrigger(JUMP_STRING);

        isComplete = true;
        FinishAction?.Invoke(); //PObjがジャンプできるように
    }

    public override bool CheckCondition()
    {
        return isComplete;
    }
}
