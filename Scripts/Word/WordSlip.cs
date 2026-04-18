using UnityEngine;
using System;

/// <summary>
/// 滑らせる足場を作成する(Slip)言葉の挙動を管理するクラス
/// </summary>
public class WordSlip : BaseWord
{
    [Header("ギミック参照")]
    [Tooltip("効果が発動した際に、滑るように変化させる足場の管理クラス")]
    [SerializeField] private StageSlipObject stageSlipObject;

    protected override  void Awake()
    {
        base.Awake();
        if(stageSlipObject == null) { Debug.LogError("stageSlipObjectが参照されていません"); return; }
    }

    /// <summary>
    /// 言葉の効果(足場を凍らして滑らせる)を発動させる
    /// </summary>
    public override void WordEffect()
    {
        //フラグをtrueにする
        base.WordEffect();

        SoundManager.Instance.PlaySE(SESource.SLIP);
        
        //言葉のアニメーションを再生
        wordAnimator.SlipAnimation();

        //ステージ上の対象のオブジェクトを滑る状態に変更
        stageSlipObject.SlipFloor();

        //全ての処理が終わったことを通知
        FinishActionEvent();
    }

    /// <summary>
    /// リトライ時などに、言葉と足場を初期状態に戻す
    /// </summary>
    public override void ResetWord()
    {
        base.ResetWord();

        //凍っていたオブジェクトを元に戻す
        stageSlipObject.ResetFloor();
    }
}
