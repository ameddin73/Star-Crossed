using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RenderLine : MonoBehaviour
{
    public LineRender linePrefab;
    private ShapeMaker shapeMaker;
    
    // Start is called before the first frame update
    void Start()
    {
        shapeMaker = Camera.main.GetComponent<ShapeMaker>();
    }

    private void OnMouseEnter()
    {
        if (Input.GetMouseButton(0) || Input.touchCount > 0)
        {
            OnMouseDown();
        }
    }

    private void OnMouseDown()
    {

        LineRender lineRender = Instantiate(linePrefab);
        lineRender.startAsteroid = this.gameObject;
        shapeMaker.AddLine(lineRender);
    }
}
