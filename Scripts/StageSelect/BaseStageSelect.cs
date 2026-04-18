using System;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

/// <summary>
/// ステージ選択ボタンの基本クラス
/// </summary>
public class BaseStageSelect : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
{
    [Header("ステージデータ設定")]
    [Tooltip("全ステージのクリア状況を管理するデータ")]
    [SerializeField] protected Clear clearData;
    [Tooltip("このボタンが担当するステージの番号")]
    [SerializeField] private int stageIndex;
    [Tooltip("選択された際に次のステップへ渡すステージのGameObject")]
    [SerializeField] private GameObject stage;

    [Header("UIコンポーネント参照")]
    [Tooltip("クリア状態を表示するテキスト")]
    [SerializeField] protected TextMeshProUGUI perfectTMP;
    [Tooltip("ボタンの画像(ホバー時の色変更")]
    [SerializeField] private Image buttonImage;

     //ボタンがクリックされた時に発火し、自信が担当するステージを外部へ渡す
    public event Action<GameObject> OnClick;

    private void Awake()
    {
        if (clearData == null) { Debug.LogError("clearDataが参照されていません"); return; }
        if (stage == null) { Debug.LogError("stageが参照されていません"); }
        if (perfectTMP == null) { Debug.LogError("perfectTMPが参照されていません"); return; }
        if (buttonImage == null) { Debug.LogError("buttonImageが参照されていません"); return; }
    }

    public virtual void OnEnable()
    {
        var _currentStageData = clearData.stageDataList[stageIndex];

        if (_currentStageData.isClear)
        {
            //クリア済みならテキストの表示
            perfectTMP.enabled = true;
            
            //パーフェクトクリアなら色を黄色にする
            if (clearData.stageDataList[stageIndex].isPerfectClear)
            {
                perfectTMP.color = Color.yellow;
            }
        }
        else
        {
            perfectTMP.enabled = false;
        }
    }

    //マウス操作の検知
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
        //色を通常状態に戻す
        SoundManager.Instance.PlaySE(SESource.STAGE_SELECT);
        buttonImage.color = Color.white;

        //ステージのオブジェクトを渡す
        OnClick?.Invoke(stage);
    }
}