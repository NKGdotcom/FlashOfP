using UnityEngine;

/// <summary>
/// プレイヤーの各種能力コンポーネントを管理し、仲介役
/// 各動作はコンポーネントに設定
/// </summary>
public class PlayerController : MonoBehaviour
{
    [Header("プレイヤーのパラメータ設定")]
    [SerializeField] private PlayerData playerData;

    //状態フラグ
    public bool IsFlip { get; private set; }
    public bool IsExplosion { get; private set; }

    [Header("プレイヤーの能力設定")]
    [SerializeField] private PlayerMovement playerMovement;
    [SerializeField] private PlayerJump playerJump;
    [SerializeField] private PlayerRbMover playerRbMover;
    [SerializeField] private PlayerFlip playerFlip;
    [SerializeField] private PlayerPopcorn playerPopcorn;
    [SerializeField] private PlayerExplosion playerExplosion;
    [SerializeField] private PlayerUp playerUp;

    private void Awake()
    {
        if(playerData == null) { Debug.LogError("playerDataが参照されていません"); return; }
        if(playerMovement == null) { Debug.LogError("playerMovementが参照されていません"); return; }
        if(playerJump == null) { Debug.LogError("playerJumpが参照されていません"); return; }
        if(playerRbMover == null) { Debug.LogError("playerRbMoverが参照されていません"); return; }
        if(playerFlip == null) { Debug.LogError("playerFlipが参照されていません"); return; }
        if(playerPopcorn == null) { Debug.LogError("playerPopcornが参照されていません"); return; }
        if(playerExplosion == null) { Debug.LogError("playerExplosionが参照されていません"); return; }
        if(playerUp == null) { Debug.LogError("playerUpが参照されていません"); return; }

        //コンポーネント初期化
        playerMovement.SetParameter(playerData);
        playerJump.SetParameter(playerData);
        playerRbMover.SetUp();
        playerPopcorn.SetParameter(playerData);
        playerExplosion.SetUp(playerData);
        playerUp.SetUp(playerData);
    }

    void Update()
    {
        if(Input.GetMouseButtonDown(0) && GameState.Instance.IsGame())
        {
            playerMovement.Movement();
        }
    }

    private void FixedUpdate()
    {
        playerUp.Floating();
    }

    private void OnDisable()
    {
        //非表示になったら停止をさせる
        playerRbMover.ForceZero();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.TryGetComponent<ExplosionItem>(out var _item))
        {
            playerExplosion.Explosion(IsFlip, collision);
        }
    }

    /// <summary>
    /// プレイヤーの全能力を初期状態にリセットする
    /// </summary>
    public void PlayerResetAbility()
    {
        playerFlip.RestoreGravity();
        playerPopcorn.ResetPopcorn();
        playerExplosion.ResetExplosion();
        playerUp.ResetUp();
        playerJump.ResetJump();
    }
}
