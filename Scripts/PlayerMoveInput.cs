using UnityEngine;

public class PlayerMoveInput : MonoBehaviour
{
    [SerializeField] private PlayerP player;
    [SerializeField] private MovePWord[] movePWords;

    [SerializeField] private GameObject clickEffect;
    private float zClickPos = 10; //カメラに合わせ、エフェクトが見えるようにする
    public bool IsTutorial { get; set; }

    private bool isDragging = false;

    private bool canMove => !isDragging && !IsTutorial;

    private Step stepManager;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        OnInitialize();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            ShowTapEffect();

            player.MoveToClickDirection(canMove); //移動
        }

        if(canMove && Input.GetKeyDown(KeyCode.Escape))
        {
            stepManager = FindFirstObjectByType<Step>();
            stepManager.ReturnToStageSelect();
            player.gameObject.SetActive(false);
        }
    }


    private void OnInitialize()
    {
        if(player == null)
        {
            Debug.LogWarning("player(PlayerPスクリプト)が設定されていません");
        }

        if(movePWords != null)
        {
            foreach(var movePWord in movePWords)
            {
                movePWord.MoveAction += OnDragStart;
                movePWord.ReleaseAction += OnDragEnd;
            }
        }
        else
        {
            Debug.LogWarning("movePWord(MovePwordスクリプト)が設定されていません");
        }
    }

    //タップエフェクトを見せる
    private void ShowTapEffect()
    {
        clickEffect.SetActive(false); //始める前に前にクリックしていたエフェクトをfalse
        Vector3 _clickPos = Input.mousePosition;
        _clickPos = Camera.main.ScreenToWorldPoint(_clickPos);
        _clickPos.z = zClickPos;
        clickEffect.transform.position = _clickPos;
        clickEffect.SetActive(true);
    }
    //ドラッグ開始
    private void OnDragStart()
    {
        isDragging = true;
    }

    //ドラッグ終了
    private void OnDragEnd()
    {
        isDragging = false;
    }
}
