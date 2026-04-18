using Cysharp.Threading.Tasks;
using System;
using System.Collections;
using System.Threading;
using Unity.Cinemachine;
using UnityEngine;
/// <summary>
/// ゴールに入ったときの処理クラス
/// 条件としてActionStepの終了条件としてもある
/// </summary>
public class Goal : BaseCondition
{
    [Header("演出：カメラ")]
    [Tooltip("ゴール到達時に切り替えるCinemachineカメラ")]
    [SerializeField] private CinemachineCamera goalCamera;

    //通常のカメラ優先度
    private int defaultCameraPriority = 0;
    //ゴール演出時のカメラ優先度
    private int goalCameraPriority = 20;

    [Header("演出：アニメーション&タイミング")]
    [SerializeField] private Animator goalAnimator;
    //音を鳴らすタイミングをゴールが完成したらにする
    private float waitSoundPlay = 2 / 3f;
    //エフェクトがほぼ終わったタイミング
    private float waitEffectTime = 2f;

    [Header("クリア判定設定")]
    [Tooltip("ステージごとのクリア状況を保存するデータ")]
    [SerializeField] private Clear clearData;
    [Tooltip("このステージのインデックス番号（-1の場合はクリア判定を行わない）")]
    [SerializeField] private int stageIndex;
    private const int NOT_STAGE_INDEX = -1;
    [Tooltip("パーフェクトクリアの条件判定を行うクラス")]
    [SerializeField] private BasePerfectCondition perfectCondition;

    private static readonly int GoalTriggerHash = Animator.StringToHash("Goal");
    private static readonly int EndTriggerHash = Animator.StringToHash("End");

    private CancellationTokenSource disableCts;

    void Awake()
    {
        if (goalCamera == null) { Debug.LogWarning("goalCameraが設定されていません"); return; }
        if (goalAnimator == null) { Debug.LogWarning("goalAnimatorが設定されていません"); return; }

        if(stageIndex != NOT_STAGE_INDEX)
        {
            if(clearData == null) { Debug.LogError("clearDataが参照されていません"); }
            if(perfectCondition == null) { Debug.LogError("perfectConditionが参照されていません"); }
        }
    }

    private void OnEnable()
    {
        GoalInitialize();
    }

    private void OnDisable()
    {
        CancelAndDisposeToken();
        goalAnimator.SetTrigger(EndTriggerHash);
    }

    /// <summary>
    /// ゴール演出に必要な状態の初期化
    /// </summary>
    private void GoalInitialize()
    {
        //アニメーションを再生し、
        goalAnimator.ResetTrigger(EndTriggerHash);
        goalCamera.Priority = defaultCameraPriority;

        CancelAndDisposeToken();
        disableCts = new CancellationTokenSource();

        isFinish = false;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.TryGetComponent<PlayerController>(out var _player))
        {
            //プレイヤーを非表示にして演出を開始
            _player.gameObject.SetActive(false);
            GoalCinemachine(disableCts.Token).Forget();

            //ステージではない場合は判定をスキップ
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
        //カメラをズームし、アニメーション再生
        goalCamera.Priority = goalCameraPriority;
        goalAnimator.SetTrigger(GoalTriggerHash);

        //指定時間待ってからゴール完了SEを鳴らす
        await UniTask.Delay(TimeSpan.FromSeconds(waitSoundPlay), cancellationToken: _token);
        SoundManager.Instance.PlaySE(SESource.STAGE_CLEAR);

        //エフェクトが落ち着くまで待機
        await UniTask.Delay(TimeSpan.FromSeconds(waitEffectTime), cancellationToken: _token); //少し待って次のアクションに移る

        //演出完了時の処理
        GoalNextAction();
    }

    /// <summary>
    /// ゴール演出がすべて終わった後に呼ばれ、クリア条件を満たした状態にする
    /// </summary>
    public void GoalNextAction()
    {
        goalAnimator.ResetTrigger(GoalTriggerHash);

        //BaseConditionのフラグをtrueにすることで、ActionStepに終わったことを伝える
        isFinish = true;
    }

    /// <summary>
    /// クリア状況を記録する
    /// </summary>
    public void ClearJudgment()
    {
        var _currentStageData = clearData.stageDataList[stageIndex];
        _currentStageData.isClear = true;

        if (perfectCondition.IsPerfect())
        {
            _currentStageData.isPerfectClear = true;
        }
    }

    /// <summary>
    /// リトライ時などに呼ばれる状態リセット処理
    /// </summary>
    public override void ResetCondition()
    {
        CancelAndDisposeToken();
        goalAnimator.SetTrigger(EndTriggerHash);
        GoalInitialize();
    }

    /// <summary>
    /// 実行中の非同期処理をキャンセルし、トークンを安全に破棄
    /// </summary>
    private void CancelAndDisposeToken()
    {
        if (disableCts != null)
        {
            disableCts.Cancel();
            disableCts.Dispose();
            disableCts = null;
        }
    }
}
