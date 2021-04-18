using Microsoft.Xna.Framework;
using System.Collections.Generic;
using System;

namespace Shapes
{
    public class RvConvexPolygon : RvShapeI
    {
        private List<Vector2> vertices;
        private Vector2 centreOfVertices; //A measure of the average position of vertices.

        public RvConvexPolygon(List<Vector2> vertices)
        {
            //should really pass in clockwise... but just to be sure!
            this.vertices = orderClockwise(vertices);
            calculateCentreOfVertices();
        }

        /*
         * You will get unexpected results if the vertices do not form a convex polygon!
         */
        private List<Vector2> orderClockwise(List<Vector2> vertices)
        {
            List<Vector2> retval = new List<Vector2>();
            int size = vertices.Count;
            Vector2 vertex = vertices[0];
            while (retval.Count < size)
            {
                retval.Add(vertex);
                vertex = getAdjacentVertices(vertex).Item1;
            }
            return retval;
        }

        private Tuple<Vector2, Vector2> getAdjacentVertices(Vector2 vertex)
        {
            List<Vector2> adjacentVertices = new List<Vector2>();
            for (int i=0; i<vertices.Count; i++)
            {
                Vector2 vtex = vertices[i];
                if (vtex.Equals(vertex))
                    continue;
                if (adjacentVertices.Count == 0
                    || adjacentVertices.Count == 1)
                  {
                    adjacentVertices.Add(vtex);
                    continue;
                  }

                //If we get to here, then we know that our vertices are initialised.
                Vector2 vNormalized = Vector2.Normalize(vertex);
                Vector2 va1Normalized = Vector2.Normalize(adjacentVertices[0]);
                Vector2 va2Normalized = Vector2.Normalize(adjacentVertices[1]);

                float val1 = Vector2.Dot(vNormalized, va1Normalized);
                float val2 = Vector2.Dot(vNormalized, va2Normalized);
                float val3 = Vector2.Dot(va1Normalized, va2Normalized);

                if (val1 <= Math.Min(val2, val3))
                {
                    adjacentVertices[1] = vertex;
                    continue;
                }
                if (val2 <= Math.Min(val1, val3))
                {
                    adjacentVertices[0] = vertex;
                    continue;
                }
                //the new vertex was between the other two - just continue.
            }
            
            //our list now contains the two adjacent vertices. Final thing to do is determine their order.
            Vector2 adjOne = adjacentVertices[0];
            Vector2 adjTwo = adjacentVertices[1];
            float det = adjOne.X * adjTwo.Y - adjOne.Y * adjTwo.X;

            //anticlockwise on the left, clockwise on the right.
            if (det > 0)
                return new Tuple<Vector2, Vector2>(adjTwo, adjOne);
            else
                return new Tuple<Vector2, Vector2>(adjOne, adjTwo);
        }

        public bool isInterior(Vector2 point)
        {
            int size = vertices.Count;
            for (int i=0; i<size; i++)
            {
                Vector2 normalizedEdge = Vector2.Normalize(vertices[(i+1)%size] - vertices[i]);
                Vector2 normalizedLine = Vector2.Normalize(point - vertices[i]);

                float det = normalizedEdge.X * normalizedLine.Y - normalizedEdge.Y * normalizedLine.X;
                if (det<0)
                    return false;
            }
            return true;
        }

        public List<Vector2> sampleEdgePoints()
        {
            //Probably want to make this better at some point (i.e. include points along edges as well!)
            return new List<Vector2>(vertices);
        }

        private void calculateCentreOfVertices()
        {
            int size = vertices.Count;
            Vector2 cov = Vector2.Zero;

            for (int i=0; i<size; i++)
            {
                cov += vertices[i];
            }
            cov = Vector2.Multiply(cov, 1/(float)size);

            this.centreOfVertices = cov;
        }

        //may want to think of a better way to do this (maybe use the normal of the nearest edge)
        public Vector2 getRestoringDirection(List<Vector2> edgePoints)
        {
            return Vector2.Zero;//unimplemented
        }

        public void setTranslation(Vector2 translation)
        {
            //not implemented yet.
        }
    }
}