using System;
using UnityEngine;

public class BaseWord : MonoBehaviour, IWord
{
    private RectTransform rectTransform;
    private Vector2 originPos;

    public event Action FinishAction;

    private void Awake()
    {
        rectTransform = GetComponent<RectTransform>();
        if(rectTransform != null)
        {
            originPos = rectTransform.anchoredPosition;
        }
    }

    private void OnEnable()
    {
        rectTransform.anchoredPosition = originPos;
    }

    public virtual void WordEffect(GameObject _word)
    {

    }

    //Œ¾—t‚ðŽg‚¤
    public void FinishActionEvent()
    {
        FinishAction?.Invoke();
    }
}
