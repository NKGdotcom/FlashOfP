using UnityEngine;

public class Stage1Select : BaseStageSelect
{
    public override void OnEnable()
    {
        base.OnEnable();

        if (clear.Stage1Clear)
        {
            perfectTMP.enabled = true;
            if (clear.Stage1PerfectClear)
            {
                perfectTMP.color = Color.yellow;
            }
        }
    }
}
