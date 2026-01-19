using UnityEngine;
using UnityEngine.Rendering;

public class Step : MonoBehaviour
{
    [SerializeField] private StepBase[] flow;
    [SerializeField] private PlayerMoveInput playerMoveInput;

    private int currentStep = -1; 

    private void Awake()
    {
        OnInitialized();
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void OnEnable()
    {
        if (currentStep == -1)
        {
            SetNextStep();
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (currentStep >= 0 && currentStep < flow.Length)
        {
            flow[currentStep].UpdateStep();
        }
    }

    //初期処理
    private void OnInitialized()
    {
        if(flow != null)
        {
            foreach(var _step in flow)
            {
                _step.OnStepCompleted -= SetNextStep;
                _step.OnStepCompleted += SetNextStep;
            }
        }
    }

    //次のチュートリアルステップに移行
    private void SetNextStep()
    {
        currentStep++;
        if(currentStep < flow.Length)
        {
            flow[currentStep].EnterStep(playerMoveInput);
        }
        else //1チュートリアルステージの処理がすべて終わったら
        {
            Debug.Log("チュートリアルが一つ終わりました");
            currentStep = -1;
            this.gameObject.SetActive(false);
        }
    }

    //リトライ機能
    public void RetryFromBeginning()
    {
        currentStep = -1;
        SetNextStep();
    }

    //ステージをスキップ(ステージ選択に戻すのが一番の目的)
    public void ReturnToStageSelect()
    {
        currentStep = flow.Length - 2; //強制的に最後-1のステップに戻す
        SetNextStep();
    }
}
