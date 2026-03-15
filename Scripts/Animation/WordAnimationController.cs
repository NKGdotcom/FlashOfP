using Cysharp.Threading.Tasks;
using System.Threading;
using UnityEngine;

/// <summary>
/// 言葉のアニメーションの処理
/// </summary>
public class WordAnimationController : MonoBehaviour
{
    [SerializeField] private Animator wordAnimator;

    private static readonly int JumpAnimHash = Animator.StringToHash("Jump");
    private static readonly int FlipAnimHash = Animator.StringToHash("Flip");
    private static readonly int SlipAnimHash = Animator.StringToHash("Slip");
    private static readonly int DropAnimHash = Animator.StringToHash("Drop");
    private static readonly int UpAnimHash = Animator.StringToHash("Up");
    private static readonly int ExplosionAnimHash = Animator.StringToHash("Explosion");
    private static readonly int PlayerAnimHash = Animator.StringToHash("Player");
    private static readonly int PopcornAnimHash = Animator.StringToHash("Popcorn");
    private static readonly int EndAnimHash = Animator.StringToHash("End");

    private const float END_ANIMATION = 1.0f;

    private void Awake()
    {
        if(wordAnimator == null) { Debug.LogError("wordAnimatorが参照されていません"); return; }
    }
    /// <summary>
    /// 指定したアニメーションを再生し、終わるまで待機する
    /// </summary>
    private async UniTask PlayAnimationAndWaitAsync(int triggerHash, CancellationToken token)
    {
        var previousStateHash = wordAnimator.GetCurrentAnimatorStateInfo(0).fullPathHash;

        wordAnimator.SetTrigger(triggerHash);

        //---次のアニメーションに切り替わるまで待機---
        await UniTask.WaitWhile(() =>
            wordAnimator.GetCurrentAnimatorStateInfo(0).fullPathHash == previousStateHash,
            cancellationToken: token);

        //---新しいアニメーションが終わるまで待機---
        var _currentState = wordAnimator.GetCurrentAnimatorStateInfo(0);
        var _stateHash = _currentState.fullPathHash;

        //---同じアニメーションが再生中 かつ 再生割合が終了値未満の間、待機し続ける---
        await UniTask.WaitWhile(() =>
        {
            var _info = wordAnimator.GetCurrentAnimatorStateInfo(0);
            return _info.fullPathHash == _stateHash && _info.normalizedTime < END_ANIMATION;
        }, cancellationToken: token);
    }

    /// <summary>
    /// ジャンプアニメーション
    /// </summary>
    public void JumpAnimation()
    {
        wordAnimator.SetTrigger(JumpAnimHash);
    }
    /// <summary>
    /// 重力が逆になるアニメーション
    /// </summary>
    public void FlipAnimation()
    {
        wordAnimator.SetTrigger(FlipAnimHash);
    }
    /// <summary>
    /// 滑るアニメ―ション
    /// </summary>
    public void SlipAnimation()
    {
        wordAnimator.SetTrigger(SlipAnimHash);
    }
    /// <summary>
    /// 上に上がるアニメーション
    /// </summary>
    public void UpAnimation()
    {
        wordAnimator.SetTrigger(UpAnimHash);
    }
    /// <summary>
    /// 爆発アニメーション
    /// </summary>
    public void ExplosionAnimation()
    {
        wordAnimator.SetTrigger(ExplosionAnimHash);
    }
    /// <summary>
    /// ドロップアニメーション
    /// </summary>
    public void DropAnimation()
    {
        wordAnimator.SetTrigger(DropAnimHash);
    }
    /// <summary>
    /// プレイヤーアニメーション
    /// </summary>
    /// <param name="token"></param>
    /// <returns></returns>
    public UniTask PlayerAnimAnimationAsync(CancellationToken token)
            => PlayAnimationAndWaitAsync(PlayerAnimHash, token);
    /// <summary>
    /// ポップコーンアニメーション
    /// </summary>
    public void PopcornAnimation()
    {
        wordAnimator.SetTrigger(PopcornAnimHash);
    }
    /// <summary>
    /// リセットする際に呼び出す
    /// </summary>
    public void EndAnimation()
    {
        wordAnimator.SetTrigger(EndAnimHash);
    }
}
