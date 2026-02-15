using UnityEngine;
using System;

public class WordUp : MonoBehaviour, IWord
{
    [SerializeField] private Animator upAnimator;
    private const string UP_STRING = "Up";
    private const string FINISH_STRING = "END";

    public event Action FinishAction;
    public void OnDisable()
    {
        upAnimator.SetTrigger(FINISH_STRING);
    }

    //アニメーションを再生
    public void WordEffect(GameObject _word)
    {
        if (upAnimator == null)
        {
            Debug.LogWarning("アニメーターが接続されていません");
            return;
        }
        upAnimator.SetTrigger(UP_STRING);
        FinishAction?.Invoke(); //浮かぶ
    }
}
