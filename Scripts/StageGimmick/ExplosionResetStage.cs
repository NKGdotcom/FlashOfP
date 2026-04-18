using UnityEngine;
/// <summary>
/// Explosionで爆発したステージを元に戻す
/// </summary>
public class ExplosionResetStage : MonoBehaviour
{
    [Header("コンポーネント参照")]
    [Tooltip("爆発が可能なオブジェクト")]
    [SerializeField] private ExplosionItem[] explosionObject;

    private void Awake()
    {
        if(explosionObject == null) { Debug.LogError("explosionObjectが参照されていません"); return; }
    }

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
    /// explosionObjectのアクティブ数を確認
    /// </summary>
    /// <returns></returns>
    public int GetActiveExplosionCount()
    {
        int _activeCount = 0;
        foreach (var _object in explosionObject)
        {
            if (_object.gameObject.activeSelf)
            {
                _activeCount++;
            }
        }
        return _activeCount;
    }
}

