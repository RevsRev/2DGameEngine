using System.Collections.Generic;
using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

public class RvPopupMenu : RvDisposableI, IObserver<string>, RvMouseListenerI, RvDrawableI
{
    private List<RvPopupMenuItem> menuItems = new List<RvPopupMenuItem>();
    private RvPopupMenuListenerI actionListener = null;

    private Rectangle menuBounds = Rectangle.Empty;
    private int clickedX = 0;
    private int clickedY = 0;

    public RvPopupMenu()
    {
        RvMouse.the().addMouseListener(this);
        RvMiscDrawableHandler.the().addDrawable(this);
        RvPopupHandler.the().addPopupMenu(this);
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
        RvPopupHandler.the().removePopupMenu(this);
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
        RvPopupMenuItem pMenu = new RvPopupMenuItem(text);
        pMenu.Subscribe(this);
        menuItems.Add(pMenu);
    }

    public void buildMenu(int x, int y)
    {
        clickedX = x;
        clickedY = y;

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
    public RvPopupMenuListenerI getActionLisener()
    {
        return actionListener;
    }

    public virtual void OnNext(String actionCommand)
    {
        actionListener.performPopupMenuAction(actionCommand);
        dispose(); //make the menu disappear once we've clicked an option.
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