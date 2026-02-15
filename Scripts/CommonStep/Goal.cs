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
    private int priorityCamera = 20;
    private float waitEffectTime = 2f;
    private float waitSoundPlay = 2 / 3f;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        NullCheck();
    }

    //nullチェック
    private void NullCheck()
    {
        if (goalCamera == null)
        {
            Debug.LogWarning("goalCameraが設定されていません");
        }
        if (goalAnimator == null)
        {
            Debug.LogWarning("goalAnimatorが設定されていません");
        }
    }

    //ゴール演出が終わった後、何をするか
    public void GoalNextAction()
    {
        goalAnimator.ResetTrigger(GOAL_TRIGGER);
        isGoal = true;
    }

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

     private async UniTaskVoid GoalEffectAsync(CancellationToken _token)
    {
        goalCamera.Priority = priorityCamera;
        goalAnimator.SetTrigger(GOAL_TRIGGER);

        await UniTask.Delay(TimeSpan.FromSeconds(waitSoundPlay), cancellationToken: _token); //音をアニメーションに合わせてタイミング良く鳴らす

        SoundManager.Instance.PlaySE(SESource.stageclear);

        await UniTask.Delay(TimeSpan.FromSeconds(waitEffectTime), cancellationToken: _token); //エフェクトを鳴らしてから少し待つ(アニメーションで鳴らしてます)

        GoalNextAction();
    }
}
