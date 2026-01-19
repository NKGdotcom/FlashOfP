using UnityEngine;
using System;

public class WordPopcorn : MonoBehaviour, IWord
{
    [SerializeField] private Animator popcornAnimator;
    private const string POPCORN_STRING = "PopCorn";
    private const string FINISH_STRING = "END";

    public event Action FinishAction;
    public void OnDisable()
    {
        popcornAnimator.SetTrigger(FINISH_STRING);
    }

    //アニメーションを再生
    public void WordEffect(GameObject _word)
    {
        if (popcornAnimator == null)
        {
            Debug.LogWarning("アニメーターが接続されていません");
            return;
        }
        popcornAnimator.SetTrigger(POPCORN_STRING);
        FinishAction?.Invoke(); //オブジェクトが逆向きに
    }
}
