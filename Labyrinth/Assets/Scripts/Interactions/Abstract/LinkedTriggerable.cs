using System.Collections.Generic;
using UnityEngine;

public abstract class LinkedTriggerable : Triggerable
{
    [SerializeField] protected List<Triggered> triggereds;

    public override void BeginTrigger(GameObject triggerer)
    {
        base.BeginTrigger(triggerer);

        if (triggereds != null && triggereds.Count > 0)
            triggereds.ForEach(i => i.BeginTrigger(triggerer));
    }

    public override void EndTrigger(GameObject triggerer)
    {
        base.EndTrigger(triggerer);

        if (triggereds != null && triggereds.Count > 0)
            triggereds.ForEach(i => i.EndTrigger(triggerer));
    }
}
