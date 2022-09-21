using BNG;
using UnityEngine;

public abstract class Wand : GrabbableEvents
{
    [Header("Settings")]
    public bool disableWhenDropped;

    private bool _isActivated;
    
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

    public override void OnRelease()
    {
        if (disableWhenDropped)
        {
            OnWandDisabled();
            OnWandHolding(false);
        }
        
        base.OnRelease();
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