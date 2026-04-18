using UnityEngine;
using System;

/// <summary>
/// 物を落とす(Drop)言葉の挙動を管理するクラス
/// </summary>
public class WordDrop : BaseWord
{
    [Header("落下ギミック設定")]
    [Tooltip("実際に落下させるオブジェクトを管理する")]
    [SerializeField] private StageDropObject stageDropObj;

    protected override void Awake()
    {
        base.Awake();
        if(stageDropObj == null) { Debug.LogError("stageDropObjが参照されていません"); return; }
    }

    /// <summary>
    /// 言葉の効果(落下)を発動する
    /// </summary>
    public override void WordEffect()
    {
        //フラグをtrueにする
        base.WordEffect();

        SoundManager.Instance.PlaySE(SESource.DROP);
        
        //言葉自身のアニメーションを再生
        wordAnimator.DropAnimation();

        //ステージ上のギミックを落下させる
        stageDropObj.DropAllObject();

        //全ての処理が終わったことを通知
        FinishActionEvent();
    }

    /// <summary>
    /// リトライ時などに、言葉とギミックを初期状態に戻す
    /// </summary>
    public override void ResetWord()
    {
        base.ResetWord();

        //落下したオブジェクトを初期位置に戻す
        stageDropObj.ResetPos();
    }
}