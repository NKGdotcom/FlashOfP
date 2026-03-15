using Cysharp.Threading.Tasks;
using System.Collections;
using System.Threading;
using UnityEngine;
/// <summary>
/// シーンの呼び出しステップ
/// </summary>
public class SceneLoadStep : StepBase
{
    //---プレイヤーの初期位置を設定---
    [SerializeField] private PlayerController player;
    [SerializeField] private Transform playerSetPos;
    [SerializeField] private bool isFirstTutorial = false;
    [SerializeField] private bool isStageSelect = false;
    //---フェードイン用---
    [SerializeField] private Fade fadeIn;

    private void Awake()
    {
        if (player == null) { Debug.LogError("playerが参照されていません"); return; }
        if (fadeIn == null) { Debug.LogWarning("fadeInが参照されていません"); return; }
    }
    public override void EnterStep()
    {
        fadeIn.FadeInAsync(this.GetCancellationTokenOnDestroy()).Forget();
        PlayerInitialSet();
        ExitStep();
    }
    /// <summary>
    /// プレイヤーの初期処理
    /// 初期位置に設定＋SetActiveをtrueにするか
    /// </summary>
    private void PlayerInitialSet()
    {
        player.gameObject.SetActive(false);
        player.PlayerResetAbility();
        //---一番初めのチュートリアルorステージ選択ではプレイヤーを表示しない---
        if (!isFirstTutorial && !isStageSelect)
        {
            player.gameObject.SetActive(true);
        }
        if (!isStageSelect)
        {
            player.transform.position = playerSetPos.position;
        }
    }
    public override void UpdateStep()
    {

    }
    public override UniTask RetryStep(CancellationToken token)
    {
        PlayerInitialSet();
        return UniTask.CompletedTask;
    }
    public override void ExitStep()
    {
        Complete();
    }
}
