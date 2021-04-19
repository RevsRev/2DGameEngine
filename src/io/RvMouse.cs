using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework;
using System.Collections.Generic;
using System.Windows.Input;
using System;

public class RvMouse
{
    private static RvMouse instance = null;

    List<RvMouseListenerI> listeners = new List<RvMouseListenerI>();

    //singleton.
    private RvMouse() 
    {
    }

    public static RvMouse the()
    {
        if (instance == null)
        {
            instance = new RvMouse();
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

        for (int i=0; i<listeners.Count; i++)
        {
            listeners[i].mouseEvent(e);
        }
    }   
}