using UnityEngine;

/// <summary>
/// プレイヤーの移動処理
/// </summary>
public class PlayerMovement : MonoBehaviour
{
    //---物理移動---
    [SerializeField] private PlayerRbMover playerRbMover;
    //---移動パラメータ---
    private float moveSpeed;
    private Camera mainCamera;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Awake()
    {
        if(playerRbMover == null) { Debug.LogError("playerRbMoverが参照されていません"); return; }
        mainCamera = Camera.main;
    }
    /// <summary>
    /// パラメーター設定
    /// </summary>
    public void SetParameter(PlayerData _data)
    {
        moveSpeed = _data.moveSpeed;
    }
    /// <summary>
    /// マウス方向に移動
    /// </summary>
    public void Movement()
    {
        Vector3 _mousePos = mainCamera.ScreenToWorldPoint(Input.mousePosition);
        float _directionX = _mousePos.x - transform.position.x;
        float _signX = Mathf.Sign(_directionX);
        playerRbMover.MovementRb(_signX * moveSpeed);
    }
}
