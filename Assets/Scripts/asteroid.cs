using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class Asteroid : MonoBehaviour
{
    public float minSpeed = 1.0f;
    public float maxSpeed = 10.0f;
    public float slowSpeed = 0.01f;
    public float maxRotation = 100f;
    public float massMultiplier = 100f;
    public ShapeMaker shapeMaker;
    private Collider2D _collider;
    private Rigidbody2D _rigidbody2D;
    private Vector2 _screenBounds;
    private float _startSpeed, _startRotation;
    
    // Start is called before the first frame update
    void Start()
    {
        shapeMaker = Camera.main.GetComponent<ShapeMaker>();
        // generate screenbounds for interactions
        _screenBounds =
            Camera.main.ViewportToWorldPoint(new Vector3(1, 1, Camera.main.nearClipPlane));
        
        // Set initial velocity
        _rigidbody2D = this.GetComponent<Rigidbody2D>();
        _startSpeed = Random.Range(minSpeed, maxSpeed);
        float xVelocity = Random.Range(0, _startSpeed);
        float yVelocity = _startSpeed - Mathf.Abs(xVelocity);
        if (transform.position.x > 0)
        {
            xVelocity = -xVelocity;
        }
        if (transform.position.y > 0)
        {
            yVelocity = -yVelocity;
        }
        _rigidbody2D.velocity = new Vector2(xVelocity, yVelocity);

        _startRotation = Random.Range(-maxRotation, maxRotation);
        _rigidbody2D.angularVelocity = _startRotation;
    }

    // Update is called once per frame
    private void Update()
    {
        TestBounds();
    }

    private void OnMouseDown()
    {
        var velocity = _rigidbody2D.velocity;
        _startSpeed = velocity.magnitude;
        velocity *= slowSpeed;
        _rigidbody2D.velocity = velocity;

        _rigidbody2D.mass *= massMultiplier;
        
        _rigidbody2D.angularVelocity *= slowSpeed;
    }

    private void TestBounds()
    {
        if (transform.position.x > _screenBounds.x * 2 ||
            transform.position.x < -_screenBounds.x * 2 ||
            transform.position.y > _screenBounds.y * 2 ||
            transform.position.y < -_screenBounds.y * 2)
        {
            shapeMaker.Destroy(this);
        }
    }
    
    private void OnMouseEnter()
    {
        if (Input.GetMouseButton(0) || Input.touchCount > 0)
        {
            OnMouseDown();
        }
    }

    private void OnDestroy()
    {
        shapeMaker.Destroy(this);
    }
}
