using Microsoft.Xna.Framework;

public abstract class RvAbstractComponent : RvDisposable
{
    protected Rectangle bounds;
    protected Vector2 offest = Vector2.Zero;
    protected float initialDrawingLayer = RvUiConstantsI.DRAWING_LAYER_DEFAULT;
    protected int layerNum = 0; //for putting components within components...

    public RvAbstractComponent(Rectangle bounds)
    {
        this.bounds = bounds;
    }

    public abstract void dispose();

    public Rectangle getBounds()
    {
        return bounds;
    }
    public void setBounds(Rectangle bounds)
    {
        this.bounds = bounds;
    }
    public Vector2 getOffset()
    {
        return offest;
    }
    public void setOffset(Vector2 offset)
    {
        this.offest = offset;
    }
    public Rectangle getDrawingRegion()
    {
        return new Rectangle((int)(bounds.X+offest.X), (int)(bounds.Y+offest.Y), bounds.Width, bounds.Height);
    }

    public int getLayerNum()
    {
        return layerNum;
    }
    public void setLayerNum(int layerNum)
    {
        this.layerNum = layerNum;
    }
    public float getInitialDrawingLayer()
    {
        return initialDrawingLayer;
    }
    public void setInitialDrawingLayer(float initialDrawingLayer)
    {
        this.initialDrawingLayer = initialDrawingLayer;
    }

    public float getDrawingLayer()
    {
        return initialDrawingLayer - (float)RvSequencesAndSeries.geometricSum(0.01f, 0.1f, layerNum);
    }

    public virtual void move(Vector2 pos)
    {
        bounds.X = (int)pos.X;
        bounds.Y = (int)pos.Y;
    }
    
    public virtual void Update(GameTime gameTime) {}
    public virtual void Draw(RvAbstractDrawer drawer) {}
}