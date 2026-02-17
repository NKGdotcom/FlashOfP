using UnityEngine;
using static UnityEditor.Experimental.GraphView.GraphView;

public class Stage3 : MonoBehaviour
{
    [SerializeField] private Clear clear;
    private int perfectScoreShotNum = 10;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.TryGetComponent<PlayerP>(out var _player))
        {
            if (IsShortPopcornCnt(_player))
            {
                clear.Stage3PerfectClear = true;
            }
            clear.Stage3Clear = true;
        }
    }

    //ポップコーンの放った回数が少ない場合
    private bool IsShortPopcornCnt(PlayerP _player)
    {
        return _player.ShotCount <= perfectScoreShotNum;
    }
}
