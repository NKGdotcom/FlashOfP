using Cysharp.Threading.Tasks;
using System;
using System.Collections;
using System.Threading;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChangeStep : StepBase
{
    [SerializeField] private Animator fadeAnimator;
    [SerializeField] private GameObject showStage; //出現させるステージ

    public void SetNextStage(GameObject stage)
    {
        showStage = stage;
    }

    private const string BOOL_CREAR = "Clear";

    private void Awake()
    {
        OnInitialized();
    }
    //初期処理
    public override void OnInitialized()
    {
        base.OnInitialized();

        if(fadeAnimator == null)
        {
            Debug.LogWarning("fadeAnimatorがnullです");
        }
        if(showStage == null)
        {
            Debug.LogWarning("showStageがnullです");
        }
    }

    public override void EnterStep(PlayerMoveInput _playerMoveInput)
    {
        _playerMoveInput.IsTutorial = true;
        WaitAnimationSequenceAsync(this.GetCancellationTokenOnDestroy()).Forget();
    }

    public override void UpdateStep()
    {

    }

    private async UniTask WaitAnimationSequenceAsync(CancellationToken _token)
    {
        fadeAnimator.SetBool(BOOL_CREAR, false);

        //再生するアニメーションを取得するため1フレーム待つ
        await UniTask.Yield(_token);

        AnimatorStateInfo _stateInfo = fadeAnimator.GetCurrentAnimatorStateInfo(0);

        await UniTask.Delay(TimeSpan.FromSeconds(_stateInfo.length), cancellationToken: _token);

        LoadScene();
    }

    //新しく出すシーンをロード
    private void LoadScene()
    {
        showStage.SetActive(true);
        Complete();
    }
}
