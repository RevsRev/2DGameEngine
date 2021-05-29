using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework;
using System.Collections.Generic;
using System.Windows.Input;
using System;

public class RvMouse
{
    private static RvMouse instance = null;
    private static readonly object padlock = new object();

    List<RvMouseListenerI> listeners = new List<RvMouseListenerI>();

    //used for dragging objects. The anchor point defines where the object was clicked in relation to the top left corner of its anchor region.
    private RvMouseListenerI boundObject = null;
    private Vector2 anchorPoint = Vector2.Zero;

    public const int BTN_MOUSE_IDLE = 0;
    public const int BTN_MOUSE_CLICK_DOWN = 1;
    public const int BTN_MOUSE_HELD = 2;
    public const int BTN_MOUSE_CLICK_RELEASE = 3;

    public int leftButton = BTN_MOUSE_IDLE;
    public int rightButton = BTN_MOUSE_IDLE;

    //singleton.
    private RvMouse() 
    {
    }

    public static RvMouse the()
    {
        if (instance == null)
        {
            lock(padlock)
            {
                if (instance == null)
                {
                    instance = new RvMouse();
                }
            }
        }
        return instance;
    }

    private void click(ButtonState buttonState, ref int btn)
    {
        if (buttonState == ButtonState.Pressed)
        {
            if (btn == BTN_MOUSE_IDLE
              || btn == BTN_MOUSE_CLICK_RELEASE)
            {
                btn = BTN_MOUSE_CLICK_DOWN;
            }
            else
            {
                btn = BTN_MOUSE_HELD;
            }
        }

        else if (buttonState == ButtonState.Released)
        {
            if (btn == BTN_MOUSE_CLICK_DOWN
              || btn == BTN_MOUSE_HELD)
            {
                btn = BTN_MOUSE_CLICK_RELEASE;
            }
            else
            {
                btn = BTN_MOUSE_IDLE;
            }
        }
    }

    public void addMouseListener(RvMouseListenerI listener)
    {
        if (!listeners.Contains(listener))
        {
            listeners.Add(listener);
        }
    }
    public void removeMouseListener(RvMouseListenerI listenerI)
    {
        listeners.Remove(listenerI);
    }

    public void Update(GameTime gameTime)
    {
        MouseState mouseState = Mouse.GetState();
        click(mouseState.LeftButton, ref leftButton);
        click(mouseState.RightButton, ref rightButton);
        updateBoundObject(mouseState);

        fireEvents(mouseState);
        updatePosition(mouseState);
    }   

    private void updatePosition(MouseState mouseState)
    {
        for (int i=0; i<listeners.Count; i++)
        {
            listeners[i].mousePosition(mouseState.X, mouseState.Y);
        }
    }

    private void fireEvents(MouseState mouseState)
    {
        //TODO - Build on this for firing right button events, etc.

        if ((leftButton != BTN_MOUSE_CLICK_DOWN && leftButton != BTN_MOUSE_CLICK_RELEASE)
          && (rightButton != BTN_MOUSE_CLICK_DOWN && rightButton != BTN_MOUSE_CLICK_RELEASE))
        {
            //not interested in holding/idle mouse.
            return;
        }

        RvMouseEvent e = new RvMouseEvent(this, mouseState.X, mouseState.Y);

        for (int i=0; i<listeners.Count; i++)
        {
            listeners[i].mouseEvent(e);
        }
    }

    private void updateBoundObject(MouseState mouseState)
    {
        if (leftButton == BTN_MOUSE_CLICK_RELEASE || leftButton == BTN_MOUSE_IDLE)
        {
            unBind();
        }

        if (!isBound())
        {
            return;
        }
        boundObject.doDrag(new Vector2(mouseState.X, mouseState.Y), anchorPoint);
    }

    public bool isBound()
    {
        return boundObject != null;
    }
    public void unBind()
    {
        boundObject = null;
        anchorPoint = Vector2.Zero;
    }
    public void bind(RvMouseListenerI toBind, Vector2 anchorPoint)
    {
        boundObject = toBind;
        this.anchorPoint = anchorPoint;
    }
}