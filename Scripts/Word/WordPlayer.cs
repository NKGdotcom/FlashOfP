using System;
using System.Collections;
using UnityEngine;

//playerWordをオブジェクトとして追加(予定ではtutorialのみ)
public class WordPlayer : MonoBehaviour, IWord
{
    [SerializeField] private Animator unionAnimator;
    [SerializeField] private GameObject p;
    private const string UNION_STRING = "CompleteWord";

    public event Action FinishAction;

    //アニメーションを再生
    public void WordEffect(GameObject _word)
    {
        if(unionAnimator == null)
        {
            Debug.LogWarning("アニメーターが接続されていません");
            return;
        }
        unionAnimator.SetTrigger(UNION_STRING);
        FinishAction?.Invoke(); //OneTutorialStoryに伝える
        p.transform.parent = null;
    }
    private void FinishAnimation() 
    {
        p.SetActive(true);
    }
}
