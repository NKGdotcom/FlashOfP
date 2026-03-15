using UnityEngine;

/// <summary>
/// プレイヤーの脳の部分
/// </summary>
public class PlayerController : MonoBehaviour
{
    //---プレイヤーのパラメータ---
    [SerializeField] private PlayerData playerData;
    //---移動---
    [SerializeField] private PlayerMovement playerMovement;
    //---ジャンプ挙動---
    [SerializeField] private PlayerJump playerJump;
    //---物理挙動---
    [SerializeField] private PlayerRbMover playerRbMover;
    //---逆重力---
    [SerializeField] private PlayerFlip playerFlip;
    public bool IsFlip { get; private set; }
    //---ポップコーン挙動
    [SerializeField] private PlayerPopcorn playerPopcorn;
    //---爆発挙動---
    [SerializeField] private PlayerExplosion playerExplosion;
    public bool IsExplosion { get; private set; }
    //---上に浮かぶ挙動---
    [SerializeField] private PlayerUp playerUp;
    private void Awake()
    {
        if(playerData == null) { Debug.LogError("playerDataが参照されていません"); return; }
        if(playerMovement == null) { Debug.LogError("playerMovementが参照されていません"); return; }
        if (playerRbMover == null) { Debug.LogError("playerRbMoverが参照されていません"); return; }
        if(playerFlip == null) { Debug.LogError("playerFlipが参照されていません"); return; }
        if(playerPopcorn == null) { Debug.LogError("playerPopcornが参照されていません"); return; }
        if(playerUp == null) { Debug.LogError("playerUpが参照されていません"); return; }
        playerMovement.SetParameter(playerData);
        playerJump.SetParameter(playerData);
        playerRbMover.SetUp();
        playerPopcorn.SetParameter(playerData);
    }
    // Update is called once per frame
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
        playerRbMover.ForceZero();
    }
    /// <summary>
    /// プレイヤーの能力をリセット
    /// </summary>
    public void PlayerResetAbility()
    {
        playerFlip.RestoreGravity();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.TryGetComponent<ExplosionItem>(out var _item))
        {
            playerExplosion.Explosion(this, collision);
        }
    }
}
