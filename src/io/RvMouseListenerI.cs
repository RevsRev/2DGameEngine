using Microsoft.Xna.Framework;

public interface RvMouseListenerI : RvEventListenerI
{
    public void mouseEvent(RvMouseEvent e) //for mouse click/held events (in future do scroll as well)
    {
        onClick(e);
        doBinding(e);
    }

    public virtual void mousePosition(int x, int y) {} //for things that always need to know where the mouse is
    public virtual Rectangle getClickableRegion() {return Rectangle.Empty;} //region to click in
    public virtual void doDrag(Vector2 mouseCoords, Vector2 anchorPoint) {} //given the mouse coords and where we clicked in relation to the anchor region, drag the mouseListenerI
    public virtual void doClick(RvMouseEvent e) {} //if we click, then do stuff.
    public virtual bool respectsClickableRegion() {return true;}

    private void onClick(RvMouseEvent e)
    {
        if (!withinClickableRegion(e.X, e.Y))
        {
            return;
        }
        doClick(e);
    }

    private void doBinding(RvMouseEvent e)
    {
        if (RvMouse.the().isBound())
        {
            return;
        }

        //this property should only be used for generic right click options anywhere on the screen. Any object that needs to be dragged must have a clickable region.
        if (!respectsClickableRegion())
        {
            return;
        }

        if (!withinClickableRegion(e.X, e.Y))
        {
            return;
        }

        //you have to click on an object before dragging it.
        if (!e.leftButton)
        {
            return;
        }

        Rectangle anchor = getClickableRegion();
        Vector2 anchorPoint = new Vector2(e.X-anchor.X, e.Y-anchor.Y);
        RvMouse.the().bind(this, anchorPoint);
    }

    private bool withinClickableRegion(int mouseX, int mouseY)
    {
        if (!respectsClickableRegion())
        {
            return true;
        }

        Rectangle clickableRegion = getClickableRegion();
        if (clickableRegion == Rectangle.Empty)
        {
            return false;
        }

        if (!clickableRegion.Contains(mouseX, mouseY))
        {
            return false;
        }

        return true;
    }
}