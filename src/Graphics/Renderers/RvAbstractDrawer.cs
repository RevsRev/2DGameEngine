using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using System.Collections.Generic;
using System;

public abstract class RvAbstractDrawer : SpriteBatch
{
    //used to define the region within which the bound object can move before we start scrolling
    public static readonly float BINDING_WIDTH_RATIO = 0.9f;
    public static readonly float BINDING_HEIGHT_RATIO = 0.9f;

    public static readonly float DEFAULT_DRAWING_LAYER = 0.5f;

    //used for drawing buttons/menus
    public static Dictionary<int, Texture2D> pens = new Dictionary<int, Texture2D>();
    public static readonly int PEN_WHITE_PIXEL      = 1;

    public static Dictionary<int, RvAbstractFont> fonts = new Dictionary<int, RvAbstractFont>();
    public static readonly int FONT_FANTASY         = 1;
    public static readonly int FONT_THEANO_DIDOT    = 2;

    //Defines the region within which we want things to be drawn.
    protected int width;
    protected int height;
    protected Vector2 centre;
    protected float drawingLayer = DEFAULT_DRAWING_LAYER;

    protected RvAbstractDrawer(ContentManager content, GraphicsDevice graphics, Vector2 centre, int width, int height) : base(graphics)
    {
        this.centre = centre;
        this.width = width;
        this.height = height;

        initPens(content);
        initFonts(content);
    }

    //If we want to follow a particular object (i.e. side scrolling for a platformer!)
    private RvPhysicalObject boundObject;

    public void initPens(ContentManager content)
    {
        pens[PEN_WHITE_PIXEL] = content.Load<Texture2D>(RvContentFiles.DRAWING + "white_pixel");
    }
    public void initFonts(ContentManager content)
    {
        fonts[FONT_FANTASY] = RvFantasyFont.factory(content);
        fonts[FONT_THEANO_DIDOT] = RvTheanoDidotFont.factory(content);
    }

    public void setBoundObject(RvPhysicalObject boundObject)
    {
        this.boundObject = boundObject;
    }
    public void unBindObject()
    {
        setBoundObject(null);
    }

    public void Update(GameTime gameTime)
    {
        if (boundObject == null)
        {
            return;
        }

        Vector2 bObjPos = boundObject.getPosition();
        int objMinX = (int)bObjPos.X;
        int objMaxX = objMinX + (int)boundObject.getHitboxWidth();
        int objMinY = (int)bObjPos.Y;
        int objMaxY = objMinY + (int)boundObject.getHitboxHeight();

        int boundMinX = (int)(centre.X - BINDING_WIDTH_RATIO*width/2);
        int boundMaxX = (int)(centre.X + BINDING_WIDTH_RATIO*width/2);
        int boundMinY = (int)(centre.Y - BINDING_HEIGHT_RATIO*height/2);
        int boundMaxY = (int)(centre.Y + BINDING_HEIGHT_RATIO*height/2);

        if (objMinX < boundMinX)
        {
            centre.X += objMinX - boundMinX;
        }
        else if (objMaxX > boundMaxX)
        {
            centre.X += objMaxX - boundMaxX;
        }

        if (objMinY < boundMinY)
        {
            centre.Y += objMinY - boundMinY;
        }
        else if (objMaxY > boundMaxY)
        {
            centre.Y += objMaxY - boundMaxY;
        }
    }

    private Tuple<bool, Rectangle, Rectangle> clipToScreen(Rectangle destinationRectangle, Rectangle sourceRectangle)
    {
        int minCanvasX = (int)(centre.X - width/2);
        int maxCanvasX = (int)(centre.X + width/2);
        int minCanvasY = (int)(centre.Y - height/2);
        int maxCanvasY = (int)(centre.Y + height/2);

        int destClippedMinX = Math.Max(minCanvasX, destinationRectangle.X);
        int destClippedMaxX = Math.Min(maxCanvasX, destinationRectangle.X + destinationRectangle.Width);
        int destClippedMinY = Math.Max(minCanvasY, destinationRectangle.Y);
        int destClippedMaxY = Math.Min(maxCanvasY, destinationRectangle.Y + destinationRectangle.Height);

        if (destClippedMinX >= destClippedMaxX)
        {
            return new Tuple<bool, Rectangle, Rectangle>(false, new Rectangle(0,0,0,0), new Rectangle(0,0,0,0));
        }
        if (destClippedMinY >= destClippedMaxY)
        {
            return new Tuple<bool, Rectangle, Rectangle>(false, new Rectangle(0,0,0,0), new Rectangle(0,0,0,0));
        }
        
        int destClippedWidth = destClippedMaxX - destClippedMinX;
        int destClippedHeight = destClippedMaxY - destClippedMinY;

        //we need to clip the source rectangle in the same ratios as we did the destination rectangle, otherwise images will get distorted.
        int sourceClippedX = (int)(sourceRectangle.X + sourceRectangle.Width * (double)(destClippedMinX - destinationRectangle.X)/destinationRectangle.Width);
        int sourceClippedWidth = (int)(sourceRectangle.Width * (double)destClippedWidth/destinationRectangle.Width);
        int sourceClippedY = (int)(sourceRectangle.Y + sourceRectangle.Height * (double)(destClippedMinY - destinationRectangle.Y)/destinationRectangle.Height);
        int sourceClippedHeight = (int)(sourceRectangle.Height * (double)destClippedHeight/destinationRectangle.Height);

        //Now that the clipping has been worked out, we should map everything to screen space.
        //To do this, need to map top left corner of the canvas to (0,0). This is easy (just subtract minCanvasX and minCanvasY)
        Rectangle destClippedAndTranslatedRectangle = new Rectangle(destClippedMinX - minCanvasX, destClippedMinY - minCanvasY, destClippedWidth, destClippedHeight);
        Rectangle sourceClippedRectangle = new Rectangle(sourceClippedX, sourceClippedY, sourceClippedWidth, sourceClippedHeight);

        return new Tuple<bool, Rectangle, Rectangle>(true, destClippedAndTranslatedRectangle, sourceClippedRectangle);
    }

