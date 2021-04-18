using Microsoft.Xna.Framework;
using System.Collections.Generic;

namespace Shapes
{
    public interface RvShapeI
    {
        public abstract bool isInterior(Vector2 point);

        public abstract List<Vector2> sampleEdgePoints();

        public abstract void setTranslation(Vector2 translation);

        private bool shapeAEdgePointInShapeB(RvShapeI shapeA, RvShapeI shapeB)
        {
            List<Vector2> shapeAEdgePoints = shapeA.sampleEdgePoints();

            for (int i=0; i<shapeAEdgePoints.Count; i++)
            {
                Vector2 point = shapeAEdgePoints[i];
                if (shapeB.isInterior(point))
                {
                    return true;
                }
            }
            return false;
        }

        public bool overlapping(RvShapeI otherShape)
        {
            return shapeAEdgePointInShapeB(this, otherShape)
                || shapeAEdgePointInShapeB(otherShape, this);
        }

        //This is a method to work out e.g. normal forces. If shapes overlap, the restoring direction should tell them which way to move to become seperate again.
        public abstract Vector2 getRestoringDirection(List<Vector2> edgePoints);
    }
}