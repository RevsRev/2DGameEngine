using Microsoft.Xna.Framework;
using System.Collections.Generic;

public abstract class RvAbstractPanel : RvAbstractComponent
{
    protected List<RvAbstractComponent> components = new List<RvAbstractComponent>();
    private Color panelColor = Color.LightGray;
    private Color borderColor = Color.Black;

    public RvAbstractPanel(Rectangle bounds) : base(bounds)
    {
    }

    public override void init()
    {
    }

    public override void Draw(RvAbstractDrawer drawer)
    {
        drawer.DrawRectangle(getDrawingRegion(), panelColor, getDrawingLayer());
        drawer.DrawRectangleBorder(getDrawingRegion(), borderColor);
        for (int i=0; i<components.Count; i++)
        {
            components[i].Draw(drawer);
        }
    }
    public override void Update(GameTime gameTime)
    {
        for (int i=0; i<components.Count; i++)
        {
            components[i].Update(gameTime);
        }
    }
    public override void move(Vector2 delPos)
    {
        base.move(delPos);
        for (int i=0; i<components.Count; i++)
        {
            components[i].move(delPos);
        }
    }

    public override void dispose()
    {
        for (int i=0; i<components.Count; i++)
        {
            components[i].dispose();
        }
    }

    public virtual void addComponent(RvAbstractComponent component)
    {
        //Make sure that adding components is relative. CHANGED THIS 
        //Rectangle absoluteBounds = component.getBounds();
        //Rectangle relativeBounds = new Rectangle(absoluteBounds.X + bounds.X, absoluteBounds.Y + bounds.Y, absoluteBounds.Width, absoluteBounds.Height);
        //component.setBounds(relativeBounds);
        component.setOffset(component.getOffset() + getOffset()); //components offset + parents offset.
        component.setLayerNum(layerNum + 1);

        component.init();
        components.Add(component);
    }

    public void setPanelColor(Color panelColor)
    {
        this.panelColor = panelColor;
    }
}