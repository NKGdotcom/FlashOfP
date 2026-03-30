using System;
using UnityEngine;

/// <summary>
/// 私は言葉ですインタフェース
/// </summary>
public interface IWord
{
    void WordEffect();  //Wordの効果
    void ResetWord(); //再読み込みした際にリセット

    event Action WordComplete; //言葉が完成
}