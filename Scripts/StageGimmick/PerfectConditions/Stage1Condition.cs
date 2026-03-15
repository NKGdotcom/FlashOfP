using UnityEngine;

/// <summary>
/// Stage 1 PerfectCondition
/// </summary>
public class Stage1Condition : BasePerfectCondition
{
    [SerializeField] private float perfectTime = 6f;
    private float timer = 0f;
    private void OnEnable()
    {
        timer = 0;
    }
    private void Update()
    {
        timer += Time.deltaTime;
    }

    public override bool IsPerfect()
    {
        return perfectTime > timer;
    }
}
