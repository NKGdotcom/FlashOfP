using System;
using UnityEngine;

public class WordFlip : MonoBehaviour, IWord
{
    [SerializeField] private Animator flipAnimator;
    private const string SLIP_STRING = "Flip";
    private const string FINISH_STRING = "END";

    public event Action FinishAction;
    public void OnDisable()
    {
        flipAnimator.SetTrigger(FINISH_STRING);
    }

    //アニメーションを再生
    public void WordEffect(GameObject _word)
    {
        if (flipAnimator == null)
        {
            Debug.LogWarning("アニメーターが接続されていません");
            return;
        }
        flipAnimator.SetTrigger(SLIP_STRING);
        FinishAction?.Invoke(); //オブジェクトが逆向きに
    }
}
