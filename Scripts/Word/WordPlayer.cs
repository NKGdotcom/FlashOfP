using Cysharp.Threading.Tasks;
using System;
using System.Collections;
using System.Threading;
using UnityEngine;

/// <summary>
/// プレイヤーを生成
/// </summary>
public class WordPlayer : BaseWord
{
    [SerializeField] private GameObject playerObj;

    private void Awake()
    {
        if(playerObj == null) { Debug.LogError("playerObjが参照されていません"); return; }
    }
    private void OnEnable()
    {

    }
    /// <summary>
    /// アニメーションを再生
    /// </summary>
    public override void WordEffect()
    {
        SoundManager.Instance.PlaySE(SESource.GET_WORD);    
        WordPerformanceAsync(this.GetCancellationTokenOnDestroy()).Forget();
    }

    /// <summary>
    /// ワードパフォーマンス
    /// </summary>
    /// <param name="_token"></param>
    /// <returns></returns>
    public async UniTaskVoid WordPerformanceAsync(CancellationToken _token) 
    {
        await wordAnimator.PlayerAnimAnimationAsync(_token);

        playerObj.SetActive(true);
        FinishActionEvent();
    }
    public override void ResetWord()
    {
        base.ResetWord();
    }
}
