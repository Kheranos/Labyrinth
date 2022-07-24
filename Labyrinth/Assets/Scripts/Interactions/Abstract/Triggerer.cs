using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider))]
public abstract class Triggerer : MonoBehaviour
{
    [SerializeField] protected ETriggerType triggerType;
    [SerializeField] protected List<Triggerable> triggereds = new();

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.TryGetComponent(out Triggerable triggerable) && 
            !triggereds.Contains(triggerable) &&
            triggerable.CanTrigger(triggerType))
        {
            triggereds.Add(triggerable);
            BeginTrigger(triggerable);
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.TryGetComponent(out Triggerable triggerable) && 
            triggereds.Contains(triggerable) &&
            triggerable.CanTrigger(triggerType))
        {
            triggereds.Remove(triggerable);
            EndTrigger(triggerable);
        }
    }

    protected void BeginTrigger(Triggerable triggerable)
    {
        triggerable.BeginTrigger(gameObject);
    }

    protected void EndTrigger(Triggerable triggerable)
    {
        triggerable.EndTrigger(gameObject);
    }
}
