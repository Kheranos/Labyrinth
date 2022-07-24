using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider))]
public abstract class Triggerable : MonoBehaviour
{
    [SerializeField] protected List<ETriggerType> canTrigger = new();

    [SerializeField] protected float timer;
    protected float runningTimer;

    protected GameObject lastTriggerer;

    public virtual void BeginTrigger(GameObject triggerer)
    {
        lastTriggerer = triggerer;

        if (timer > 0f)
            runningTimer = timer;
    }

    //TODO Remove and use toggle instead, or use another type of Triggerable, to see...
    public virtual void EndTrigger(GameObject triggerer)
    {
    }

    public bool CanTrigger(ETriggerType triggerType)
        => canTrigger.Contains(triggerType);

    protected void Update()
    {
        if (timer > 0)
        {
            runningTimer -= Time.deltaTime;
            if (runningTimer <= 0 && lastTriggerer != null)
            {
                BeginTrigger(lastTriggerer);
                lastTriggerer = null;
                runningTimer = 0;
            }
        }
    }
}
