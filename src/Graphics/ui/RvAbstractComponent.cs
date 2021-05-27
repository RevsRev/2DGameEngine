using Microsoft.Xna.Framework;

public abstract class RvAbstractComponent
{
    protected Rectangle bounds;
    protected float initialDrawingLayer = RvUiConstantsI.DRAWING_LAYER_DEFAULT;
    protected int layerNum = 0; //for putting components within components...

    public RvAbstractComponent(Rectangle bounds)
    {
        this.bounds = bounds;
    }

    public abstract void unInit();

    public Rectangle getBounds()
    {
        return bounds;
    }
    public void setBounds(Rectangle bounds)
    {
        this.bounds = bounds;
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
    
    public virtual void Update(GameTime gameTime) {}
    public virtual void Draw(RvAbstractDrawer drawer) {}
}