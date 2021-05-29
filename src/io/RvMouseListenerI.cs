using Microsoft.Xna.Framework;

public interface RvMouseListenerI
{
    public void mouseEvent(RvMouseEvent e)
    {
        doClick(e);
        doBinding(e);
    }

    public virtual Rectangle getAnchorRegion() {return Rectangle.Empty;}
    public virtual void doDrag(Vector2 mouseCoords, Vector2 anchorPoint) {}
    public virtual void doClick(RvMouseEvent e) {}

    private void doBinding(RvMouseEvent e)
    {
        if (RvMouse.the().isBound())
        {
            return;
        }

        if (!withinAnchorRegion(e.X, e.Y))
        {
            return;
        }

        //you have to click on an object before dragging it.
        if (!e.leftButton)
        {
            return;
        }

        Rectangle anchor = getAnchorRegion();
        Vector2 anchorPoint = new Vector2(e.X-anchor.X, e.Y-anchor.Y);
        RvMouse.the().bind(this, anchorPoint);
    }

    private bool withinAnchorRegion(int mouseX, int mouseY)
    {
        Rectangle anchorRegion = getAnchorRegion();
        if (anchorRegion == Rectangle.Empty)
        {
            return false;
        }

        if (!anchorRegion.Contains(mouseX, mouseY))
        {
            return false;
        }

        return true;
    }
}