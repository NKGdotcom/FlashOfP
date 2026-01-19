using UnityEngine;

public class Stage2Select : BaseStageSelect
{
    public override void OnEnable()
    {
        base.OnEnable();

        if (clear.Stage2Clear)
        {
            perfectTMP.enabled = true;
            if (clear.Stage2PerfectClear)
            {
                perfectTMP.color = Color.yellow;
            }
        }
    }
}