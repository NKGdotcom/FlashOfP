using System.Collections;
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
        StartCoroutine(WaitAnimationSequence());
    }

    public override void UpdateStep()
    {

    }

    private IEnumerator WaitAnimationSequence()
    {
        fadeAnimator.SetBool(BOOL_CREAR, false);

        //再生するアニメーションを取得するため1フレーム待つ
        yield return null;

        AnimatorStateInfo _stateInfo = fadeAnimator.GetCurrentAnimatorStateInfo(0);

        yield return new WaitForSeconds(_stateInfo.length);

        LoadScene();

        yield break;
    }

    //新しく出すシーンをロード
    private void LoadScene()
    {
        showStage.SetActive(true);
        Complete();
    }
}
