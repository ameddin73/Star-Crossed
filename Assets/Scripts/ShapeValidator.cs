using System;
using System.Collections.Generic;
using UnityEngine;

public static class ShapeValidator
{
    public static bool IsValid(List<Vector2> vectors)
    {
        vectors.Add(vectors[0]);

        int i = 1;
        float angle = 0;
        float prevAngle = Vector2.SignedAngle(vectors[0], vectors[1]);
        while (i < vectors.Count + 2)
        {
            angle = Vector2.SignedAngle(vectors[i], vectors[i + 1]);
            if (Math.Abs(prevAngle / Mathf.Abs(prevAngle) - angle / Mathf.Abs(angle)) < 0.1)
            {
                return false;
            }
            i++;
        }
        return true;
    }
}