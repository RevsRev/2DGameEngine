using Microsoft.Xna.Framework;
using System.Collections.Generic;
using Microsoft.Xna.Framework.Graphics;
using System;
using Newtonsoft.Json;

namespace Shapes
{
    public class RvHorizontalRectangle : RvAbstractShape
    {
        [JsonProperty] private Vector2 bottomLeftCorner;
        [JsonProperty] private float width;
        [JsonProperty] private float height;
        
        public RvHorizontalRectangle(Vector2 bottomLeftCorner, float width, float height)
        {
            this.bottomLeftCorner = bottomLeftCorner;
            this.width = width;
            this.height = height;
        }

        public override Boolean isInterior(Vector2 point)
        {
            float X = point.X;
            float Y = point.Y;

            return bottomLeftCorner.X < X && X < bottomLeftCorner.X + width
              && bottomLeftCorner.Y < Y && Y <bottomLeftCorner.Y + height;
        }

        public override List<Vector2> sampleEdgePoints()
        {
            return getCorners();
        }

        private List<Vector2> getCorners()
        {
            Vector2 bottomLeft = new Vector2(bottomLeftCorner.X, bottomLeftCorner.Y);
            Vector2 bottomRight = new Vector2(bottomLeftCorner.X + width, bottomLeftCorner.Y);
            Vector2 topLeft = new Vector2(bottomLeftCorner.X, bottomLeftCorner.Y + height);
            Vector2 topRight = new Vector2(bottomLeftCorner.X + width, bottomLeftCorner.Y + height);

            return new List<Vector2>{bottomLeft, topLeft, topRight, bottomRight};
        }

        public override Vector2 getRestoringDirection(List<Vector2> edgePoints)
        {
            Vector2 posDiagonal = Vector2.Normalize(new Vector2(width, height));
            Vector2 negDiagnoal = Vector2.Normalize(new Vector2(width, -height));

            List<Vector2> restoringDirrections = new List<Vector2>();

            for (int i=0; i<edgePoints.Count; i++)
            {
                Vector2 point = edgePoints[i];
                Vector2 pointInLocalCoords = point - (bottomLeftCorner + new Vector2(width/2, height/2));

                if (!isInterior(point))
                {
                    continue;
                }

                bool abovePosDiagnoal = pointInLocalCoords.X * posDiagonal.Y - pointInLocalCoords.Y * posDiagonal.X < 0;
                bool aboveNegDiagonal = pointInLocalCoords.X * negDiagnoal.Y - pointInLocalCoords.Y * negDiagnoal.X < 0;

                if (abovePosDiagnoal && aboveNegDiagonal)
                {
                    float distance = Math.Max(bottomLeftCorner.Y + height - point.Y, 0);
                    restoringDirrections.Add(new Vector2(0,distance));
                }
                else if (abovePosDiagnoal && !aboveNegDiagonal)
                {
                    float distance = Math.Max(point.X - bottomLeftCorner.X, 0);
                    restoringDirrections.Add(new Vector2(-distance,0));
                }
                else if (!abovePosDiagnoal && aboveNegDiagonal)
                {
                    float distance = Math.Max(bottomLeftCorner.X + width - point.X, 0);
                    restoringDirrections.Add(new Vector2(distance,0));
                }
                else
                {
                    float distance = Math.Max(point.Y - bottomLeftCorner.Y, 0);
                    restoringDirrections.Add(new Vector2(0, -distance));
                }
            }

            if (restoringDirrections.Count == 0)
            {
                return Vector2.Zero;
            }

            //Now we have a vector of restoring directions. Pick the one with the smallest magnitude and restore.
            Vector2 restoringDirrection = restoringDirrections[0];
            for (int i=1; i<restoringDirrections.Count; i++)
            {
                Vector2 restoringDir = restoringDirrections[i];
                float length = restoringDir.Length();

                if (length < restoringDirrection.Length())
                {
                    restoringDirrection = restoringDir;
                }
            }
            return restoringDirrection;
        }

        public override void setTranslation(Vector2 translation)
        {
            this.bottomLeftCorner = translation;
        }

        public void drawBoundary(RvSpriteBatch spriteBatch)
        {
            Rectangle top = new Rectangle((int)(bottomLeftCorner.X), (int)(bottomLeftCorner.Y + height), (int)width, 1);
            Rectangle right = new Rectangle((int)(bottomLeftCorner.X + width), (int)(bottomLeftCorner.Y), 1, (int)height);
            Rectangle bottom = new Rectangle((int)(bottomLeftCorner.X), (int)(bottomLeftCorner.Y), (int)width, 1);
            Rectangle left = new Rectangle((int)(bottomLeftCorner.X), (int)(bottomLeftCorner.Y), 1, (int)height);

            Texture2D pixel = RvDebug.getPixel();

            spriteBatch.Draw(pixel, top, Color.LimeGreen);
            spriteBatch.Draw(pixel, right, Color.LimeGreen);
            spriteBatch.Draw(pixel, bottom, Color.LimeGreen);
            spriteBatch.Draw(pixel, left, Color.LimeGreen);
        }

        public override Rectangle getTextureRectangle()
        {
            return new Rectangle((int)bottomLeftCorner.X, (int)bottomLeftCorner.Y, (int)width, (int)height);
        }

        public float getWidth()
        {
            return width;
        }
        public float getHeight()
        {
            return height;
        }
    }
}