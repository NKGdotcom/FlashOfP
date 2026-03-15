using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// 言葉の処理などをリセットさせるクラス
/// </summary>
public class RetryWord : MonoBehaviour
{
    [SerializeField] private List<GameObject> stageWords;
    private List<IWord> wordsList = new List<IWord>();
    void Awake()
    {
        if (stageWords == null) return;

        foreach (var _word in stageWords)
        {
            IWord _wordInterface = _word.GetComponent<IWord>();
            if (_wordInterface != null)
            {
                wordsList.Add(_wordInterface);
            }
        }
    }
    /// <summary>
    /// 言葉をすべてリセット
    /// </summary>
    public void WordAllReset()
    {
        foreach (var _word in wordsList)
        {
            _word.ResetWord();
        }
    }
}
