using System;
using System.Collections.Generic;
using UnityEngine;

public static class ShapeValidator
{
    public static bool IsValid(List<Vector2> vectors)
    {
        vectors.Add(vectors[0]);

        int i = 1;
        Vector2 AB = vectors[i - 1] - vectors[i];
        Vector2 BC = vectors[i + 1] - vectors[i];
        var prevAngle = Vector2.SignedAngle(AB, BC);
        i++;
        
        while (i + 1 < vectors.Count)
        {
            AB = vectors[i - 1] - vectors[i];
            BC = vectors[i + 1] - vectors[i];
            var angle = Vector2.SignedAngle(AB, BC);

            if (prevAngle / Mathf.Abs(prevAngle) != angle / Mathf.Abs(angle))
            {
                return false;
            }

            i++;
        }

        return true;
    }
}