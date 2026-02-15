using Cysharp.Threading.Tasks;
using System;
using System.Collections;
using System.Threading;
using UnityEngine;

public class PlayerPopcorn : MonoBehaviour
{
    [SerializeField] private GameObject popcornPrefab;
    [SerializeField] private Transform spawnPoint;
    [SerializeField] private float minForce = 5f;
    [SerializeField] private float maxForce = 8f;
    [SerializeField] private float shotInterval = 1f;
    [Range(0f, 1f)]
    [SerializeField] private float spreadAmount = 0.2f;
    [SerializeField] private float destroyInterval = 5f;

    //ポップコーンを投げる準備
    public async UniTask AutoShotPopcornAsync(PlayerP _player, CancellationToken _token)
    {
        while (true)
        {
            await UniTask.WaitUntil(() => _player.IsPopcorn, cancellationToken: _token);

            PopcornShot(_player);

            await UniTask.Delay(TimeSpan.FromSeconds(shotInterval), cancellationToken: _token);
        }
    }

    //ポップコーンを放つ
    private void PopcornShot(PlayerP _player)
    {
        SoundManager.Instance.PlaySE(SESource.popcorn);

        _player.ShotCount++;

        GameObject _popcorn = Instantiate(popcornPrefab, spawnPoint.position, UnityEngine.Random.rotation);
        Rigidbody2D _popcornRb = _popcorn.GetComponent<Rigidbody2D>();

        Vector3 _randomSpread = UnityEngine.Random.insideUnitSphere * spreadAmount;
        Vector3 _launchDirection = (transform.up + _randomSpread).normalized;

        if (_launchDirection.y < 0) _launchDirection.y *= -1;

        float _launchForce = UnityEngine.Random.Range(minForce, maxForce);

        _popcornRb.AddForce(_launchDirection * _launchForce, ForceMode2D.Impulse);

        Destroy(_popcorn, destroyInterval);
    }
}
