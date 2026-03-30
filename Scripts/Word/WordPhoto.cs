using UnityEngine;
using System;

public class WordPhoto : BaseWord
{
    [SerializeField] private CameraCapture cameraCapture;

    //アニメーションを再生
    public override void WordEffect()
    {
        base.WordEffect();
        SoundManager.Instance.PlaySE(SESource.CAPTURE);
        wordAnimator.PhotoAnimation();
        cameraCapture.CaptureAndSave();
        FinishActionEvent();
    }
    public override void ResetWord()
    {
        cameraCapture.ClearCameraImage();
    }
}
