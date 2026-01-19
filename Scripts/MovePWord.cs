using System;
using TMPro;
using UnityEngine;

//Pの文字を視覚的に動かす用(離したら元の位置に戻る)
public class MovePWord : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI textP; //移動しないほうのテキスト

    [SerializeField] private Color colorOnTheMove; //移動中に表示する色
    [SerializeField] private Color colorOffTheMove; //移動していないときの色

    private Vector3 touchPoint; //オブジェクトの中心からどれだけ離れた場所を触ったのか
    private Vector3 originPoint; //言葉を持つ前の位置

    private bool isOnWord = false; //言葉の上にテキストが置いてあるか
    
    private IWord iword; //使うIWordを選択

    public event Action MoveAction; //ドラッグ中は動けないようにする
    public event Action ReleaseAction; //離したら言葉の効果を発動

    void Start()
    {
        OnInitialize();
    }

    // Update is called once per frame
    void Update()
    {
       
    }

    //初期処理
    private void OnInitialize()
    {
        originPoint = transform.localPosition;

        if (textP != null)
        {
            textP.color = colorOffTheMove;
        }
        else
        {
            Debug.LogWarning("textPが設定されていません");
        }
    }

    //Pの文字を視覚的に移動させる準備(色を変える、現在位置を保存)
    private void SetVisibility()
    {
        textP.color = colorOnTheMove;
        touchPoint = transform.position - 
            Camera.main.ScreenToWorldPoint(new Vector2
            (Input.mousePosition.x, Input.mousePosition.y));
    }

    //Pの文字を視覚的に移動
    private void MovePVisually()
    {
        Vector2 _currentMousePoint = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
        Vector3 _currentPosition = Camera.main.ScreenToWorldPoint(_currentMousePoint) + touchPoint;
        transform.position = _currentPosition;
    }

    //Pの文字を視覚的に元の位置に戻る
    private void BackP()
    {
        textP.color = colorOffTheMove;
        transform.localPosition = originPoint;
    }

    private void OnMouseDown()
    {
        SetVisibility();
        MoveAction?.Invoke();
    }

    private void OnMouseDrag()
    {
        MovePVisually();
    }

    private void OnMouseUp()
    {
        if (isOnWord)
        {
            SoundManager.Instance.PlaySE(SESource.getword);
            iword.WordEffect(transform.parent.gameObject); //言葉の処理を実行
        }
        BackP();
        ReleaseAction?.Invoke();
    }



    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.TryGetComponent<IWord>(out var wordObj))
        {
            isOnWord = true;
            iword = wordObj;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.TryGetComponent<IWord>(out var wordObj))
        {
            isOnWord = false;
            iword = null;
        }
    }
}
