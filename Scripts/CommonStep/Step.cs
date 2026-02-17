using UnityEngine;
using UnityEngine.Rendering;

public class Step : MonoBehaviour
{
    [SerializeField] private StepBase[] flow;
    [SerializeField] private PlayerMoveInput playerMoveInput;
    [SerializeField] private GameObject stageWords;

    private int currentStep = -1; //一番最初に戻る

    private void Awake()
    {
        OnInitialized();
    }

    //初期処理
    private void OnInitialized()
    {
        if (flow != null)
        {
            foreach (var _step in flow)
            {
                _step.OnStepCompleted -= SetNextStep;
                _step.OnStepCompleted += SetNextStep;
            }
        }
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void OnEnable()
    {
        if (currentStep == -1) //一番最初に戻る
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
            Debug.Log("ステップがが一つ終わりました");
            currentStep = -1;　//Stepを一番初めからスタート
            gameObject.SetActive(false);
        }
    }

    //リトライ機能
    public void RetryFromBeginning()
    {
        currentStep = -1; //一番初めからスタート
        stageWords.SetActive(false);
        stageWords.SetActive(true);
        SetNextStep();
    }

    //ステージをスキップ(ステージ選択に戻すのが一番の目的)
    public void ReturnToStageSelect()
    {
        currentStep = flow.Length - 2; //強制的に最後-1のステップに戻す
        SetNextStep();
    }
}
