using UnityEngine;

public class Stage5Select : BaseStageSelect
{
    public override void OnEnable()
    {
        base.OnEnable();

        if (clear.Stage5Clear)
        {
            perfectTMP.enabled = true;
            if (clear.Stage5PerfectClear)
            {
                perfectTMP.color = Color.yellow;
            }
        }
    }
}