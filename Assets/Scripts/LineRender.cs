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
    public LineCollider lineCollider;

    private bool _complete;

    // Start is called before the first frame update
    void Start()
    {
        _lineRenderer = GetComponent<LineRenderer>();
        shapeMaker = Camera.main.GetComponent<ShapeMaker>();

        // Set start position
        _lineRenderer.positionCount = 2;
        
        // Create collider
        lineCollider = Instantiate(lineCollider);
        lineCollider.LineRender = this;
    }

    // Update is called once per frame
    void Update()
    {
        // Set start position to asteroid and end position to mouse/end asteroid
        _lineRenderer.SetPosition(0, startAsteroid.transform.position);
        _lineRenderer.SetPosition(1, !_complete ? FindInputPosition() : endAsteroid.transform.position);

        // Destroy it when released
        if (Input.GetMouseButtonUp(0) && Input.touchCount == 0)
        {
            shapeMaker.Destroy(this);
        }
    }

    public void Collide(GameObject other)
    {
        if (!_complete && other.gameObject != startAsteroid && !shapeMaker.IsEndAsteroid(other.gameObject))
        {
            Destroy(lineCollider.gameObject);
            _complete = true;
            endAsteroid = other.gameObject;
        }
    }

    public Vector3 GetEndPosition()
    {
        return _lineRenderer.GetPosition(1);
    }

    public bool IsComplete()
    {
        return _complete;
    }

    public Vector2[] GetPositions()
    {
        return new Vector2[] {_lineRenderer.GetPosition(0), _lineRenderer.GetPosition(1)};
    }

    private Vector3 FindInputPosition()
    {
        Vector2 position = new Vector2();
        if (Input.GetMouseButton(0))
        {
            position = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        }

        if (Input.touchCount > 0)
        {
            position = new Vector2(Input.GetTouch(0).position.x, Input.GetTouch(0).position.y);
            position = Camera.main.ScreenToWorldPoint(position);
        }

        return position;
    }

    private void OnDestroy()
    {
        if (lineCollider)
        {
            Destroy(lineCollider.gameObject);
        }
        shapeMaker.Destroy(this);
    }
}