using outline;
using Unity.VisualScripting;
using UnityEngine;

public enum SelectableType {Selected, Hover, Unselected};

public class Selectable : MonoBehaviour
{
    private OutlineController _controller;
    
    public void UpdateStatus(SelectableType type)
    {
        _checkOutlineScript();

        switch(type)
        {
            case SelectableType.Hover: 
                _HoverObject();
                break;
            case SelectableType.Selected:
                _SelectObject();
                break;
            case SelectableType.Unselected:
                _UnselectObject();
                break;
            default:
                _UnselectObject();
                break;
        }
    }

    private void _HoverObject()
    {

        _controller.isVisible = true;
        _controller.outlineColor = Dimens.OutlineHoverColor;
    }

    

    private void _SelectObject()
    {
        _controller.isVisible = true;
        _controller.outlineColor = Dimens.OutlineSelectedColor;
    }

    private void _UnselectObject()
    {
        _controller.isVisible = false;
        _controller.outlineColor = Dimens.OutlineHoverColor.WithAlpha(.0f);
    }

    private void _checkOutlineScript()
    {
        if(gameObject.GetComponent<OutlineSettings>() == null)
            gameObject.AddComponent<OutlineSettings>();

        _controller = gameObject.GetComponent<OutlineController>();
    }
}
