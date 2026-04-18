using UnityEngine;
using System;

/// <summary>
/// ジャンプする(Jump)言葉の挙動を管理するクラス
/// </summary>
public class WordJump : BaseCondition, IWord
{
    [Header("アニメーション制御")]
    [Tooltip("言葉のアニメーションを統括するコントローラ")]
    [SerializeField] private WordAnimationController wordAnimator;

    //言葉の完成時に呼ばれる
    public event Action WordComplete;

    //言葉が初期状態にリセットされた時に呼ばれる
    public event Action WordReset;

    /// <summary>
    /// Jumpの効果が発動しているか
    /// </summary>
    public bool IsJumpTrigger { get; set; } = false;

    private void Awake()
    {
        if(wordAnimator == null) { Debug.LogError("wordAnimatorが参照されていません"); return; }
    }

    private void OnEnable()
    {
        IsJumpTrigger = false;
    }

    //言葉の効果(Jump)を発動する
    public void WordEffect()
    {
        //言葉のアニメーションを再生
        wordAnimator.JumpAnimation();

        isFinish = true;
        
        //ジャンプのフラグをtrueにする
        IsJumpTrigger = true;
        
        WordComplete?.Invoke();
    }

    /// <summary>
    /// リトライ時などに言葉とギミックを初期状態に戻す
    /// </summary>
    public void ResetWord()
    {
        if (IsJumpTrigger)
        {
            //言葉のアニメーションを初期状態に戻す
            wordAnimator.EndAnimation();
            
            isFinish = false;

            IsJumpTrigger = false;

            //リセットが終わった
            WordReset?.Invoke();
        }
    }
}
