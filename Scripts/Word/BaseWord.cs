using System;
using UnityEngine;
using UnityEngine.EventSystems;
/// <summary>
/// ゲーム内に登場する言葉の基本となるクラス
/// アニメーションの制御や、完成時のイベント通知
/// </summary>
public class BaseWord : MonoBehaviour, IWord
{
    [Header("アニメーション制御")]
    [Tooltip("言葉のアニメーションを統括するコントローラ")]
    [SerializeField] protected WordAnimationController wordAnimator;

    //すでにアニメーションが実行されたかどうか
    private bool isAnimated = false;
    
    //言葉の完成時に呼ばれる
    public event Action WordComplete;

    //言葉が初期状態にリセットされた時に呼ばれる
    public event Action WordReset;

    protected virtual void Awake()
    {
        if (wordAnimator == null) { Debug.LogWarning("wordAnimatorが参照されていません"); return; }
    }

    /// <summary>
    /// 言葉の効果を発動する
    /// </summary>
    /// <param name="_word"></param>
    public virtual void WordEffect()
    {
        isAnimated = true;
    }

    /// <summary>
    /// 言葉の状態やアニメーションを初期状態に戻す
    /// </summary>
    public virtual void ResetWord()
    {
        //既に使われている場合のみリセット処理を行う
        if (isAnimated)
        {
            isAnimated = false;
            wordAnimator.EndAnimation();
            WordReset?.Invoke();
        }
    }
    
    /// <summary>
    /// 言葉のアニメーションや効果が全て完成した後に呼ばれる処理
    /// </summary>
    public void FinishActionEvent() => WordComplete?.Invoke();
}