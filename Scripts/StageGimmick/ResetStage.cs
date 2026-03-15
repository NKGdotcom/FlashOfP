using UnityEngine;
/// <summary>
/// 爆発系を全て元に戻す
/// </summary>
public class ResetStage : MonoBehaviour
{
    [SerializeField] private ExplosionItem[] explosionObject;

    private void OnEnable()
    {
        ResetExplosionObject();
    }

    /// <summary>
    /// 爆発で消えたオブジェクトを元に戻す
    /// </summary>
    public void ResetExplosionObject()
    {
        foreach (var _object in explosionObject)
        {
            _object.gameObject.SetActive(true);
        }
    }
    /// <summary>
    /// アクティブ数を確認
    /// </summary>
    /// <returns></returns>
    public int GetActiveExplosionCount()
    {
        int activeCount = 0;
        foreach (var _object in explosionObject)
        {
            if (_object.gameObject.activeSelf)
            {
                activeCount++;
            }
        }
        return activeCount;
    }
}

