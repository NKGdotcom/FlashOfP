using Cysharp.Threading.Tasks;
using System;
using System.Collections;
using System.Threading;
using Unity.Cinemachine;
using UnityEngine;
/// <summary>
/// ゴールしたら次のステップに進む
/// </summary>
public class Goal : BaseCondition
{
    //---ゴールのカメラ演出---
    [SerializeField] private CinemachineCamera goalCamera;
    private int defaultCamera = 0;
    private int priorityCamera = 20;
    //---ゴールしたときの演出---
    [SerializeField] private Animator goalAnimator;
    private const string GOAL_TRIGGER = "Goal";
    private const string FINISH_STRING = "End";
    //---音を鳴らすタイミングをゴールが完成したらにする---
    private float waitSoundPlay = 2 / 3f;
    //---エフェクトがほぼ終わったタイミング---
    private float waitEffectTime = 2f; 
    //---どんなクリアか判定---
    [SerializeField] private Clear clearData;
    [SerializeField] private int stageIndex;
    [SerializeField] private BasePerfectCondition perfectCondition;
    private const int NOT_STAGE_INDEX = -1;

    private CancellationTokenSource disableCts;
    void Awake()
    {
        if (goalCamera == null) { Debug.LogWarning("goalCameraが設定されていません"); return; }
        if (goalAnimator == null) { Debug.LogWarning("goalAnimatorが設定されていません"); return; }
    }
    private void OnEnable()
    {
        GoalInitialize();
    }
    /// <summary>
    /// ゴールのスクリプトの初期化
    /// </summary>
    private void GoalInitialize()
    {
        goalAnimator.ResetTrigger(FINISH_STRING);
        goalCamera.Priority = defaultCamera;
        disableCts = new CancellationTokenSource();
        isFinish = false;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.TryGetComponent<PlayerController>(out var _player))
        {
            _player.gameObject.SetActive(false);
            GoalCinemachine(this.GetCancellationTokenOnDestroy()).Forget();
            if (stageIndex == NOT_STAGE_INDEX) return;
            ClearJudgment();
        }
    }
    /// <summary>
    /// ゴールのシネマシーン
    /// </summary>
    /// <param name="_token"></param>
    /// <returns></returns>
    private async UniTaskVoid GoalCinemachine(CancellationToken _token)
    {
        goalCamera.Priority = priorityCamera;
        goalAnimator.SetTrigger(GOAL_TRIGGER);

        //---GoalPointのアニメーションがちょうど終わったタイミングで鳴らす---
        await UniTask.Delay(TimeSpan.FromSeconds(waitSoundPlay), cancellationToken: _token);
        SoundManager.Instance.PlaySE(SESource.STAGE_CLEAR);

        //---エフェクトのクラッカーが大体終わったら次に移る---
        await UniTask.Delay(TimeSpan.FromSeconds(waitEffectTime), cancellationToken: _token); //少し待って次のアクションに移る

        GoalNextAction();
    }
    /// <summary>
    /// ゴールシネマシーンが終わったら
    /// </summary>
    public void GoalNextAction()
    {
        Debug.Log("終了");
        goalAnimator.ResetTrigger(GOAL_TRIGGER);
        isFinish = true;
    }
    /// <summary>
    /// クリア判定を取る
    /// </summary>
    public void ClearJudgment()
    {
        var _currentStageData = clearData.stageDataList[stageIndex];
        Debug.Log(_currentStageData);
        _currentStageData.isClear = true;

        if (perfectCondition.IsPerfect())
        {
            _currentStageData.isPerfectClear = true;
        }
    }
    public override void ResetCondition()
    {
        if (disableCts != null)
        {
            disableCts.Cancel();
            disableCts.Dispose();
            disableCts = null;
        }
        goalAnimator.SetTrigger(FINISH_STRING);

        GoalInitialize();
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
