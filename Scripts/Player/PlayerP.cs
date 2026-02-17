using Cysharp.Threading.Tasks;
using System.Threading;
using UnityEngine;

//プレイヤー自身(移動や能力が使えるようになる)
public class PlayerP : MonoBehaviour
{
    //baseの能力
    [SerializeField] private float moveSpeed;

    //能力
    //ジャンプ
    [SerializeField] private WordJump[] wordJumps;
    [SerializeField] private PlayerJump playerJump;
    //フリップ(重力を逆に)
    [SerializeField] private WordFlip[] wordFlips;
    [SerializeField] private PlayerFlip playerFlip;
    //ポップコーン(物を投げる)
    [SerializeField] private WordPopcorn[] wordPopcorns;
    [SerializeField] private PlayerPopcorn playerPopcorn;
    //爆発
    [SerializeField] private WordExplosion[] wordExplosions;
    [SerializeField] private PlayerExplosion playerExplosion;
    //上に浮遊
    [SerializeField] private WordUp[] wordUps;
    [SerializeField] private PlayerUp playerUp;

    public Rigidbody2D PlayerRb { get; private set; }
    private Camera mainCamera;
    private CancellationTokenSource abilityCts;

    public int ShotCount { get; set; } = 0;
    public int ExplosionCount { get; set; } = 0;
    public bool IsJump { get; private set; }
    public bool IsFlip { get; private set; }
    public bool IsExplosion { get; private set; }
    public bool IsPopcorn { get; private set; }
    public bool IsUp { get; private set; }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Awake()
    {
        if(PlayerRb == null)
        {
            PlayerRb = GetComponent<Rigidbody2D>();
        }
        mainCamera = Camera.main;

        OnInitialize();
    }
    //初期処理
    private void OnInitialize()
    {
        if (wordJumps != null)
        {
            foreach (var wordJump in wordJumps)
            {
                wordJump.FinishAction += SetJumpAbility;
            }
            IsJump = false;
        }
        
        if (wordFlips != null)
        {
            foreach (var wordFlip in wordFlips)
            {
                wordFlip.FinishAction += SetFlipAbility;
            }
            IsFlip = false;
        }

        if (wordPopcorns != null)
        {
            foreach (var wordPopcorn in wordPopcorns)
            {
                wordPopcorn.FinishAction += SetPopcornAbility;
            }
            IsPopcorn = false;
        }

        if (wordExplosions != null)
        {
            foreach (var wordExplosion in wordExplosions)
            {
                wordExplosion.FinishAction += SetExplosionAbility;
            }
            IsExplosion = false;
        }

        if (wordUps != null)
        {
            foreach (var wordUp in wordUps)
            {
                wordUp.FinishAction += SetUpAbility;
            }
            IsUp = false;
        }

        NullCheck();
    }

    private void NullCheck()
    {
        if(wordJumps == null) { Debug.LogWarning("wordJump(WordJumpスクリプト)が設定されていません"); return; }
        if (playerJump == null) { Debug.LogWarning("playerJump(PlayerJumpスクリプト)が設定されていません"); return; }
        if(wordFlips == null) { Debug.LogWarning("wordFlip(WordFlipスクリプト)が設定されていません"); return; }
        if(playerFlip == null) { Debug.LogWarning("playerFlip(PlayerFlipスクリプト)が設定されていません"); return; }
        if(wordPopcorns == null) { Debug.LogWarning("wordPopcorn(WordPopcornスクリプト)が設定されていません"); return; }
        if(playerPopcorn == null) { Debug.LogWarning("playerPopcorn(PlayerPopcornスクリプト)が設定されていません"); return; }
        if(wordExplosions == null) { Debug.LogWarning("wordExplosion(WordExplosionスクリプト)が設定されていません"); return; }
        if (playerExplosion == null) { Debug.LogWarning("playerExplosion(PlayerExplosionスクリプト)が設定されていません"); return; }
        if (wordUps == null) { Debug.LogWarning("wordUp(WordUpスクリプト)が設定されていません"); return; }
        if (playerUp == null) { Debug.LogWarning("playerUp(PlayerUpスクリプト)が設定されていません"); return; }
    }

    private void OnEnable()
    {
        ResetVal();

        if (abilityCts != null)
        {
            abilityCts.Cancel();
            abilityCts.Dispose();
        }
        abilityCts = new CancellationTokenSource();

        StartAbilityInterval(abilityCts.Token);
    }

    //変数をリセット
    private void ResetVal()
    {
        ShotCount = 0;
        ExplosionCount = 0;
        IsJump = false;

        IsFlip = false;
        playerFlip.ResetGravity(PlayerRb);

        IsPopcorn = false;
        IsExplosion = false;
        IsUp = false;
    }

    //インターバルで何かするものをここに設定
    private void StartAbilityInterval(CancellationToken _token)
    {
        if (playerJump != null) { playerJump.AutoJumpLoopAsync(this, _token).Forget(); } //一定期間でジャンプ
        if (playerPopcorn != null) { playerPopcorn.AutoShotPopcornAsync(this, _token).Forget(); } //一定期間でポップコーンを投げる
    }

    void FixedUpdate()
    {
        playerUp.Floating(this);
    }

    public void MoveToClickDirection(bool _canMove)
    {
        if (!_canMove) return; //動かしている最中は移動しない

        Vector3 _mousePos = mainCamera.ScreenToWorldPoint(Input.mousePosition);
        float _directionX = _mousePos.x - transform.position.x;
        float _signX = Mathf.Sign(_directionX);
        PlayerRb.linearVelocity = new Vector2(_signX * moveSpeed, PlayerRb.linearVelocity.y);
    }

    private void SetJumpAbility()
    {
        IsJump = true;
    }

    private void SetFlipAbility()
    {
        IsFlip = true;
        playerFlip.ChangeGravity(PlayerRb);
    }

    private void SetPopcornAbility()
    {
        IsPopcorn = true;
    }

    private void SetExplosionAbility()
    {
        IsExplosion = true;
    }

    private void SetUpAbility()
    {
        IsUp = true;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.TryGetComponent<ExplosionItem>(out var explosionItem))
        {
            if (IsExplosion)
            {
                playerExplosion.Explosion(this);
                collision.gameObject.SetActive(false);

            }
        }
    }

    private void OnDisable()
    {
        if (abilityCts != null)
        {
            abilityCts.Cancel();
            abilityCts.Dispose();
            abilityCts = null; // 使い終わったら空にしておく
        }
    }
}
