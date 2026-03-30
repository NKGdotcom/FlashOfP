using System;
using System.Collections;
using UnityEngine;

//playerWordをオブジェクトとして追加(予定ではtutorialのみ)
public class WordPlayer : BaseWord
{
    [SerializeField] private Animator unionAnimator;
    [SerializeField] private GameObject p;
    private const string UNION_STRING = "CompleteWord";

    //アニメーションを再生
    public override void WordEffect(GameObject _word)
    {
        if(unionAnimator == null) { Debug.LogWarning("アニメーターが接続されていません"); return; }

        unionAnimator.SetTrigger(UNION_STRING);
        FinishActionEvent(); //OneTutorialStoryに伝える
        p.transform.parent = null;
    }
    //アニメーションで関数呼び出し
    private void FinishAnimation() 
    {
        p.SetActive(true);
    }
}
