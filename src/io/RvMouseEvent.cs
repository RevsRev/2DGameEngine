using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework;

public class RvMouseEvent
{
    public const int NO_MOUSE_BUTTON_CLICKED = 0;
    public const int LEFT_MOUSE_BUTTON_CLICKED = 1;
    public const int RIGHT_MOUSE_BUTTON_CLICKED = 2;

    public const int NO_MOUSE_BUTTON_HELD = 0;
    public const int LEFT_MOUSE_BUTTON_HELD = 1;
    public const int RIGHT_MOUSE_BUTTON_HELD = 2;

    public int X;
    public int Y;
    public int clicked = NO_MOUSE_BUTTON_CLICKED;
    public int held = NO_MOUSE_BUTTON_HELD;

    public RvMouseEvent(MouseState mouse)
    {
        X = mouse.X;
        Y = mouse.Y;

        //do clicked states
        if (mouse.LeftButton == ButtonState.Pressed)
        {
            click(LEFT_MOUSE_BUTTON_CLICKED);
        }
        else if (mouse.RightButton == ButtonState.Pressed)
        {
            click(RIGHT_MOUSE_BUTTON_CLICKED);
        }
        else
        {
            click(NO_MOUSE_BUTTON_CLICKED);
        }

        //do held states
        if (mouse.LeftButton == ButtonState.Pressed)
        {
            held = LEFT_MOUSE_BUTTON_HELD;
        }
        else if (mouse.RightButton  == ButtonState.Pressed)
        {
            held = RIGHT_MOUSE_BUTTON_HELD;
        }
        else
        {
            held = NO_MOUSE_BUTTON_HELD;
        }
    }

    private void click(int mouseBtn)
    {
        if (justClicked())
        {
            clicked = NO_MOUSE_BUTTON_CLICKED;
        }
        else
        {
            clicked = mouseBtn;
        }
    }

    private bool justClicked()
    {
        return clicked != NO_MOUSE_BUTTON_CLICKED;
    }

    public Vector2 getCoords()
    {
        return new Vector2(X, Y);
    }
}