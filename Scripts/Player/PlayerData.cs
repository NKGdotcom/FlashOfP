using UnityEngine;

/// <summary>
/// プレイヤーの能力をまとめたデータ
/// </summary>
[CreateAssetMenu(fileName ="PlayerData", menuName = "ScriptableObjects/PlayerData")]
public class PlayerData : ScriptableObject
{
    //---プレイヤーの移動---
    [Header("プレイヤーの移動スピード")]
    public float moveSpeed = 5;
    //---プレイヤーのジャンプ---
    [Header("プレイヤーのジャンプ力")]
    public float jumpPower = 8;
    public float jumpInterval = 1;
    //---プレイヤーのポップコーン---
    [Header("プレイヤーのポップコーン飛ばす力")]
    public GameObject popcornPrefab;
    public float minPower = 5;
    public float maxPower = 8;
    public float shotInterval = 1;
    [Range(0,1)]
    public float spreadAmount = 0.2f;
    public float destroyInterval = 5;
    //---プレイヤーの爆発---
    [Header("プレイヤーが爆発する力")]
    public float explosionPower = 8;
    //---プレイヤーが浮かぶ---
    [Header("プレイヤーが浮かぶ力")]
    public float upSpeed = 1f;
}
