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

    public void addMouseListener(RvMouseListenerI listener)
    {
        if (!listeners.Contains(listener))
        {
            listeners.Add(listener);
        }
    }

    public void Update(GameTime gameTime)
    {
        RvMouseEvent e = new RvMouseEvent(Mouse.GetState());

        updateBoundObject(e);

        for (int i=0; i<listeners.Count; i++)
        {
            listeners[i].mouseEvent(e);
        }
    }   

    private void updateBoundObject(RvMouseEvent e)
    {
        if (e.held == RvMouseEvent.NO_MOUSE_BUTTON_HELD)
        {
            unBind();
        }

        if (!isBound())
        {
            return;
        }
        boundObject.doDrag(new Vector2(e.X, e.Y), anchorPoint);
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