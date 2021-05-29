using System.Collections.Generic;
using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

public class RvPopupMenu : RvDisposable, IObserver<string>, RvMouseListenerI, RvDrawableI
{
    private List<RvPopupMenuItem> menuItems = new List<RvPopupMenuItem>();
    private RvPopupMenuListenerI actionListener = null;

    private Rectangle menuBounds = Rectangle.Empty;

    public RvPopupMenu()
    {
        RvMouse.the().addMouseListener(this);
        RvMiscDrawableHandler.the().addDrawable(this);
    }

    public void dispose()
    {
        for (int i=0; i<menuItems.Count; i++)
        {
            menuItems[i].dispose();
        }
        actionListener = null;
        RvMouse.the().removeMouseListener(this);
        RvMiscDrawableHandler.the().removeDrawable(this);
    }

    public void Draw()
    {
        RvAbstractDrawer drawer = RvUiDrawer.the();

        drawer.Begin(SpriteSortMode.BackToFront, null);
        for (int i=0; i<menuItems.Count; i++)
        {
            menuItems[i].Draw(drawer);
        }
        drawer.End();
    }

    public void mousePosition(int x, int y)
    {
        if (!menuBounds.Contains(x,y))
        {
            dispose();
        }
    }

    public void addPopupMenuItem(string text)
    {
        menuItems.Add(new RvPopupMenuItem(text));
    }

    public void buildMenu(int x, int y)
    {
        //testing
        x-= 20;
        y-= 20;

        int yVal = y;
        int width = 0;

        for (int i=0; i<menuItems.Count; i++)
        {
            menuItems[i].setOffset(new Vector2(x, yVal));
            yVal += menuItems[i].getHeight();

            if (width == 0)
            {
                width = menuItems[i].getWidth();
            }
        }
        menuBounds = new Rectangle(x, y, width, yVal - y);
    }

    public void setActionListener(RvPopupMenuListenerI actionListener)
    {
        this.actionListener = actionListener;
    }

    public virtual void OnNext(String actionCommand)
    {
        actionListener.performPopupMenuAction(actionCommand);
    }

    public virtual void OnError(Exception exception)
    {
        //no implementation (yet)
    }
    public virtual void OnCompleted()
    {
        //also unimplemented!
    }
}