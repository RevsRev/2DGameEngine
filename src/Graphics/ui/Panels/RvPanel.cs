using Microsoft.Xna.Framework;
using System.Collections.Generic;

public class RvPanel : RvAbstractComponent
{
    private List<RvAbstractComponent> components = new List<RvAbstractComponent>();

    public RvPanel(Rectangle bounds) : base(bounds)
    {
    }

    public override void Draw(RvSpriteBatch spriteBatch)
    {
        spriteBatch.DrawRectangle(bounds, Color.White, 0.0f); //todo - ui layer depths...
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

    public void addComponent(RvAbstractComponent component)
    {
        components.Add(component);
    }
}