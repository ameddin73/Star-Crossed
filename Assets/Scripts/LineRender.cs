using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.PackageManager;
using UnityEngine;

public class LineRender : MonoBehaviour
{
    private LineRenderer _lineRenderer;
    
    // Start is called before the first frame update
    void Start()
    {
        _lineRenderer = GetComponent<LineRenderer>();
        
        // Set start position
        _lineRenderer.positionCount = 2;
        _lineRenderer.SetPosition(0, GetPosition());
        _lineRenderer.SetPosition(1, GetPosition());
    }

    // Update is called once per frame
    void Update()
    {
        _lineRenderer.SetPosition(1, GetPosition());
        if (Input.GetMouseButtonUp(0) && Input.touchCount == 0)
        {
            Destroy(this.gameObject);
        }
    }

    private Vector3 GetPosition()
    {
        Vector2 position = new Vector2();
        if (Input.GetMouseButton(0))
        {
            position = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Debug.Log("Mouse: " + position);
        }

        if (Input.touchCount > 0)
        {
            position = new Vector2(Input.GetTouch(0).position.x, Input.GetTouch(0).position.y);
            position = Camera.main.ScreenToWorldPoint(position);
        Debug.Log("Touch: " + position);
        }
        Debug.Log("Mouse: " + position);
        return position;
    }
}
