using UnityEngine;

/// <summary>
/// プレイヤーの移動処理
/// </summary>
public class PlayerMovement : MonoBehaviour
{
    [Header("コンポーネント参照")]
    [Tooltip("物理移動を管理するコンポーネント")]
    [SerializeField] private PlayerRbMover playerRbMover;
    
    //移動パラメータ
    private float moveSpeed;
    private float deadZone = 0.1f;

    private Camera mainCamera;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Awake()
    {
        if(playerRbMover == null) { Debug.LogError("playerRbMoverが参照されていません"); return; }
        
        mainCamera = Camera.main;
    }

    /// <summary>
    /// PlayerDataからパラメータを設定
    /// </summary>
    public void SetParameter(PlayerData _data)
    {
        moveSpeed = _data.MoveSpeed;
    }

    /// <summary>
    /// マウスをクリックした方向に移動
    /// </summary>
    public void Movement()
    {
        //マウスの画面座標をワールド座標に変換
        Vector3 _mousePos = mainCamera.ScreenToWorldPoint(Input.mousePosition);
        float _directionX = _mousePos.x - transform.position.x;

        //プレイヤーの真ん中をクリックした場合は無視
        if (Mathf.Abs(_directionX) > deadZone)
        {
            // 左(-1)か右(1)かを判定
            float signX = Mathf.Sign(_directionX);
            playerRbMover.MovementRb(signX * moveSpeed);
        }
    }
}
