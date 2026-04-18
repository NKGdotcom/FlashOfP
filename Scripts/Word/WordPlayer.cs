using Cysharp.Threading.Tasks;
using System;
using System.Collections;
using System.Threading;
using UnityEngine;

/// <summary>
/// プレイヤーをチュートリアルで生成する(Player)言葉の挙動を管理するクラス
/// </summary>
public class WordPlayer : BaseWord
{
    [Header("出現設定")]
    [Tooltip("言葉の効果で出現させるプレイヤーのオブジェクト")]
    [SerializeField] private GameObject playerObj;

    protected override void Awake()
    {
        base.Awake();
        if (playerObj == null) { Debug.LogError("playerObjが参照されていません"); return; }
    }

    /// <summary>
    /// 言葉の効果(プレイヤー出現)を発動する
    /// </summary>
    public override void WordEffect()
    {
        //フラグをtrueにする
        base.WordEffect();

        SoundManager.Instance.PlaySE(SESource.GET_WORD);
        
        //アニメーションの処理を非同期で進行
        WordPerformanceAsync(this.GetCancellationTokenOnDestroy()).Forget();
    }

    /// <summary>
    /// リトライ時などに、言葉とギミックを初期状態に戻す
    /// </summary>
    public override void ResetWord()
    {
        base.ResetWord();
    }

    /// <summary>
    /// アニメーション完了後にプレイヤーを出現させ、状態を切り替える
    /// </summary>
    /// <param name="_token"></param>
    /// <returns></returns>
    private async UniTaskVoid WordPerformanceAsync(CancellationToken _token)
    {
        //プレイヤーのアニメーションが終わるまで待機
        await wordAnimator.PlayerAnimAnimationAsync(_token);

        //アニメーションが終わったらプレイヤーを出現させる
        playerObj.SetActive(true);

        //全ての処理が完了したことを通知
        FinishActionEvent();
        GameState.Instance.SetState(State.EXPLAIN);
    }
}