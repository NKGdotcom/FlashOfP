using UnityEngine;
using UnityEngine.EventSystems;

/// <summary>
/// Pのドラッグをここで実行
/// </summary>
public class PlayerDragController : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField] private PlayerDragView playerDragView;
    private bool isOnWord = false;

    private IWord wordUI;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Awake()
    {
        if(playerDragView == null) { Debug.LogError("playerDragViewが参照されていません"); return; }
    }
    /// <summary>
    /// ドラッグ開始
    /// </summary>
    /// <param name="eventData"></param>
    public void OnBeginDrag(PointerEventData eventData)
    {
        playerDragView.SetVisibility(eventData);
    }
    /// <summary>
    /// ドラッグ中
    /// </summary>
    /// <param name="eventData"></param>
    public void OnDrag(PointerEventData eventData)
    {
        if (GameState.Instance.IsExplain()) return;

        playerDragView.MovePSpriteObj(eventData);
    }
    /// <summary>
    /// ドラッグが終了
    /// </summary>
    /// <param name="eventData"></param>
    public void OnEndDrag(PointerEventData eventData)
    {
        if (GameState.Instance.IsExplain()) return;

        GameState.Instance.SetState(State.GAME_ACT);

        if (isOnWord && wordUI != null)
        {
            wordUI.WordEffect();
            wordUI = null;
            isOnWord = false;
        }

        playerDragView.BackPosVisibility();
    }
    public void OnPointerEnter(PointerEventData eventData)
    {
        if (GameState.Instance.IsExplain()) return;

        GameState.Instance.SetState(State.DRAG_ACT);
    }
    public void OnPointerExit(PointerEventData eventData)
    {
        if (GameState.Instance.IsExplain()) return;

        GameState.Instance.SetState(State.GAME_ACT);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (GameState.Instance.IsExplain()) return;

        if (collision.TryGetComponent<IWord>(out wordUI))
        {
            Debug.Log(wordUI);
            isOnWord = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (GameState.Instance.IsExplain()) return;

        if (collision.TryGetComponent<IWord>(out wordUI))
        {
            wordUI = null;
            isOnWord = false;
        }
    }
}
