using Cysharp.Threading.Tasks;
using System.Threading;
using UnityEngine;
using UnityEngine.Rendering;
/// <summary>
/// ステップの統括
/// </summary>
public class Step : MonoBehaviour
{
    [Header("進行フロー")]
    [Tooltip("このステージで実行するステップを順番に設定")]
    [SerializeField] private StepBase[] flow;

    [Header("リトライ関連")]
    [Tooltip("言葉のリセットを管理するコンポーネント")]
    [SerializeField] private RetryWord retryWord;

    [Tooltip("リトライ判定を持つオブジェクト(とげや落下判定など")]
    [SerializeField] private RetryObject[] retryObjects;

    [Header("爆発で消えたオブジェクトを元に戻す(未設定でも可能)")]
    [SerializeField] private ExplosionResetStage resetStage;

    //Rキーでリトライ可能なフロー数の上限
    private int tutorialNum = 4;

    //状態
    private int currentStep = 0; //一番最初に戻る
    private const int FIRST_STEP = 0;

    private void Awake()
    {
        if(flow == null) { Debug.LogError("flowが参照されていません"); return; }
        if(retryWord == null) { Debug.LogError("retryWordが参照されていません"); return; }
        if(retryObjects == null) { Debug.LogError("retryObjectsが参照されていません"); return; }
        if(resetStage == null) { Debug.LogError("resetStageが参照されていません"); return; }

        foreach (var _step in flow)
        {
            _step.OnFinishStep -= SetNextStep;
            _step.OnFinishStep += SetNextStep;
        }

        foreach (var _retryObject in retryObjects)
        {
            _retryObject.OnRetry += OnRetryTriggerd;
        }

        currentStep = FIRST_STEP;
    }

    void OnEnable()
    {
        StartStep();
    }

    // Update is called once per frame
    void Update()
    {
        //現在のステップの更新処理を毎フレーム呼ぶ
        if (currentStep >= 0 && currentStep < flow.Length)
        {
            flow[currentStep].UpdateStep();
        }

        //ゲームプレイ中かつゲーム本編のみリトライ
        if (Input.GetKeyDown(KeyCode.R) && GameState.Instance.IsGame() && flow.Length < tutorialNum)
        {
            OnRetryTriggerd();
        }
    }

    public void OnDestroy()
    {
        foreach (var _step in flow)
        {
            _step.OnFinishStep -= SetNextStep;
        }

        foreach (var _retryObject in retryObjects)
        {
            _retryObject.OnRetry -= OnRetryTriggerd; 
        }
    }

    /// <summary>
    /// フローを一番最初から開始
    /// </summary>
    private void StartStep()
    {
        currentStep = FIRST_STEP; 
        flow[currentStep].EnterStep();
    }

    /// <summary>
    /// 現在のステップが完了した際に呼ばれ、次のステップへ移行する
    /// </summary>
    private void SetNextStep()
    {
        currentStep++;
        if(currentStep < flow.Length)
        {
            //次のステップが存在すれば開始
            flow[currentStep].EnterStep();
        }
        else 
        {
            //全てのステップが完了した場合
            Debug.Log("一つのステップフローが終わりました");
            gameObject.SetActive(false); //このマネージャー自体の役割を終える
        }
    }
    /// <summary>
    /// 全てのステップに対してリトライ命令を出し、すべて完了するまで待つ
    /// </summary>
    public async UniTask ExecuteRetryAllAsync(CancellationToken token)
    {
        //全てのステップのRetryStepを非同期で並列実行
        var tasks = flow.Select(step => step.RetryStep(token));
        await UniTask.WhenAll(tasks);

        //全てのリセットが終わったら、再び最初のステップから開始
        StartStep();
    }
    /// <summary>
    /// ステップをスキップ
    /// </summary>
    public void SkipStep()
    {
        currentStep = flow.Length - 2; //強制的に最後-1のステップに戻す
        SetNextStep();
    }


    /// <summary>
    /// イベントから呼ばれる中継メソッド
    /// </summary>
    private void OnRetryTriggerd()
    {
        StageRetryAsync().Forget();
    }

    /// <summary>
    /// ステージをリトライする際の一連の非同期処理
    /// </summary>
    private async UniTaskVoid StageRetryAsync()
    {
        // 前っステップの初期化が完了するまで待つ
        await ExecuteRetryAllAsync(this.GetCancellationTokenOnDestroy());

        //単語ギミックと爆発オブジェクトを初期状態に戻す
        retryWord.WordAllReset();
        if (resetStage != null) { resetStage.ResetExplosionObject(); }
    }
}
