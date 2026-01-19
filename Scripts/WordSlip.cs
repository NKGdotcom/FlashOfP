using UnityEngine;
using System;

public class WordSlip : MonoBehaviour, IWord
{
    [SerializeField] private Animator slipAnimator;
    private const string SLIP_STRING = "Slip";
    private const string FINISH_STRING = "END";

    public event Action FinishAction;
    public void OnDisable()
    {
        slipAnimator.SetTrigger(FINISH_STRING);
    }

    //アニメーションを再生
    public void WordEffect(GameObject _word)
    {
        if (slipAnimator == null)
        {
            Debug.LogWarning("アニメーターが接続されていません");
            return;
        }
        SoundManager.Instance.PlaySE(SESource.slip);
        slipAnimator.SetTrigger(SLIP_STRING);
        FinishAction?.Invoke(); //オブジェクトが滑るように
    }
}
