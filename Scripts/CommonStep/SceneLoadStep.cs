using Cysharp.Threading.Tasks;
using System.Collections;
using System.Threading;
using UnityEngine;
/// <summary>
/// ステップが始まったときに呼び出されるステップ
/// フェードイン演出の開始、プレイヤーの初期位置・状態のセットを行い、次のステップへ進む
/// </summary>
public class SceneLoadStep : StepBase
{
    [Header("プレイヤー設定")]
    [Tooltip("操作対象のプレイヤーコントローラー")]
    [SerializeField] private PlayerController player;
    [Tooltip("プレイヤーの出現位置（ステージ選択画面以外で必須）")]
    [SerializeField] private Transform playerSetPos;
    [Tooltip("画面のフェードイン演出を行うコンポーネント")]
    [SerializeField] private Fade fadeIn;

    [Header("特殊ステージ判定")]
    [Tooltip("一番初めのチュートリアルかどうか（trueならプレイヤーを非表示）")]
    [SerializeField] private bool isFirstTutorial = false;
    [Tooltip("ステージ選択画面かどうか（trueならプレイヤーを非表示＆位置セット不要）")]
    [SerializeField] private bool isStageSelect = false;

    private void Awake()
    {
        if (player == null) { Debug.LogError("playerが参照されていません"); return; }
        if (fadeIn == null) { Debug.LogWarning("fadeInが参照されていません"); return; }

        if (!isStageSelect)
        {
            Debug.LogError("playerSetPosが参照されていません");
        }
    }

    /// <summary>
    /// このステップに入った瞬間に呼ばれる処理(初期化)
    /// </summary>
    public override void EnterStep()
    {
        //非同期でフェードインを開始
        fadeIn.FadeInAsync(this.GetCancellationTokenOnDestroy()).Forget();
        //プレイヤーの配置と初期化
        PlayerInitialSet();
        //ゲームの状態を説明モードへ移行
        GameState.Instance.SetState(State.EXPLAIN);
        ExitStep();
    }

    /// <summary>
    /// このステップにいる間マイフレーム呼ばれる更新処理
    /// </summary>
    public override void UpdateStep() { }

    /// <summary>
    /// このステップを終了して次へ進むときの処理
    /// </summary>
    public override void ExitStep() => Complete();

    /// <summary>
    /// ゲームをリトライした際に、このステップの状態を初期化
    /// </summary>
    /// <param name="token"></param>
    /// <returns></returns>
    public override UniTask RetryStep(CancellationToken token)
    {
        PlayerInitialSet();
        return UniTask.CompletedTask;
    }

    /// <summary>
    /// プレイヤーの能力リセット、表示/非表示の切り替え、初期位置への移動
    /// </summary>
    private void PlayerInitialSet()
    {
        //いったん非表示にし、能力をすべてリセット
        player.gameObject.SetActive(false);
        player.PlayerResetAbility();

        // 特殊なシーン（初回チュートリアル、ステージ選択）以外ならプレイヤーを表示する
        if (!isFirstTutorial && !isStageSelect)
        {
            player.gameObject.SetActive(true);
        }

        //ステージ選択画面でなければ、指定した初期位置へ移動させる
        if (!isStageSelect)
        {
            player.transform.position = playerSetPos.position;
        }
    }
}
