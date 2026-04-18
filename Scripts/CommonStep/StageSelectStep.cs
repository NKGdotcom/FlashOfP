using UnityEngine;

/// <summary>
/// ステージ選択画面の進行を管理するステップ
/// ユーザーがボタンを押すのを待機し、選択されたステージを次のシーン遷移ステップに渡す
/// </summary>
public class StageSelectStep : StepBase
{
    [Header("UI参照")]
    [Tooltip("画面内に配置されている全ステージ選択ボタンの配列")]
    [SerializeField] private BaseStageSelect[] stageSelectButtonLists;

    [Header("遷移先ステップ")]
    [Tooltip("ステージ決定後に実行されるシーン遷移（ロード）ステップ")]
    [SerializeField] private SceneChangeStep nextSceneStep;

    private void Awake()
    {
        if(stageSelectButtonLists == null) { Debug.LogError("stageSelectButtonListsが参照されていません"); return;}
        if(nextSceneStep == null) { Debug.LogError("nextSceneStepが参照されていません"); return; }

        foreach(var stageSelect in stageSelectButtonLists)
        {
            stageSelect.OnClick += OnStageSelected;
        }
    }

    private void OnDestroy()
    {
        foreach (var stageSelect in stageSelectButtonLists)
        {
            stageSelect.OnClick -= OnStageSelected;
        }
    }

    /// <summary>
    /// このステップに入った瞬間に呼ばれる処理(初期化)
    /// </summary>
    public override void EnterStep()
    {
        GameState.Instance.SetState(State.STAGE_SELECT);
    }

    /// <summary>
    /// このステップにいる間マイフレーム呼ばれる更新処理
    /// </summary>
    public override void UpdateStep() { }

    /// <summary>
    /// このステップを終了して次へ進むときの処理
    /// </summary>
    public override void ExitStep()
    {
        Complete();
    }

    /// <summary>
    /// いずれかのステージ選択ボタンが押された時呼ばれる処理
    /// ボタンに設定しているオブジェクトを呼び出す
    /// </summary>
    /// <param name="_selectedStage"></param>
    private void OnStageSelected(GameObject _selectedStage)
    {
        if(_selectedStage != null)
        {
            nextSceneStep.SetNextStage(_selectedStage);
        }
        ExitStep();
    }
}
