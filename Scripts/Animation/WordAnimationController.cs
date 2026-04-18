using Cysharp.Threading.Tasks;
using System.Threading;
using UnityEngine;

/// <summary>
/// テキストのアニメーションの処理の統括
/// </summary>
public class WordAnimationController : MonoBehaviour
{
    [Header("コンポーネント参照")]
    [Tooltip("アニメーションを制御する対象のAnimator")]
    [SerializeField] private Animator wordAnimator;

    //処理負荷を抑えるためにハッシュ値
    private static readonly int JumpAnimHash = Animator.StringToHash("Jump");
    private static readonly int FlipAnimHash = Animator.StringToHash("Flip");
    private static readonly int SlipAnimHash = Animator.StringToHash("Slip");
    private static readonly int DropAnimHash = Animator.StringToHash("Drop");
    private static readonly int UpAnimHash = Animator.StringToHash("Up");
    private static readonly int ExplosionAnimHash = Animator.StringToHash("Explosion");
    private static readonly int PlayerAnimHash = Animator.StringToHash("Player");
    private static readonly int PopcornAnimHash = Animator.StringToHash("Popcorn");
    private static readonly int PhotoAnimHash = Animator.StringToHash("Photo");
    private static readonly int EndAnimHash = Animator.StringToHash("End");

    //アニメーションの終了を判定する
    private const float END_ANIMATION = 1.0f;

    private void Awake()
    {
        if(wordAnimator == null) { Debug.LogError("wordAnimatorが参照されていません"); return; }
    }

    /// <summary>
    /// 指定したアニメーションのTriggerを引き、アニメーションが終了するまで、非同期で待機
    /// </summary>
    private async UniTask PlayAnimationAndWaitAsync(int triggerHash, CancellationToken token)
    {
        //現在のアニメーターの状態を記憶
        var previousStateHash = wordAnimator.GetCurrentAnimatorStateInfo(0).fullPathHash;
        
        //アニメーションのトリガー発動
        wordAnimator.SetTrigger(triggerHash);

        //次のアニメーションに切り替わるまで待機
        await UniTask.WaitWhile(() =>
            wordAnimator.GetCurrentAnimatorStateInfo(0).fullPathHash == previousStateHash,
            cancellationToken: token);

        //新しいアニメーションの情報を取得
        var _currentState = wordAnimator.GetCurrentAnimatorStateInfo(0);
        var _stateHash = _currentState.fullPathHash;

        //同じアニメーションが再生中 かつ 再生割合が終了値未満の間、待機し続ける
        await UniTask.WaitWhile(() =>
        {
            var _info = wordAnimator.GetCurrentAnimatorStateInfo(0);
            return _info.fullPathHash == _stateHash && _info.normalizedTime < END_ANIMATION;
        }, cancellationToken: token);
    }

    /// <summary>
    /// プレイヤーアニメーションを再生し、完了するまで待機する
    /// </summary>
    /// <param name="token"></param>
    /// <returns></returns>
    public UniTask PlayerAnimAnimationAsync(CancellationToken token)
            => PlayAnimationAndWaitAsync(PlayerAnimHash, token);

    //アニメーションを呼び出す際
    /// <summary>
    /// ジャンプテキストアニメーション
    /// </summary>
    public void JumpAnimation() => wordAnimator.SetTrigger(JumpAnimHash);

    /// <summary>
    /// 重力反転テキストのアニメーション
    /// </summary>
    public void FlipAnimation() => wordAnimator.SetTrigger(FlipAnimHash);

    /// <summary>
    /// 滑るテキストアニメ―ション
    /// </summary>
    public void SlipAnimation() => wordAnimator.SetTrigger(SlipAnimHash);

    /// <summary>
    /// 上昇テキストアニメーション
    /// </summary>
    public void UpAnimation() => wordAnimator.SetTrigger(UpAnimHash);

    /// <summary>
    /// 爆発テキストアニメーション
    /// </summary>
    public void ExplosionAnimation() => wordAnimator.SetTrigger(ExplosionAnimHash);

    /// <summary>
    /// 落下テキストアニメーション
    /// </summary>
    public void DropAnimation() => wordAnimator.SetTrigger(DropAnimHash);

    /// <summary>
    /// ポップコーン発射テキストアニメーション
    /// </summary>
    public void PopcornAnimation() => wordAnimator.SetTrigger(PopcornAnimHash);

    /// <summary>
    /// カメラ撮影
    /// </summary>
    public void PhotoAnimation() => wordAnimator.SetTrigger(PhotoAnimHash);

    /// <summary>
    /// 初期状態へのリセット
    /// </summary>
    public void EndAnimation() => wordAnimator.SetTrigger(EndAnimHash);
}
