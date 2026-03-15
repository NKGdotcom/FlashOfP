using Cysharp.Threading.Tasks;
using System;
using System.Threading;
using UnityEngine;

/// <summary>
/// フェード用のアニメーション
/// </summary>
public class Fade : MonoBehaviour
{
    //---アニメーション---
    [SerializeField] private Animator fadeAnimator;
    private const string BOOL_CLEAR = "Clear";
    private void Awake()
    {
        if (fadeAnimator == null) { Debug.LogError("fadeAnimatorが参照されていません"); return; }
    }
    /// <summary>
    /// フェードインの流れ
    /// </summary>
    /// <param name="_token"></param>
    /// <returns></returns>
    public async UniTask FadeInAsync(CancellationToken _token)
    {
        fadeAnimator.SetBool(BOOL_CLEAR, true);
        //---1フレーム待つ---
        await UniTask.Yield(_token);
    }

    /// <summary>
    /// フェードアウトの流れ
    /// </summary>
    /// <param name="_token"></param>
    /// <returns></returns>
    public async UniTask FadeOutAsync(CancellationToken _token)
    {
        fadeAnimator.SetBool(BOOL_CLEAR, false);
        //---1フレーム待つ---
        await UniTask.Yield(_token);
        AnimatorStateInfo _stateInfo = fadeAnimator.GetCurrentAnimatorStateInfo(0);
        //---現在再生しているアニメーションの長さ分待つ---
        await UniTask.Delay(TimeSpan.FromSeconds(_stateInfo.length), cancellationToken: _token);
    }
}
