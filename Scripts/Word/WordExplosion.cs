using UnityEngine;
using System;

/// <summary>
/// 爆発させる(Explosion)言葉の挙動を管理するクラス
/// </summary>
public class WordExplosion : BaseWord
{
    /// <summary>
    /// 爆発させる言葉の効果が発動しているかどうか
    /// </summary>
    public bool IsExplosionTrigger { get; private set; } = false;

    /// <summary>
    /// 言葉の効果(爆発)を発動する
    /// </summary>
    public override void WordEffect()
    {
        //フラグをtrueにする
        base.WordEffect();

        SoundManager.Instance.PlaySE(SESource.EXPLOSION);

        //言葉自身のアニメーションを再生
        wordAnimator.ExplosionAnimation();

        //フラグ発動
        IsExplosionTrigger = true;
        
        //全ての処理が終わったことを通知
        FinishActionEvent();
    }

    /// <summary>
    /// リトライ時などに、言葉とギミックを初期状態に戻す
    /// </summary>
    public override void ResetWord()
    {
        base.ResetWord();
        IsExplosionTrigger = false;
    }
}