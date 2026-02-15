using UnityEngine;

public interface IPlayerAbility
{
    void Unlock(); //能力開放
    void OnUpdate(); //毎フレーム処理
    void OnFixedUpdate(); //物理演算の処理
    void OnCollisionEnter(Collider2D collider); //衝突処理
}
