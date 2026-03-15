using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UIElements;

/// <summary>
/// ドラッグの見た目の部分を処理
/// </summary>
public class PlayerDragView : MonoBehaviour
{
    //---移動する方のテキスト---
    private GameObject pSpriteObj;
    //---移動しないほうのテキストの色変更---
    [SerializeField] private TextMeshProUGUI playerPTMP;
    [SerializeField] private Color colorOnTheMove; //移動中に表示する色
    [SerializeField] private Color colorOffTheMove; //移動していないときの色

    private Vector3 originPos;
    private Vector3 offset;
    private Quaternion originRtation = new Quaternion(0, 0, 0, 0); 

    private Transform originalParent;

    private void Awake()
    {
        if(playerPTMP == null) { Debug.LogError("playerPTMPが参照されていません"); return; }

        pSpriteObj = this.gameObject;
        originPos = transform.localPosition;

        originalParent = transform.parent;
    }

    /// <summary>
    /// Pの文字を視覚的に移動する準備
    /// </summary>
    public void SetVisibility(PointerEventData eventData)
    {
        playerPTMP.color = colorOnTheMove;

        transform.SetParent(null);

        Vector3 worldMousePos = Camera.main.ScreenToWorldPoint(new Vector3(eventData.position.x, eventData.position.y, Camera.main.nearClipPlane));
        worldMousePos.z = pSpriteObj.transform.position.z;

        offset = pSpriteObj.transform.position - worldMousePos;    }
    /// <summary>
    /// Pの文字を視覚的に移動
    /// </summary>
    public void MovePSpriteObj(PointerEventData eventData)
    {
        Vector3 _worldMousePos = Camera.main.ScreenToWorldPoint(new Vector3(eventData.position.x, eventData.position.y, Camera.main.nearClipPlane));
        _worldMousePos.z = pSpriteObj.transform.position.z;

        pSpriteObj.transform.position = _worldMousePos + offset;
    }
    /// <summary>
    /// Pの文字を視覚的に元の位置に戻す
    /// </summary>
    public void BackPosVisibility()
    {
        transform.SetParent(originalParent);
        playerPTMP.color = colorOffTheMove;

        transform.localPosition = originPos;
        transform.localRotation = originRtation;
    }
}
