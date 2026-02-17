using Cysharp.Threading.Tasks;
using System;
using System.Collections;
using System.Threading;
using Unity.Cinemachine;
using UnityEngine;

public class Goal : ConditionBase
{
    private bool isGoal = false;

    [SerializeField] private CinemachineCamera goalCamera;
    [SerializeField] private Animator goalAnimator;

    private const string GOAL_TRIGGER = "Goal";
    private const string FINISH_STRING = "End";
    private int defaultCamera = 0;
    private int priorityCamera = 20;
    private float waitEffectTime = 2f;
    private float waitSoundPlay = 2 / 3f;

    private CancellationTokenSource disableCts;

    void Awake()
    {
        NullCheck();
    }

    //nullチェック
    private void NullCheck()
    {
        if (goalCamera == null) { Debug.LogWarning("goalCameraが設定されていません"); return; }
        if (goalAnimator == null) { Debug.LogWarning("goalAnimatorが設定されていません"); return; }
    }

    private void OnEnable()
    {
        goalCamera.Priority = defaultCamera;
        disableCts = new CancellationTokenSource();
        isGoal = false;
    }

    //ゴールしたかの確認
    public override bool CheckCondition()
    {
        return isGoal;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.TryGetComponent<PlayerP>(out var pObj))
        {
            collision.gameObject.SetActive(false);
            GoalEffectAsync(this.GetCancellationTokenOnDestroy()).Forget();
        }
    }

    //ゴールエフェクトのタイミング
     private async UniTaskVoid GoalEffectAsync(CancellationToken _token)
    {
        Debug.Log("ゴールエフェクト発火");
        goalCamera.Priority = priorityCamera;
        goalAnimator.SetTrigger(GOAL_TRIGGER);

        await UniTask.Delay(TimeSpan.FromSeconds(waitSoundPlay), cancellationToken: _token); //ゴールポイントの演出に対応して音を鳴らす

        SoundManager.Instance.PlaySE(SESource.stageclear);

        await UniTask.Delay(TimeSpan.FromSeconds(waitEffectTime), cancellationToken: _token); //少し待って次のアクションに移る

        GoalNextAction();
    }

    //ゴール演出が終わった後、何をするか
    public void GoalNextAction()
    {
        Debug.Log("終了");
        goalAnimator.ResetTrigger(GOAL_TRIGGER);
        isGoal = true;
    }

    private void OnDisable()
    {
        if (disableCts != null)
        {
            disableCts.Cancel();
            disableCts.Dispose();
            disableCts = null;
        }
        goalAnimator.SetTrigger(FINISH_STRING);
    }
}
