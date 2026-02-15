using System;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class BaseStageSelect : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler,IPointerClickHandler
{
    [SerializeField] protected Clear clear;

    [SerializeField] protected TextMeshProUGUI perfectTMP;

    [SerializeField] private GameObject stage; //使用するステージ

    [SerializeField] private Image buttonImage;

    public event Action<GameObject> OnClick;

    public virtual void OnEnable()
    {

    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        buttonImage.color = Color.gray;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        buttonImage.color = Color.white;
    }

    public virtual void OnPointerClick(PointerEventData eventData)
    {
        SoundManager.Instance.PlaySE(SESource.stageselect);
        buttonImage.color = Color.white;
        OnClick?.Invoke(stage);
    }
}
