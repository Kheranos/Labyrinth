using UnityEngine;

public abstract class Triggered : MonoBehaviour
{
    public abstract void BeginTrigger(GameObject triggerer);
    public abstract void EndTrigger(GameObject triggerer);
}
