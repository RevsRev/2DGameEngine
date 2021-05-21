using Microsoft.Xna.Framework;
using System.Collections.Generic;

public abstract class RvAbstractPanel : RvAbstractComponent
{
    protected List<RvAbstractComponent> components = new List<RvAbstractComponent>();
    private Color color = Color.LightGray;

    public RvAbstractPanel(Rectangle bounds) : base(bounds)
    {
    }

    public override void Draw()
    {
        RvUiDrawer.the().DrawRectangle(bounds, color, 0.0f); //todo - ui layer depths...
        for (int i=0; i<components.Count; i++)
        {
            components[i].Draw();
        }
    }
    public override void Update()
    {
        for (int i=0; i<components.Count; i++)
        {
            components[i].Update();
        }
    }

    public abstract override void unInit();

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