    public Vector2 getTopLeftGameCoords()
    {
        return new Vector2(centre.X - width/2, centre.Y - height/2);
    }
    
    new public void Draw(Texture2D texture, Rectangle destinationRectangle, Color color)
    {
        Rectangle sourceRectangle = new Rectangle(0,0,texture.Width, texture.Height);
        Draw(texture, destinationRectangle, sourceRectangle, color);
    }

    new public void Draw(Texture2D texture, Vector2 position, Color color)
    {
        //implement
        base.Draw(texture, position, color);
    }

    new public void Draw(Texture2D texture, Vector2 position, Rectangle? sourceRectangle, Color color)
    {
        //implement
        base.Draw(texture, position, sourceRectangle, color);
    }

    new public void Draw(Texture2D texture, Rectangle destinationRectangle, Rectangle? sourceRectangle, Color color)
    {
        if (sourceRectangle == null) //I haven't written the code to handle this yet.
        {
            base.Draw(texture, destinationRectangle, sourceRectangle, color);
        }

        Tuple<bool, Rectangle, Rectangle> clippedTuple = clipToScreen(destinationRectangle, (Rectangle)sourceRectangle);
        if (clippedTuple.Item1)
        {
            base.Draw(texture, clippedTuple.Item2, clippedTuple.Item3, color);
        }
    }

    public void Draw(Texture2D texture, Vector2 position, Rectangle? sourceRectangle, Color color, float rotation, Vector2 origin, float scale, SpriteEffects effects)
    {
        Draw(texture, position, sourceRectangle, color, rotation, origin, scale, effects, drawingLayer);
    }
    new public void Draw(Texture2D texture, Vector2 position, Rectangle? sourceRectangle, Color color, float rotation, Vector2 origin, float scale, SpriteEffects effects, float layerDepth)
    {
        //implement
        base.Draw(texture, position, sourceRectangle, color, rotation, origin, scale, effects, layerDepth);
    }

    public void Draw(Texture2D texture, Rectangle destinationRectangle, Rectangle? sourceRectangle, Color color, float rotation, Vector2 origin, SpriteEffects effects)
    {
        Draw(texture, destinationRectangle, sourceRectangle, color, rotation, origin, effects, drawingLayer);
    }
    new public void Draw(Texture2D texture, Rectangle destinationRectangle, Rectangle? sourceRectangle, Color color, float rotation, Vector2 origin, SpriteEffects effects, float layerDepth)
    {
        if (sourceRectangle == null) //I haven't written the code to handle this yet.
        {
            base.Draw(texture, destinationRectangle, sourceRectangle, color, rotation, origin, effects, layerDepth);
        }

        Tuple<bool, Rectangle, Rectangle> clippedTuple = clipToScreen(destinationRectangle, (Rectangle)sourceRectangle);
        if (clippedTuple.Item1)
        {
            base.Draw(texture, clippedTuple.Item2, clippedTuple.Item3, color, rotation, origin, effects, layerDepth);
        }
    }

    public void DrawRectangle(Rectangle destinationRectangle, Color color)
    {
        DrawRectangle(destinationRectangle, color, drawingLayer);
    }
    public void DrawRectangle(Rectangle destinationRectangle, Color color, float layerDepth)
    {
        base.Draw(pens[PEN_WHITE_PIXEL], destinationRectangle, null, color, 0.0f, Vector2.Zero, SpriteEffects.None, layerDepth);
    }

    public virtual void DrawRectangleBorder(Rectangle destinationRectangle, Color color)
    {
        DrawRectangle(destinationRectangle, color, drawingLayer);
    }
    public void DrawRectangleBorder(Rectangle destinationRectangle, Color color, float layerDepth)
    {
        Rectangle top = new Rectangle(destinationRectangle.X, destinationRectangle.Y + destinationRectangle.Height - 1, destinationRectangle.Width, 1);
        Rectangle left = new Rectangle(destinationRectangle.X, destinationRectangle.Y, 1, destinationRectangle.Height);
        Rectangle bottom = new Rectangle(destinationRectangle.X, destinationRectangle.Y, destinationRectangle.Width, 1);
        Rectangle right = new Rectangle(destinationRectangle.X + destinationRectangle.Width - 1, destinationRectangle.Y, 1, destinationRectangle.Height);

        DrawRectangle(top, color, layerDepth);
        DrawRectangle(left, color, layerDepth);
        DrawRectangle(right, color, layerDepth);
        DrawRectangle(bottom, color, layerDepth);
    }

    public virtual void DrawString(string text, Vector2 position, float size)
    {
        DrawString(text, position, size, drawingLayer);
    }

    public void DrawString(string text, Vector2 position, float size, float layerDepth)
    {
        Vector2 letterSize = fonts[FONT_THEANO_DIDOT].getLetterSize();
        float scale = size/letterSize.Y;

        DrawString(fonts[FONT_THEANO_DIDOT].createSpriteFont(), text, position, Color.Black, 0.0f, Vector2.Zero, scale, SpriteEffects.None, layerDepth);
    }
}