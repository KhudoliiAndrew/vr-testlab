using System;
using BNG;
using UnityEngine;

public abstract class Wand : GrabbableEvents
{
    [Header("Settings")] public bool disableWhenDropped;

    private bool _isActivated;

    private void Update()
    {
        TouchpadParser();
    }

    private void TouchpadParser()
    {
        if(thisGrabber == null) return;
        
        Vector2 touchpadInput = Vector2.zero;

        if (thisGrabber.HandSide == ControllerHand.Left)
            touchpadInput = input.LeftThumbstickAxis;
        
        if (thisGrabber.HandSide == ControllerHand.Right)
            touchpadInput = input.RightThumbstickAxis;

        OnThumbstickAxis(touchpadInput);
    }

    public override void OnTriggerDown()
    {
        _isActivated = !_isActivated;

        if (_isActivated) OnWandActivated();
        else OnWandDisabled();

        OnWandHolding(_isActivated);

        base.OnTriggerDown();
    }

    public override void OnTriggerUp()
    {
        //OnWandHolding(false);

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

    protected virtual void OnThumbstickAxis(Vector2 input)
    {
    }
}