using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework;

public class RvMouseEvent
{
    public int X;
    public int Y;
    public bool leftButton = false;
    public bool rightButton = false;

    public RvMouseEvent(RvMouse mouse, int X, int Y)
    {
        this.X = X;
        this.Y = Y;

        this.leftButton = mouse.leftButton == RvMouse.BTN_MOUSE_CLICK_DOWN; //if we have a click or release event.
        this.rightButton = mouse.rightButton == RvMouse.BTN_MOUSE_CLICK_DOWN;
    }

    public Vector2 getCoords()
    {
        return new Vector2(X, Y);
    }
}