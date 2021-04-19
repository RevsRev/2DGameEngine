using Microsoft.Xna.Framework;
using System.Collections.Generic;

public class RvPanel : RvAbstractComponent
{
    protected List<RvAbstractComponent> components = new List<RvAbstractComponent>();
    private Color color = Color.LightGray;

    public RvPanel(Rectangle bounds) : base(bounds)
    {
    }

    public override void Draw(RvSpriteBatch spriteBatch)
    {
        spriteBatch.DrawRectangle(bounds, color, 0.0f); //todo - ui layer depths...
        for (int i=0; i<components.Count; i++)
        {
            components[i].Draw(spriteBatch);
        }
    }
    public override void Update(GameTime gameTime)
    {
        //some code to update this component.

        //now update the children.
        for (int i=0; i<components.Count; i++)
        {
            components[i].Update(gameTime);
        }
    }

    public virtual void addComponent(RvAbstractComponent component)
    {
        //Make sure that adding components is relative.
        Rectangle absoluteBounds = component.getBounds();
        Rectangle relativeBounds = new Rectangle(absoluteBounds.X + bounds.X, absoluteBounds.Y + bounds.Y, absoluteBounds.Width, absoluteBounds.Height);
        component.setBounds(relativeBounds);

        components.Add(component);
    }

    public void setColor(Color color)
    {
        this.color = color;
    }
}