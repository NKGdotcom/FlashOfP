using UnityEngine;
using System;

public class WordDrop : MonoBehaviour, IWord
{
    [SerializeField] private Animator dropAnimator;
    private const string DROP_STRING = "Drop";
    private const string FINISH_STRING = "END";

    public event Action FinishAction;
    public void OnDisable()
    {
        dropAnimator.SetTrigger(FINISH_STRING);
    }

    //アニメーションを再生
    public void WordEffect(GameObject _word)
    {
        if (dropAnimator == null)
        {
            Debug.LogWarning("アニメーターが接続されていません");
            return;
        }
        SoundManager.Instance.PlaySE(SESource.drop);
        dropAnimator.SetTrigger(DROP_STRING);
        FinishAction?.Invoke(); //PObjがジャンプできるように
    }
}
