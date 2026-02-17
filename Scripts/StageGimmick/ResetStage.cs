using UnityEngine;
//爆発系を全て元に戻す
public class ResetStage : MonoBehaviour
{
    [SerializeField] private ExplosionItem[] explosionObject;
    private void OnEnable()
    {
        ResetExplosionObject();
    }

    //爆発で消えたオブジェクトを元に戻す
    public void ResetExplosionObject()
    {
        foreach(var _object in explosionObject)
        {
            _object.gameObject.SetActive(true);
        }
    }
}
