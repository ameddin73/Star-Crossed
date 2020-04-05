using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RenderLine : MonoBehaviour
{
    public LineRender linePrefab;
    
    // Start is called before the first frame update
    void Start()
    {
        
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
            linePrefab.startAsteroid = this.gameObject;
            Instantiate(linePrefab);
    }
}
