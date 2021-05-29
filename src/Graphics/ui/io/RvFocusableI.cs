using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

public interface RvFocusableI
{
    public Rectangle getFocusRegion();
    public void focusKeyEvent(Keys keys);

    public void focusMouseEvent(RvMouseEvent e)
    {
        if (getFocusRegion().Contains(e.X, e.Y))
        {
            RvFocusHandler.the().addFocusRequest(this);
        }
    }
}