using UnityEngine;
using System;

public class WordJump : ConditionBase, IWord
{
    [SerializeField] private Animator jumpAnimator;
    private RectTransform rectTransform;
    private const string JUMP_STRING = "Jump";
    private const string FINISH_STRING = "End";

    private Vector2 originPos;
    private bool isComplete = false;
    public event Action FinishAction;

    private void Awake()
    {
        rectTransform = GetComponent<RectTransform>();
        originPos = rectTransform.anchoredPosition;
    }

    private void OnEnable()
    {
        rectTransform.anchoredPosition = originPos;
    }
    //アニメーションを再生
    public void WordEffect(GameObject _word)
    {
        if (jumpAnimator == null) { Debug.LogWarning("アニメーターが接続されていません"); return; }

        jumpAnimator.SetTrigger(JUMP_STRING);

        isComplete = true;
        FinishAction?.Invoke(); //PObjがジャンプできるように
    }

    public override bool CheckCondition()
    {
        return isComplete;
    }
}
