using System;
using UnityEngine;
using UnityEngine.EventSystems;
/// <summary>
/// 言葉の元となるもの
/// </summary>

public class BaseWord : MonoBehaviour, IWord
{
    //---言葉を完成した際のアニメーション
    [SerializeField] protected WordAnimationController wordAnimator;
    private bool isAnimated = false;
    //---言葉を使用した---
    public event Action WordComplete;

    private void Awake()
    {
        if (wordAnimator == null) { Debug.LogWarning("wordAnimatorが参照されていません"); return; }
    }

    private void OnEnable()
    {

    }
    /// <summary>
    /// 言葉の位置などをリセット
    /// </summary>
    public virtual void ResetWord()
    {
        if (isAnimated)
        {
            isAnimated = false;
            wordAnimator.EndAnimation();
        }
    }
    /// <summary>
    /// 言葉の効果
    /// </summary>
    /// <param name="_word"></param>
    public virtual void WordEffect()
    {
        isAnimated = true;

    }
    /// <summary>
    /// 言葉が完成した後
    /// </summary>

    public void FinishActionEvent()
    {
        WordComplete?.Invoke();
    }
}