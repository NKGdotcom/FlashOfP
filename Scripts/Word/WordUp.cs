using UnityEngine;
using System;

/// <summary>
/// 浮遊する(Up)言葉の挙動を管理するクラス
/// </summary>
public class WordUp : BaseWord
{
    /// <summary>
    /// 浮遊の効果が発動しているかを確認
    /// </summary>
    public bool IsUp { get; private set; } = false;

    /// <summary>
    /// 言葉の効果(Up)を発動する
    /// </summary>
    public override void WordEffect()
    {
        //フラグをtrueにする
        base.WordEffect();

        IsUp = true;
        
        SoundManager.Instance.PlaySE(SESource.UP);
        
        //言葉のアニメーションを再生
        wordAnimator.UpAnimation();
        
        //アクションが終わったことを伝える
        FinishActionEvent();
    }

    /// <summary>
    /// リトライ時などに言葉とギミックを初期状態に戻す
    /// </summary>
    public override void ResetWord()
    {
        base.ResetWord();
        IsUp = false;
    }
}