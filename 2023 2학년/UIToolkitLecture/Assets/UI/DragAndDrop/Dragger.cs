using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class Dragger : MouseManipulator
{
    private Vector2 _startPos; 
    private bool _isDragging = false;
    private Vector2 _originalPos;

    private Action<Vector2, Vector2> _dropCallback; 
                                                    

    public Dragger(Action<Vector2, Vector2> DropCallback)
    {
        _dropCallback = DropCallback; 
        _isDragging = false;
        activators.Add(new ManipulatorActivationFilter { button = MouseButton.LeftMouse });
    }

    protected override void RegisterCallbacksOnTarget()
    {
        target.RegisterCallback<MouseDownEvent>(OnMouseDown);
        target.RegisterCallback<MouseMoveEvent>(OnMouseMove);
        target.RegisterCallback<MouseUpEvent>(OnMouseUp);
    }

    protected override void UnregisterCallbacksFromTarget()
    {
        target.UnregisterCallback<MouseDownEvent>(OnMouseDown);
        target.UnregisterCallback<MouseMoveEvent>(OnMouseMove);
        target.UnregisterCallback<MouseUpEvent>(OnMouseUp);
    }

    protected void OnMouseDown(MouseDownEvent evt)
    {
        if(CanStartManipulation(evt)) 
        {
            _startPos = evt.localMousePosition;
            _originalPos = new Vector2(target.style.left.value.value, target.style.top.value.value);
            _isDragging = true;

            target.CaptureMouse(); 
            evt.StopPropagation();
        }
    }

    protected void OnMouseMove(MouseMoveEvent evt)
    {
        if(_isDragging && target.HasMouseCapture())
        {
            Vector2 diff = evt.localMousePosition - _startPos;

            target.style.top = new Length(target.layout.y + diff.y, LengthUnit.Pixel);
            target.style.left = new Length(target.layout.x + diff.x, LengthUnit.Pixel);
        }
    }

    protected void OnMouseUp(MouseUpEvent evt)
    {
        if (!_isDragging || !target.HasMouseCapture()) return;

        target.ReleaseMouse();
        _isDragging = false;
        _dropCallback?.Invoke(_originalPos, evt.mousePosition);
    }
}
