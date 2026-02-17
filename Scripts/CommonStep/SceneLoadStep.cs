using Cysharp.Threading.Tasks;
using System.Collections;
using System.Threading;
using UnityEngine;

public class SceneLoadStep : StepBase
{
    [SerializeField] private Animator fadeAnimator;
    [SerializeField] private Transform playerSetPos;
    [SerializeField] private bool isFirstTutorialOrStageSelect;

    private const string BOOL_CREAR = "Clear";

    private void Awake()
    {
        OnInitialized();
    }

    public override void OnInitialized()
    {
        base.OnInitialized();

        NullCheck();
    }

    private void NullCheck()
    {
        if (fadeAnimator == null) { Debug.LogWarning("fadeAnimatorÇ™nullÇ≈Ç∑"); return; }
    }

    public override void EnterStep(PlayerMoveInput _playerMoveInput)
    {
        if (!isFirstTutorialOrStageSelect)
        {
            _playerMoveInput.gameObject.SetActive(true);
            _playerMoveInput.transform.position = playerSetPos.position;
        }

        PlayerP _player = _playerMoveInput.GetComponent<PlayerP>();
        if(_player != null && _player.PlayerRb != null)
        {
            _player.PlayerRb.linearVelocity = Vector2.zero;
            _player.PlayerRb.angularVelocity = 0f;
            _player.transform.rotation = Quaternion.identity;
        }

        WaitAnimationSequenceAsync(this.GetCancellationTokenOnDestroy()).Forget();
    }

    public override void UpdateStep()
    {

    }

    private async UniTask WaitAnimationSequenceAsync(CancellationToken _token)
    {
        fadeAnimator.SetBool(BOOL_CREAR, true);

        await UniTask.Yield(_token);

        NextStep();
    }

    //éüÇÃèàóùÇé¿çs
    private void NextStep()
    {
        Complete();
    }
}
