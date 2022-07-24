using UnityEngine;

public class Invisibloc : Triggered
{
    private void Start()
    {
        gameObject.SetActive(false);
    }

    public override void BeginTrigger(GameObject triggerer)
    {
        gameObject.SetActive(!gameObject.activeInHierarchy);
    }

    public override void EndTrigger(GameObject triggerer)
    {
    }
}
