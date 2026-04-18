using UnityEngine;
using UnityEngine.EventSystems;

/// <summary>
/// Pのドラッグ操作と、単語との接触を統括
/// </summary>
public class PlayerDragController : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler, IPointerEnterHandler, IPointerExitHandler
{
    [Header("コンポーネント参照")]
    [SerializeField] private PlayerDragView playerDragView;
    
    //現在接触している単語(nullなら乗っていない状態)
    private IWord currentWordUI;
    //言葉のUIの上に乗っているかどうか
    private bool isOnWord = false;

    void Awake()
    {
        if(playerDragView == null) { Debug.LogError("playerDragViewが参照されていません"); return; }
    }

    //マウスホバー
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

    //ドラッグ操作
    public void OnBeginDrag(PointerEventData eventData)
    {
        playerDragView.SetVisibility(eventData);
    }

    public void OnDrag(PointerEventData eventData)
    {
        if (GameState.Instance.IsExplain()) return;

        playerDragView.MovePSpriteObj(eventData);
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        if (GameState.Instance.IsExplain()) return;

        GameState.Instance.SetState(State.GAME_ACT);

        //乗っている単語があれば効果を発動し、リセットする
        if (isOnWord && currentWordUI != null)
        {
            currentWordUI.WordEffect();
            currentWordUI = null;
            isOnWord = false;
        }

        playerDragView.BackPosVisibility();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (GameState.Instance.IsExplain()) return;

        if (collision.TryGetComponent<IWord>(out currentWordUI))
        {
            isOnWord = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (GameState.Instance.IsExplain()) return;

        if (collision.TryGetComponent<IWord>(out currentWordUI))
        {
            currentWordUI = null;
            isOnWord = false;
        }
    }
}
