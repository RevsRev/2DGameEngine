using System.Collections.Generic;

public class RvFocusHandler
{
    private static RvFocusHandler instance;
    private static readonly object padlock = new object();

    RvFocusableI focused = null;

    private RvFocusHandler()
    {
    }

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

    public void setFocused(RvFocusableI focusable)
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