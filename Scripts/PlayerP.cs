using UnityEngine;

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

    public int ShotCount { get; set; } = 0;
    public int ExplosionCount { get; set; } = 0;
    public bool IsJump { get; private set; }
    public bool IsFlip { get; private set; }
    public bool IsExplosion { get; private set; }
    public bool IsPopcorn { get; private set; }
    public bool IsUp { get; private set; }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        if(PlayerRb == null)
        {
            PlayerRb = GetComponent<Rigidbody2D>();
        }
        mainCamera = Camera.main;

        OnInitialize();
    }

    private void OnEnable()
    {
        ResetVal();

        StartAbilityCoroutine();
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

    //ジャンプ能力が使えるように
    private void SetJumpAbility()
    {
        IsJump = true;
    }

    //重力変更能力が使えるように
    private void SetFlipAbility()
    {
        IsFlip = true;
        playerFlip.GravityChange(PlayerRb);
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

    //初期処理
    private void OnInitialize()
    {
        if (wordJumps != null)
        {
            foreach(var wordJump in wordJumps)
            {
                wordJump.FinishAction += SetJumpAbility;
            }
            IsJump = false;
        }
        else Debug.LogWarning("wordJump(WordJumpスクリプト)が設定されていません");


        if (wordFlips != null)
        {
            foreach (var wordFlip in wordFlips)
            {
                wordFlip.FinishAction += SetFlipAbility;
            }
            IsFlip = false;
        }
        else Debug.LogWarning("wordFlip(WordFlipスクリプト)が設定されていません");

        if (playerFlip == null) Debug.LogWarning("playerFlip(PlayerFlipスクリプト)が設定されていません");

        if (wordPopcorns != null)
        {
            foreach(var wordPopcorn in wordPopcorns)
            {
                wordPopcorn.FinishAction += SetPopcornAbility;
            }
            IsPopcorn = false;
        }
        else Debug.LogWarning("wordPopcorn(WordPopcornスクリプト)が設定されていません");

        if (wordExplosions != null)
        {
            foreach( var wordExplosion in wordExplosions)
            {
                wordExplosion.FinishAction += SetExplosionAbility;
            }
            IsExplosion = false;
        }
        else Debug.LogWarning("wordExplosion(WordExplosionスクリプト)が設定されていません");

        if (playerExplosion == null) Debug.LogWarning("playerExplosion(PlayerExplosionスクリプト)が設定されていません");

        if (wordUps != null)
        {
            foreach(var wordUp in wordUps)
            {
                wordUp.FinishAction += SetUpAbility;
            }
            IsUp = false;
        }
        else Debug.LogWarning("wordUp(WordUpスクリプト)が設定されていません");

        if (playerUp == null) Debug.LogWarning("playerUp(PlayerUpスクリプト)が設定されていません");
    }

    private void StartAbilityCoroutine()
    {
        if (playerJump != null)
        {
            StartCoroutine(playerJump.AutoJumpLoop(this));
        }
        else Debug.LogWarning("playerJump(PlayerJumpスクリプト)が設定されていません");

        if (playerPopcorn != null)
        {
            StartCoroutine(playerPopcorn.AutoShotPopcorn(this));
        }
        else Debug.LogWarning("playerPopcorn(PlayerPopcornスクリプト)が設定されていません");
    }
    //変数を削除
    private void ResetVal()
    {
        ShotCount = 0;
        ExplosionCount = 0;
        IsJump = false;
        IsFlip = false;
        IsPopcorn = false;
        IsExplosion = false;
        IsUp = false;
    }
}
