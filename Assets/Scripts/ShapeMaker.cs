using System;
using System.Collections.Generic;
using UnityEditor.Experimental.UIElements;
using UnityEngine;

public class ShapeMaker : MonoBehaviour
{
    private List<LineRender> _lines = new List<LineRender>();
    private List<Asteroid> _asteroids = new List<Asteroid>();
    public float ejectionVelocity = 3;

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void LateUpdate()
    {
        if (_lines.Count > 3 && ReferenceEquals(_lines[0].startAsteroid, _lines[_lines.Count - 1].startAsteroid))
        {
            while (_lines.Count > 0)
            {
                Destroy(_lines[0].startAsteroid);
                Destroy(_lines[0].endAsteroid);
                Destroy(_lines[0]);
            }
        }
    }

    private void Update()
    {
        if (ConcaveAngle() || Input.GetMouseButtonUp(0) ||
            Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Ended)
        {
            EjectAsteroids();
            while (_lines.Count > 0)
            {
                Destroy(_lines[0]);
            }
        }
    }

    private bool ConcaveAngle()
    {
        int i = 0;
        while (i + 1 < _lines.Count)
        {
            if (!_lines[i].IsComplete() || !_lines[i + 1].IsComplete()) break;
            
            Vector2 firstLine = _lines[i].GetComponent<LineRenderer>().GetPosition(1) -
                                _lines[i].GetComponent<LineRenderer>().GetPosition(0);
            Vector2 secondLine = _lines[i + 1].GetComponent<LineRenderer>().GetPosition(1) -
                                 _lines[i + 1].GetComponent<LineRenderer>().GetPosition(0);
            if (Vector2.Angle(firstLine, secondLine) < 90)
            {
                return true;
            }
        }

        return false;
    }

    public void EjectAsteroids()
    {
        Vector3 center = FindShapeCenter();
        foreach (var VARIABLE in _lines)
        {
            if (VARIABLE.startAsteroid != null)
            {
                VARIABLE.startAsteroid.GetComponent<Rigidbody2D>().velocity =
                    (VARIABLE.startAsteroid.transform.position - center).normalized * ejectionVelocity;
            }

            if (VARIABLE.endAsteroid != null)
            {
                VARIABLE.endAsteroid.GetComponent<Rigidbody2D>().velocity =
                    (VARIABLE.endAsteroid.transform.position - center).normalized * ejectionVelocity;
            }
        }
    }

    private Vector2 FindShapeCenter()
    {
        if (_lines.Count == 0)
        {
            return new Vector2(0, 0);
        }

        Vector2 max, min, position;
        max = min = position = _lines[0].GetPositions()[0];
        foreach (var VARIABLE in _lines)
        {
            position = VARIABLE.GetPositions()[0];
            max = Vector2.Max(max, position);
            min = Vector2.Min(min, position);
            position = VARIABLE.GetPositions()[1];
            max = Vector2.Max(max, position);
            min = Vector2.Min(min, position);
        }

        position = max - (max - min) / 2;
        return position;
    }

    public bool FreeAsteroid(GameObject asteroid)
    {
        foreach (var VARIABLE in _lines)
        {
            if (ReferenceEquals(VARIABLE.endAsteroid, asteroid))
                return false;
        }

        return true;
    }

    public void Destroy(LineRender victim)
    {
        _lines.Remove(victim);
        Destroy(victim.gameObject);
    }

    public void Destroy(Asteroid victim)
    {
        _asteroids.Remove(victim);
        Destroy(victim.gameObject);
    }

    public void AddLine(LineRender line)
    {
        _lines.Add(line);
    }

    public List<LineRender> GetLines()
    {
        return _lines;
    }

    public void AddAsteroid(Asteroid asteroid)
    {
        _asteroids.Add(asteroid);
    }

    public List<Asteroid> GetAsteroids()
    {
        return _asteroids;
    }
}