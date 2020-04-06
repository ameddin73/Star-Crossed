using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.PackageManager;
using UnityEngine;

public class LineRender : MonoBehaviour
{
    private LineRenderer _lineRenderer;
    private ShapeMaker shapeMaker;
    public GameObject startAsteroid, endAsteroid;
    
    private bool _complete;
    
    // Start is called before the first frame update
    void Start()
    {
        _lineRenderer = GetComponent<LineRenderer>();
        shapeMaker = Camera.main.GetComponent<ShapeMaker>();
        
        // Set start position
        _lineRenderer.positionCount = 2;
        // _lineRenderer.SetPosition(0, GetPosition());
        // _lineRenderer.SetPosition(1, GetPosition());
    }

    // Update is called once per frame
    void Update()
    {
        // Set start position to asteroid and end position to mouse/end asteroid
        _lineRenderer.SetPosition(0, startAsteroid.transform.position);
        _lineRenderer.SetPosition(1, !_complete ? FindInputPosition() : endAsteroid.transform.position);
        
        // check if new position intersects w/ an asteroid
        Collider2D overlapPoint = Physics2D.OverlapPoint(_lineRenderer.GetPosition(1));
        if (!_complete && overlapPoint && overlapPoint.gameObject != startAsteroid
            && shapeMaker.FreeAsteroid(overlapPoint.gameObject))
        {
            Debug.Log("Overlap Point: " + _lineRenderer.GetPosition(1));
            _complete = true;
            endAsteroid = overlapPoint.gameObject;
        }

        if (Input.GetMouseButtonUp(0) && Input.touchCount == 0)
        {
            shapeMaker.Destroy(this);
        }
    }

    public bool IsComplete()
    {
        return _complete;
    }

    public Vector2[] GetPositions()
    {
        return new Vector2[]{_lineRenderer.GetPosition(0), _lineRenderer.GetPosition(1)};
    }

    private Vector3 FindInputPosition()
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

    private void OnDestroy()
    {
        shapeMaker.Destroy(this);
    }
}
