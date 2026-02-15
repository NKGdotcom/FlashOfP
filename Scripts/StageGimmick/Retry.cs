using Cysharp.Threading.Tasks;
using System;
using System.Collections;
using System.Threading;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Retry : MonoBehaviour
{
    [SerializeField] private Step step;
    [SerializeField] private Animator _animator;
    private const string CLEAR = "Clear";
    private float waitTime = 0.5f;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            StageRetryAsync(this.GetCancellationTokenOnDestroy()).Forget();
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.TryGetComponent<PlayerP>(out var _player))
        {
            _player.gameObject.SetActive(false);
            StageRetryAsync(this.GetCancellationTokenOnDestroy()).Forget();
        }
    }
    private async UniTask StageRetryAsync(CancellationToken _token)
    {
        SoundManager.Instance.PlaySE(SESource.retry);
        _animator.SetBool(CLEAR, false);
        await UniTask.Delay(TimeSpan.FromSeconds(waitTime), cancellationToken: _token);
        step.RetryFromBeginning();
    }
}
