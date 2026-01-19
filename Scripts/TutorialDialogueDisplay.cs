using System.Collections;
using TMPro;
using UnityEngine;


public class TutorialDialogueDisplay : MonoBehaviour
{
    public static TutorialDialogueDisplay Instance {  get; private set; }

    [SerializeField] private TextMeshProUGUI tutorialDialogueText;
    [SerializeField] private GameObject dialogueUI; //テキストと黒い背景が含まれる
    [SerializeField] private float displaySpeed = 0.05f;
    private string onceDialogue;

    private Coroutine typingCoroutine;

    private void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
        }

        OnInitialized();

        HiddenUI();
    }
 
    //初期化処理
    private void OnInitialized()
    {
        if(tutorialDialogueText == null)
        {
            Debug.LogWarning(" tutorialDialogueTextがnullです");
        }
        if(dialogueUI == null)
        {
            Debug.LogWarning("dialogueUIがnullです");
        }
    }

    //UIを非表示
    public void HiddenUI()
    {
        dialogueUI.SetActive(false);
    }

    //UIの表示
    public void ShowUI()
    {
        dialogueUI.SetActive(true);
    }

    //チュートリアルのダイアログを出す準備
    public void SetDialogueString(string _oneDialogue)
    {
        onceDialogue = "";
        onceDialogue = _oneDialogue;
        PrintDialogue();
    }

    //コルーチンを準備
    private void PrintDialogue()
    {
        if (typingCoroutine != null)
        {
            StopCoroutine(typingCoroutine);
        }

        typingCoroutine = StartCoroutine(TypeSentence());
    }

    //文字を出力
    private IEnumerator TypeSentence()
    {
        tutorialDialogueText.text = "";

        foreach (var _letter in onceDialogue)
        {
            tutorialDialogueText.text += _letter;
            yield return new WaitForSeconds(displaySpeed);
        }
    }
}
