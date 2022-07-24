using UnityEngine;

public class Lever : LinkedTriggerable
{
    [SerializeField] private Transform lever;
    [SerializeField] private Vector3 onPosition, offPosition;
    [SerializeField] private bool isOn;
    private bool IsOn
    {
        get => isOn;
        set
        {
            isOn = value;
            lever.localPosition = isOn ? onPosition : offPosition;
        }
    }

    private void Start()
    {
        IsOn = false;
    }

    public override void BeginTrigger(GameObject triggerer)
    {
        IsOn = !IsOn;

        base.BeginTrigger(triggerer);
    }
}
