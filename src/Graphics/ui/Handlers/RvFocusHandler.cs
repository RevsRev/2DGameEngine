using System.Collections.Generic;
using Microsoft.Xna.Framework.Input;

public class RvFocusHandler : RvKeyboardListenerI, RvMouseListenerI
{
    private static RvFocusHandler instance;
    private static readonly object padlock = new object();

    RvFocusableI focused = null;
    List<RvFocusableI> focusables = new List<RvFocusableI>();
    List<RvFocusableI> focusRequests = new List<RvFocusableI>();

    private RvFocusHandler()
    {
        RvMouse.the().addMouseListener(this);
        RvKeyboard.the().addKeyboardListener(this);
    }

    public bool respectsClickableRegion() {return false;}

    public static RvFocusHandler the()
    {
        if (instance == null)
        {
            lock(padlock)
            {
                if (instance == null)
                {
                    instance = new RvFocusHandler();
                }
            }
        }
        return instance;
    }

    public void addFocusable(RvFocusableI focusable)
    {
        if (!focusables.Contains(focusable))
        {
            focusables.Add(focusable);
        }
    }

    public void keyPressed(Keys key)
    {
        if (focused != null)
        {
            focused.focusKeyEvent(key);
        }
    }

    public void doClick(RvMouseEvent e)
    {
        for (int i=0; i<focusables.Count; i++)
        {
            focusables[i].focusMouseEvent(e);
        }
        doFocusRequests();
    }

    public void addFocusRequest(RvFocusableI focusable)
    {
        if (!focusRequests.Contains(focusable))
        {
            focusRequests.Add(focusable);
        }
    }
    private void doFocusRequests()
    {
        if (focusRequests.Count>0)
        {
            focused = focusRequests[0];
            focusRequests = new List<RvFocusableI>();
            return;
        }
        focused = null;
    }

    private void setFocused(RvFocusableI focusable)
    {
        focused = focusable;
    }
    public void removeFocus(RvFocusableI focusable)
    {
        if (focused == focusable)
        {
            focused = null;
        }
    }
    public bool isFocused(RvFocusableI focusable)
    {
        return focused == focusable;
    }
}