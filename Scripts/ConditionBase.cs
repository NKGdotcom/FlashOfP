using UnityEngine;

public abstract class ConditionBase : MonoBehaviour
{
    public virtual void OnInitialize() { }

    // ğŒ‚ğ–‚½‚µ‚½‚©‚Ç‚¤‚©‚ğ•Ô‚·
    public abstract bool CheckCondition();
}
