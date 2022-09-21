using BNG;
using UnityEngine;

public abstract class Wand : GrabbableEvents
{
    private bool _isActivated = false;

    public override void OnTriggerDown()
    {
        _isActivated = !_isActivated;

        if (_isActivated) OnWandActivated();
        else OnWandDisabled();
        
        OnWandHolding(true);

        base.OnTriggerDown();
    }

    public override void OnTriggerUp()
    {
        OnWandHolding(false);

        base.OnTriggerUp();
    }

    protected virtual void OnWandActivated()
    {
    }

    protected virtual void OnWandDisabled()
    {
    }

    protected virtual void OnWandHolding(bool isHolding)
    {
    }
}