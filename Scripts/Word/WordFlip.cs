using System;
using UnityEngine;

/// <summary>
/// プレイヤーの重力を反転する(Flip)言葉の挙動を管理するクラス
/// </summary>

public class WordFlip : BaseWord
{
    /// <summary>
    /// 言葉の効果(反転)を発動する
    /// </summary>
    public override void WordEffect()
    {
        //フラグをtrueにする
        base.WordEffect();

        SoundManager.Instance.PlaySE(SESource.FLIP);

        //言葉のアニメーションを再生する
        wordAnimator.FlipAnimation();

        //処理が終わったことを通知
        FinishActionEvent();
    }

    /// <summary>
    /// リトライ時などに、言葉とギミック状態を初期状態に戻す
    /// </summary>
    public override void ResetWord()
    {
        base.ResetWord();
    }
}
