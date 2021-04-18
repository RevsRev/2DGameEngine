using Microsoft.Xna.Framework;

public class RvAbstractComponent
{
    protected Rectangle bounds;

    public RvAbstractComponent(Rectangle bounds)
    {
        this.bounds = bounds;
    }
    
    public virtual void Update(GameTime gameTime) {}
    public virtual void Draw(RvSpriteBatch spriteBatch) {}
}