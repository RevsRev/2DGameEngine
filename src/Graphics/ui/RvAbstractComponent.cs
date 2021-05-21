using Microsoft.Xna.Framework;

public abstract class RvAbstractComponent
{
    protected Rectangle bounds;

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
    
    public virtual void Update() {}
    public virtual void Draw() {}
}