using Microsoft.Xna.Framework.Input;

public class RvMouseEvent
{
    public const int NO_MOUSE_BUTTON_PRESSED = 0;
    public const int LEFT_MOUSE_BUTTON_PRESSED = 1;
    public const int RIGHT_MOUSE_BUTTON_PRESSED = 2;

    public int X;
    public int Y;
    public int pressed = NO_MOUSE_BUTTON_PRESSED;

    public RvMouseEvent(MouseState mouse)
    {
        X = mouse.X;
        Y = mouse.Y;

        if (mouse.LeftButton == ButtonState.Pressed)
        {
            pressed = LEFT_MOUSE_BUTTON_PRESSED;
        }
        else if (mouse.RightButton == ButtonState.Pressed)
        {
            pressed = RIGHT_MOUSE_BUTTON_PRESSED;
        }
    }
}