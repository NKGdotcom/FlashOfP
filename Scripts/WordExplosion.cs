using UnityEngine;
using System;

public class WordExplosion : MonoBehaviour, IWord
{
    [SerializeField] private Animator explosionAnimator;
    private const string EXPLOSION_STRING = "Explosion";
    private const string FINISH_STRING = "END";

    public event Action FinishAction;
    public void OnDisable()
    {
        explosionAnimator.SetTrigger(FINISH_STRING);
    }

    //アニメーションを再生
    public void WordEffect(GameObject _word)
    {
        if (explosionAnimator == null)
        {
            Debug.LogWarning("アニメーターが接続されていません");
            return;
        }
        explosionAnimator.SetTrigger(EXPLOSION_STRING);
        FinishAction?.Invoke(); //爆発
    }
}
