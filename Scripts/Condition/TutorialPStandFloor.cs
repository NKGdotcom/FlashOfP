using UnityEngine;
/// <summary>
/// 1つ目のチュートリアルでプレイヤーの文字を完成させた際に床に触れたら
/// </summary>
public class TutorialPStandFloor : BaseCondition
{
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.TryGetComponent<PlayerController>(out var _player))
        {
            //条件となるトリガーをtrueにする
            isFinish = true;
        }
    }

    /// <summary>
    /// ステージをリセットする際に呼び出し、初期化
    /// </summary>
    public override void ResetCondition()
    {
        isFinish = false;
    }
}
