using Microsoft.Xna.Framework;

public class RvAbstractComponent
{
    protected Rectangle bounds;
    protected bool focus = false;

    public RvAbstractComponent(Rectangle bounds)
    {
        this.bounds = bounds;
    }

    public bool getFocus() { return focus; }
    public void setFocus(bool focus) { this.focus = focus; }

    public virtual void Update(GameTime gameTime) {}
    public virtual void Draw(RvSpriteBatch spriteBatch) {}
}