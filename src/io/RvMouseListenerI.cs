using Microsoft.Xna.Framework;

public interface RvMouseListenerI
{
    public void mouseEvent(RvMouseEvent e)
    {
        doClick(e.clicked, e.X, e.Y);
        doHeld(e.held, e.X, e.Y);
    }

    public virtual void doClick(int btnClicked, int X, int Y) {}

    public virtual Rectangle getAnchorRegion() {return Rectangle.Empty;}
    public virtual void doDrag(Vector2 mouseCoords, Vector2 anchorPoint) {}

    private void doHeld(int btnHeld, int mouseX, int mouseY)
    {
        if (!ableToBind(btnHeld, mouseX, mouseY))
        {
            return;
        }

        Rectangle anchor = getAnchorRegion();
        Vector2 anchorPoint = new Vector2(mouseX-anchor.X, mouseY-anchor.Y);
        RvMouse.the().bind(this, anchorPoint);
    }

    private bool ableToBind(int btnHeld, int mouseX, int mouseY)
    {
        if (btnHeld == RvMouseEvent.NO_MOUSE_BUTTON_HELD)
        {
            return false;
        }

        if (RvMouse.the().isBound())
        {
            return false;
        }

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