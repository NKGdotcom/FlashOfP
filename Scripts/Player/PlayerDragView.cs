using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UIElements;

/// <summary>
/// Pのドラッグ時の見た目(移動、色変更、親子関係)を処理
/// </summary>
public class PlayerDragView : MonoBehaviour
{
    [Header("テキスト参照")]
    [Tooltip("移動しない元の位置に残るテキスト（色変更用）")]
    [SerializeField] private TextMeshProUGUI playerPTMP;
    [Header("色の設定")]
    [Tooltip("移動中に表示する色")]
    [SerializeField] private Color colorOnTheMove;
    [Tooltip("移動していない時の色")]
    [SerializeField] private Color colorOffTheMove;

    private Camera mainCamera;
    private Transform originalParent;

    private GameObject pSpriteObj;
    private Vector3 originLocalPos;
    private Quaternion originLocalRot;
    private Vector3 dragOffset;

    private void Awake()
    {
        if(playerPTMP == null) { Debug.LogError("playerPTMPが参照されていません"); return; }

        mainCamera = Camera.main;

        //初期状態の保存
        pSpriteObj = this.gameObject;
        originalParent = transform.parent;
        originLocalPos = transform.localPosition;
        originLocalRot = transform.localRotation;
    }

    /// <summary>
    /// Pの文字を視覚的に移動する準備
    /// </summary>
    public void SetVisibility(PointerEventData eventData)
    {
        playerPTMP.color = colorOnTheMove;

        //親子関係を削除
        transform.SetParent(null);

        //マウス位置からオフセット(掴んだ位置のズレ)を計算
        Vector3 worldMousePos = GetWorldMousePosition(eventData.position);
        worldMousePos.z = pSpriteObj.transform.position.z;

        dragOffset = pSpriteObj.transform.position - worldMousePos;
    }

    /// <summary>
    /// Pの文字を視覚的に移動
    /// </summary>
    public void MovePSpriteObj(PointerEventData eventData)
    {
        Vector3 _worldMousePos = GetWorldMousePosition(eventData.position);
        _worldMousePos.z = pSpriteObj.transform.position.z;

        pSpriteObj.transform.position = _worldMousePos + dragOffset;
    }

    /// <summary>
    /// Pの文字を視覚的に元の位置に戻す
    /// </summary>
    public void BackPosVisibility()
    {
        transform.SetParent(originalParent);
        playerPTMP.color = colorOffTheMove;

        transform.localPosition = originLocalPos;
        transform.localRotation = originLocalRot;
    }

    /// <summary>
    /// スクリーン座標をワールド座標に変換する
    /// </summary>
    private Vector3 GetWorldMousePosition(Vector2 _screenPosition)
    {
        //Z軸をカメラのNearクリップ面に合わせる
        Vector3 _screenPosWithZ = new Vector2(_screenPosition.x, _screenPosition.y);
        Vector3 _worldPos = mainCamera.ScreenToWorldPoint(_screenPosWithZ);

        _worldPos.z = transform.position.z;

        return _worldPos;
    }
}
