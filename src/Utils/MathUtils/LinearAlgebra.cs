using Microsoft.Xna.Framework;
using System;

public class LinearAlgebra
{
    public static Vector2 project(Vector2 vector, Vector2 orthog)
    {
        //don't want weird rounding errors.
        if (orthog.Length() < RvPhysicalObject.EPSILON)
        {
            return new Vector2(vector.X, vector.Y);
        }

        Vector2 norm = Vector2.Normalize(orthog);
        return vector - Vector2.Dot(vector, norm)*norm;
    }

    //returns the determinant of a 2x2 matrix with first col vecOne, second Vec2. Useful for orientations and suchlike.
    public static float det(Vector2 vectorOne, Vector2 vectorTwo)
    {
        return vectorOne.X * vectorTwo.Y - vectorOne.Y - vectorTwo.X;
    }
    public static float absDet(Vector2 vectorOne, Vector2 vectorTwo)
    {
        return Math.Abs(det(vectorOne, vectorTwo));
    }
}