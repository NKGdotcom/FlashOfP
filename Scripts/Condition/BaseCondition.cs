using UnityEngine;

/// <summary>
/// ƒQ[ƒ€–{•Ò‚Ì•”•ª‚ÌğŒ‚ÌŠî–{•”•ª
/// </summary>
public class BaseCondition : MonoBehaviour, ICondition
{
    protected bool isFinish;
    public virtual bool CheckCondition()
    {
        return isFinish;
    }
    public virtual void ResetCondition()
    {
        isFinish = false;
    }
}
