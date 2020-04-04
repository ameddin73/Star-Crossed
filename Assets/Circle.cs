using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Circle : MonoBehaviour
{
    private float _startPoseX, _startPoseY;
    private bool _isBeingHeld = false;
    private Vector3 _mousePos;

    // Update is called once per frame
    void Update()
    {
        if (_isBeingHeld)
        {
            _mousePos = Input.mousePosition;
            if (Camera.main != null) _mousePos = Camera.main.ScreenToWorldPoint(_mousePos);
            this.gameObject.transform.localPosition = new Vector3(_mousePos.x - _startPoseX, _mousePos.y - _startPoseY,0);
        }
    }

    private void OnMouseDown()
    {
        if (Input.GetMouseButtonDown(0))
        {
            _mousePos = Input.mousePosition;
            if (Camera.main != null) _mousePos = Camera.main.ScreenToWorldPoint(_mousePos);

            var localPosition = transform.localPosition;
            _startPoseX = _mousePos.x - localPosition.x;
            _startPoseY = _mousePos.y - localPosition.y;

            _isBeingHeld = true;
        }
    }

    private void OnMouseUp()
    {
        _isBeingHeld = false;
    }
}