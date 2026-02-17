using UnityEngine;

public class TutorialTextConplete : ConditionBase
{
    private bool isComplete = false;


    //チュートリアルのテキストが終わったか判断
    public override bool CheckCondition()
    {
        return isComplete;
    }
}
