using UnityEngine;
using System;
/// <summary>
/// 写真を撮る(Photo)言葉の挙動を管理するクラス
/// </summary>
public class WordPhoto : BaseWord
{
    [Header("コンポーネント参照")]
    [Tooltip("カメラで現在の映像を撮影する")]
    [SerializeField] private CameraCapture cameraCapture;

    protected override void Awake()
    {
        base.Awake();
        if(cameraCapture == null) { Debug.LogError("cameraCapture"); return; }
    }

    /// <summary>
    /// 言葉の効果(撮影)を発動する
    /// </summary>
    public override void WordEffect()
    {
        //フラグをtrueにする
        base.WordEffect();

        SoundManager.Instance.PlaySE(SESource.CAPTURE);
        
        //言葉自身のアニメーションを再生
        wordAnimator.PhotoAnimation();

        //ステージの写真撮影をして、画面の邪魔をする
        cameraCapture.CaptureAndSave();

        //全ての処理が終わったことを通知
        FinishActionEvent();
    }

    /// <summary>
    /// リトライ時などに、言葉とギミックを初期状態に戻す
    /// </summary>
    public override void ResetWord()
    {
        cameraCapture.ClearCameraImage();
    }
}
