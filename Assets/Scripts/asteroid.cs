using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Asteroid : MonoBehaviour
{
    public float minSpeed = 1.0f;
    public float maxSpeed = 10.0f;
    private Rigidbody2D _rigidbody2D;
    private Vector2 _screenBounds;
    
    // Start is called before the first frame update
    void Start()
    {
        // generate screenbounds for interactions
        _screenBounds =
            Camera.main.ViewportToWorldPoint(new Vector3(1, 1, Camera.main.nearClipPlane));
        
        // Set initial velocity
        _rigidbody2D = this.GetComponent<Rigidbody2D>();
        float speed = Random.Range(minSpeed, maxSpeed);
        float xVelocity = Random.Range(0, speed);
        float yVelocity = speed - Mathf.Abs(xVelocity);
        if (transform.position.x > 0)
        {
            xVelocity = -xVelocity;
        }
        if (transform.position.y > 0)
        {
            yVelocity = -yVelocity;
        }
        _rigidbody2D.velocity = new Vector2(xVelocity, yVelocity);
    }

    // Update is called once per frame
    void Update()
    {
        if (transform.position.x > _screenBounds.x * 2 ||
            transform.position.x < -_screenBounds.x * 2 ||
            transform.position.y > _screenBounds.y * 2 ||
            transform.position.y < -_screenBounds.y * 2)
        {
            Destroy(this.gameObject);
        }
    }
}
