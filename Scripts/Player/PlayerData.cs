using UnityEngine;

/// <summary>
/// プレイヤーの能力をまとめたデータ
/// </summary>
[CreateAssetMenu(fileName ="PlayerData", menuName = "ScriptableObjects/PlayerData")]
public class PlayerData : ScriptableObject
{
    [Header("移動・ジャンプ")]
    [Tooltip("プレイヤーの基本移動スピード (m/s)")]
    [field: SerializeField] public float MoveSpeed { get; private set; }
    [Tooltip("ジャンプの初速パワー")]
    [field: SerializeField] public float JumpPower { get; private set; }
    [Tooltip("連続してジャンプする際の最低クールタイム（秒）")]
    [field: SerializeField] public float JumpInterval { get; private set; }
    [Header("ポップコーン能力")]
    [Tooltip("発射するポップコーンのプレハブ")]
    [field: SerializeField] public GameObject PopcornPrefab { get; private set; }
    [Tooltip("発射時の最小パワー")]
    [field: SerializeField] public float MinPower { get; private set; }
    [Tooltip("発射時の最大パワー")]
    [field: SerializeField] public float MaxPower { get; private set; }
    [Tooltip("ポップコーンを連射する間隔（秒）")]
    [field: SerializeField] public float ShotInterval { get; private set; }
    [Range(0,1)]
    [Tooltip("発射のブレ具合（0で真っ直ぐ、1で広範囲）")]
    [field: SerializeField] public float SpreadAmount { get; private set; } = 0.2f;
    [Tooltip("発射後、消滅するまでの時間（秒）")]
    [field: SerializeField] public float DestroyInterval { get; private set; } = 5;
    [Header("爆発能力")]
    [Tooltip("爆発時に周囲を吹き飛ばす力")]
    [field: SerializeField] public float ExplosionPower { get; private set; } = 8;
    [Header("浮遊能力")]
    [Tooltip("上方向へ浮かび上がるスピード")]
    [field: SerializeField] public float UpSpeed { get; private set; } = 1f;
}
