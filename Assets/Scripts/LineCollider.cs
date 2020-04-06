using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LineCollider : MonoBehaviour
{
    private LineRender _lineRender;

    public LineRender LineRender
    {
        get => _lineRender;
        set => _lineRender = value;
    }

    // Start is called before the first frame update
    void Start()
    {
        Update();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Asteroid") && !other.GetComponent<Asteroid>().Ejected)
        {
            _lineRender.Collide(other.gameObject);
            other.GetComponent<RenderLine>().Collide();
        }
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = _lineRender.GetEndPosition();
    }
}