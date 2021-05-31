using Shapes;
using System.Collections.Generic;
using Microsoft.Xna.Framework;

public abstract class RvAbstractShape : RvAbstractWrappable, RvShapeI
{
    public abstract Rectangle getRectangle();

    public abstract override RvAbstractShapeWrapper wrap();

    /*
     * RvShapeI methods
     */
    public abstract bool isInterior(Vector2 point);

    public abstract List<Vector2> sampleEdgePoints();

    public abstract void setTranslation(Vector2 translation);

    //This is a method to work out e.g. normal forces. If shapes overlap, the restoring direction should tell them which way to move to become seperate again.
    public abstract Vector2 getRestoringDirection(List<Vector2> edgePoints);

    //not ideal, but will do for now.
    public abstract float getWidth();
    public abstract float getHeight();
}

public abstract class RvAbstractShapeWrapper : RvAbstractWrapper
{
    public abstract override RvAbstractShape unWrap();
}