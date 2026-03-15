using Cysharp.Threading.Tasks;
using System;
using System.Collections;
using System.Threading;
using UnityEditor;
using UnityEngine;
/// <summary>
/// シーンの切り替えステップ
/// </summary>
public class SceneChangeStep : StepBase
{
    [SerializeField] private Fade fadeOut;
    [SerializeField] private GameObject nextStepStage; //出現させるステージ
    private void Awake()
    {
        if (fadeOut == null) { Debug.LogError("fadeOutが参照されていません"); return; }
    }
    public override void EnterStep()
    {
        FadeOutLoadSceneAsync(this.GetCancellationTokenOnDestroy()).Forget();
    }
    /// <summary>
    /// フェードアウトして、次のシーンをロードし、ステップを終了する
    /// </summary>
    private async UniTask FadeOutLoadSceneAsync(CancellationToken _token)
    {
        await fadeOut.FadeOutAsync(_token); 
        LoadScene(nextStepStage);  
    }
    /// <summary>
    /// 次に進むステージを設定
    /// </summary>
    /// <param name="stage"></param>
    public void SetNextStage(GameObject _stage)
    {
        nextStepStage = _stage;
    }
    public override void UpdateStep()
    {

    }
    public override async UniTask RetryStep(CancellationToken _token)
    {
        await fadeOut.FadeOutAsync(_token);
    }
    /// <summary>
    /// 新しく出すシーンをロード
    /// </summary>
    private void LoadScene(GameObject _nextStepStage)
    {
        _nextStepStage.SetActive(true);
        ExitStep();
    }
    public override void ExitStep()
    {
        Complete();
    }
}
