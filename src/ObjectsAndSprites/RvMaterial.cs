using System;
using System.Collections.Generic;

public class RvMaterial
{
    public int id = 0;

    public RvMaterial(int id)
    {
        this.id = id;
    }

    public static readonly int MATERIAL_UNDEFINED                   = 0;
    public static readonly int MATERIAL_KNIGHT                      = 1;
    public static readonly int MATERIAL_BRICK_WALL                  = 2;

    public static Dictionary<Tuple<int,int>, float> dictCoefficientsOfFriction = new Dictionary<Tuple<int, int>, float>();

    public static float getCoefficientOfFriction(RvMaterial materialOne, RvMaterial materialTwo)
    {
        return getCoefficientOfFriction(materialOne.id, materialTwo.id);
    }

    public static float getCoefficientOfFriction(int idOne, int idTwo)
    {
        try 
        {
            return dictCoefficientsOfFriction[new Tuple<int,int>(idOne, idTwo)];
        }
        catch (KeyNotFoundException e)
        {
            return 0.0f;
        }
    }

    public static void initCoefficientsOfFriction()
    {
        //add all coefficients of friction between material ids in here.

    }

    public static void addCoefficientOfFriction(int idOne, int idTwo, float value)
    {
        Tuple<int, int> tuple = new Tuple<int, int>(idOne, idTwo);
        Tuple<int, int> tupleSymmetry = new Tuple<int, int>(idTwo, idOne);

        dictCoefficientsOfFriction[tuple] = value;
        dictCoefficientsOfFriction[tupleSymmetry] = value;
    }
}