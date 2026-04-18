using System;
using UnityEngine;

/// <summary>
/// 言葉の効果発動やリセットを定義するインタフェース
/// </summary>
public interface IWord
{
    void WordEffect();  //Wordの効果
    void ResetWord(); //再読み込みした際にリセット

    event Action WordComplete; //言葉が完成
}