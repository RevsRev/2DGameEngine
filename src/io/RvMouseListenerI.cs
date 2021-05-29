using Microsoft.Xna.Framework;

public interface RvMouseListenerI
{
    public void mouseEvent(RvMouseEvent e) //for mouse click/held events (in future do scroll as well)
    {
        doClick(e);
        doBinding(e);
    }

    public virtual void mousePosition(int x, int y) {} //for things that always need to know where the mouse is
    public virtual Rectangle getAnchorRegion() {return Rectangle.Empty;} //region to click in
    public virtual void doDrag(Vector2 mouseCoords, Vector2 anchorPoint) {} //given the mouse coords and where we clicked in relation to the anchor region, drag the mouseListenerI
    public virtual void doClick(RvMouseEvent e) {} //if we click, then do stuff.

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