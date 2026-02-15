using UnityEngine;

public class TutorialPStandFloor : ConditionBase
{
    private bool isTouched = false;

    //プレイヤーが床に触れたらチェック
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.TryGetComponent<PlayerP>(out var pObj))
        {
            isTouched = true;
        }
    }
    public override bool CheckCondition()
    {
        return isTouched;
    }
}
