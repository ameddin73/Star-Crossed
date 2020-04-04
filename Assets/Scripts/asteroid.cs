using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class asteroid : MonoBehaviour
{
    public float speed = 10.0f;
    private Rigidbody2D _rigidbody2D;
    private Vector2 _screenBounds;
    
    // Start is called before the first frame update
    void Start()
    {
        _rigidbody2D = this.GetComponent<Rigidbody2D>();
        _rigidbody2D.velocity = new Vector2(-speed, 0);
        _screenBounds =
            Camera.main.ViewportToWorldPoint(new Vector3(0, 0, Camera.main.nearClipPlane));
    }

    // Update is called once per frame
    void Update()
    {
        if (transform.position.x < _screenBounds.x * 2)
        {
            Destroy(this.gameObject);
        }
    }
}
