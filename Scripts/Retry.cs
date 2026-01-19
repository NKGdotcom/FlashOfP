using System.Collections;
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
            StartCoroutine(StageRetry());
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.TryGetComponent<PlayerP>(out var _player))
        {
            _player.gameObject.SetActive(false);
            StartCoroutine(StageRetry());
        }
    }
    private IEnumerator StageRetry()
    {
        SoundManager.Instance.PlaySE(SESource.retry);
        _animator.SetBool(CLEAR, false);
        yield return new WaitForSeconds(waitTime);
        step.RetryFromBeginning();
    }
}
