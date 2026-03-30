using UnityEngine;
/// <summary>
/// チュートリアルの床に触れたら
/// </summary>
public class TutorialPStandFloor : BaseCondition
{
    //プレイヤーが床に触れたらチェック
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.TryGetComponent<PlayerController>(out var _player))
        {
            isFinish = true;
        }
    }
    public override void ResetCondition()
    {
        isFinish = false;
    }
}
