using Cysharp.Threading.Tasks;
using System;
using System.Collections;
using System.Threading;
using UnityEngine;

/// <summary>
/// ステージのシーン切り替え演出を行うステップ(終わりに呼び出す)
/// フェードアウトを行い、次のステージを出現させてから修了
/// </summary>
public class SceneChangeStep : StepBase
{
    [Header("遷移設定")]
    [Tooltip("画面のフェードアウト演出を行うコンポーネント")]
    [SerializeField] private Fade fadeOut;
    [Tooltip("次に出現させるGameObject")]
    [SerializeField] private GameObject nextStepStage;

    private void Awake()
    {
        if (fadeOut == null) { Debug.LogError("fadeOutが参照されていません"); return; }
    }

    /// <summary>
    /// このステップに入った瞬間に呼ばれる処理(初期化)
    /// </summary>
    public override void EnterStep()
    {
        FadeOutLoadSceneAsync(this.GetCancellationTokenOnDestroy()).Forget();
    }

    /// <summary>
    /// このステップにいる間マイフレーム呼ばれる更新処理
    /// </summary>
    public override void UpdateStep() { }

    /// <summary>
    /// このステップを終了して次へ進むときの処理
    /// </summary>
    public override void ExitStep() => Complete();

    /// <summary>
    /// 次に進むステージを設定
    /// </summary>
    /// <param name="stage"></param>
    public void SetNextStage(GameObject _stage)
    {
        nextStepStage = _stage;
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
    /// 次のステージを出現させる
    /// </summary>
    private void LoadScene(GameObject _nextStepStage)
    {
        _nextStepStage.SetActive(true);
        ExitStep();
    }
    
    /// <summary>
    /// ゲームをリトライした際に、このステップの状態を初期化
    /// </summary>
    /// <param name="token"></param>
    /// <returns></returns>
    public override async UniTask RetryStep(CancellationToken _token)
    {
        await fadeOut.FadeOutAsync(_token);
    }
}
