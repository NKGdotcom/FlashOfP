using UnityEngine;

public class Stage4Select : BaseStageSelect
{
    public override void OnEnable()
    {
        base.OnEnable();

        if (clear.Stage4Clear)
        {
            perfectTMP.enabled = true;
            if (clear.Stage4PerfectClear)
            {
                perfectTMP.color = Color.yellow;
            }
        }
    }
}