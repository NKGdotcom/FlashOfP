using System.Collections;
using UnityEngine;
using static UnityEditor.Experimental.GraphView.GraphView;

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

        if (fadeAnimator == null)
        {
            Debug.LogWarning("fadeAnimatorÇ™nullÇ≈Ç∑");
        }
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

        StartCoroutine(WaitAnimationSequence());
    }

    public override void UpdateStep()
    {

    }

    private IEnumerator WaitAnimationSequence()
    {
        fadeAnimator.SetBool(BOOL_CREAR, true);

        yield return null;

        NextStep();
    }

    //éüÇÃèàóùÇé¿çs
    private void NextStep()
    {
        Complete();
    }
}
