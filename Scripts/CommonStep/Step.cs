using Cysharp.Threading.Tasks;
using System.Threading;
using UnityEngine;
using UnityEngine.Rendering;
/// <summary>
/// ステップの統括
/// </summary>
public class Step : MonoBehaviour
{
    [SerializeField] private StepBase[] flow;
    [SerializeField] private RetryWord retryWord;
    [SerializeField] private RetryObject[] retryObjects;
    [Header("爆発で消えたオブジェクトを元に戻す(これが設定しなくても動きます)")]
    [SerializeField] private ResetStage resetStage;

    private int currentStep = 0; //一番最初に戻る
    private const int FIRST_STEP = 0; 
    private void Awake()
    {
        if(flow == null) { Debug.LogError("flowが参照されていません"); return; }

        foreach (var _step in flow)
        {
            _step.OnFinishStep -= SetNextStep;
            _step.OnFinishStep += SetNextStep;
        }

        foreach (var _retryObject in retryObjects)
        {
            _retryObject.OnRetry += () => StageRetry().Forget();
        }

        currentStep = FIRST_STEP;
    }

    void OnEnable()
    {
        StartStep();
    }
    /// <summary>
    /// ステップに入る
    /// </summary>
    private void StartStep()
    {
        currentStep = FIRST_STEP; //Stepを一番初めからスタート
        flow[currentStep].EnterStep();
    }
    // Update is called once per frame
    void Update()
    {
        if (currentStep >= 0 && currentStep < flow.Length)
        {
            flow[currentStep].UpdateStep();
        }
        if (Input.GetKeyDown(KeyCode.R) && GameState.Instance.IsGame())
        {
            StageRetry().Forget();
        }
    }
    /// <summary>
    /// ステージをリトライするとき
    /// </summary>
    private async UniTaskVoid StageRetry()
    {
        await ExecuteRetryAllAsync(this.GetCancellationTokenOnDestroy());
        retryWord.WordAllReset();
        if(resetStage != null) { resetStage.ResetExplosionObject(); }
    }

    /// <summary>
    /// 次のステップに移行
    /// </summary>
    private void SetNextStep()
    {
        currentStep++;
        if(currentStep < flow.Length)
        {
            flow[currentStep].EnterStep();
        }
        else //1チュートリアルステージの処理がすべて終わったら
        {
            Debug.Log("一つのステップフローが終わりました");
            gameObject.SetActive(false);
        }
    }
    /// <summary>
    /// ステップを一番初めから
    /// </summary>
    public async UniTask ExecuteRetryAllAsync(CancellationToken token)
    {
        var tasks = flow.Select(step => step.RetryStep(token));
        await UniTask.WhenAll(tasks);

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
    public void OnDestroy()
    {
        foreach (var _step in flow)
        {
            _step.OnFinishStep -= SetNextStep;
        }

        foreach (var _retryObject in retryObjects)
        {
            _retryObject.OnRetry -= () => StageRetry().Forget();
        }
    }
}
