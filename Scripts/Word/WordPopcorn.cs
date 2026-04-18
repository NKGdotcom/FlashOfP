using UnityEngine;
using System;

/// <summary>
/// ポップコーンを発射する(popcorn)言葉の挙動を管理するクラス
/// </summary>
public class WordPopcorn : BaseWord
{
    /// <summary>
    /// ポップコーンの効果が発動されているかを確認
    /// </summary>
    public bool IsPopcornTrigger { get; set; } = false;

    /// <summary>
    /// 言葉の効果(ポップコーン発射)を発動する
    /// </summary>
    public override void WordEffect()
    {
        //フラグをtrueにする
        base.WordEffect();

        SoundManager.Instance.PlaySE(SESource.POPCORN);

        //言葉自身のアニメーションを再生
        wordAnimator.PopcornAnimation();

        IsPopcornTrigger = true;

        //全ての処理が終わったことを通知
        FinishActionEvent();
    }

    /// <summary>
    /// リトライ時などに、言葉とギミックを初期状態に戻す
    /// </summary>
    public override void ResetWord()
    {
        base.ResetWord();
        IsPopcornTrigger = false;
    }
}