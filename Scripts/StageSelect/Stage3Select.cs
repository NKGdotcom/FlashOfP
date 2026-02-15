using UnityEngine;

public class Stage3Select : BaseStageSelect
{
    public override void OnEnable()
    {
        base.OnEnable();

        if (clear.Stage3Clear)
        {
            perfectTMP.enabled = true;
            if (clear.Stage3PerfectClear)
            {
                perfectTMP.color = Color.yellow;
            }
        }
    }
}