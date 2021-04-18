using Microsoft.Xna.Framework;
using System.Collections.Generic;

namespace Shapes
{
        public class RvCircle : RvShapeI
    {
        private Vector2 centre;
        private float radius;

        public RvCircle(Vector2 centre, float radius)
        {
            this.centre = centre;
            this.radius = radius;
        }

        public bool isInterior(Vector2 point)
        {
            return Vector2.Distance(point, centre) <= radius;
        }
        public List<Vector2> sampleEdgePoints()
        {
            return new List<Vector2>();
        }
  
        public Vector2 getRestoringDirection(List<Vector2> edgePoints)
        {
            return Vector2.Zero; //unimplemented
        }

        public void setTranslation(Vector2 translation)
        {
            this.centre = translation;
        }
    }
}