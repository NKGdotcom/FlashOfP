using UnityEngine;
using UnityEngine.EventSystems;

public class Stage1 : MonoBehaviour
{
    [SerializeField] private Clear clear;
    [SerializeField] private float perfectTime = 6;
    private float timer = 0;

    private void Update()
    {
        timer += Time.deltaTime;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.TryGetComponent<PlayerP>(out var _player))
        {
            if (IsFastClear())
            {
                clear.Stage1PerfectClear = true;
            }
            clear.Stage1Clear = true;
        }
    }
    //ŽžŠÔ“à‚ÉƒNƒŠƒA‚µ‚½‚ç
    private bool IsFastClear()
    {
        return perfectTime > timer;
    }
}
