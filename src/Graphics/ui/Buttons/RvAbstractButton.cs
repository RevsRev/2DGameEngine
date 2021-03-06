using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;

public class RvAbstractButton<T> : RvAbstractComponent, IObservable<T>, RvMouseListenerI
{
    //Keep track of who's listening to the button
    protected List<IObserver<T>> observers = new List<IObserver<T>>();
    protected T message;

    public RvAbstractButton(T message, Rectangle bounds) : base(bounds)
    {
        this.message = message;
    }

    public override void init()
    {
        RvMouse.the().addListener(this);
    }

    public override void dispose()
    {
        RvMouse.the().removeListener(this);
    }

    public Rectangle getClickableRegion()
    {
        return getDrawingRegion();
    }
    public void doClick(RvMouseEvent e)
    {
        if (e.leftButton)
        {
            if (enteredButton(e.X, e.Y))
            {
                buttonPressed();
            }
        }
    }

    public override void Draw(RvAbstractDrawer drawer)
    {
        //just the rectangle of the button.
        drawer.DrawRectangle(getDrawingRegion(), Color.Gray);
        drawer.DrawRectangleBorder(getDrawingRegion(), Color.Black);
    }

    public bool enteredButton(int cursorX, int cursorY)
    {
        Rectangle visibleRegion = getDrawingRegion();

        return visibleRegion.X < cursorX && cursorX < visibleRegion.X + visibleRegion.Width
            && visibleRegion.Y < cursorY && cursorY < visibleRegion.Y + visibleRegion.Height;
    }

    public void buttonPressed()
    {
        foreach (var observer in observers)
        {
            observer.OnNext(message);
        }
    }

    public void setPosition(int x, int y)
    {
        bounds.X = x;
        bounds.Y = y;
    }

    public int getHeight()
    {
        return bounds.Height;
    }
    public int getWidth()
    {
        return bounds.Width;
    }

    public IDisposable Subscribe(IObserver<T> observer)
    {
        //Check if we've already added the button to the observer. If not, then add it.
        if (!observers.Contains(observer))
        {
            observers.Add(observer);
        }
        return new Unsubscriber<T>(observers, observer);
    }
}

public class Unsubscriber<T> : IDisposable
{
    private List<IObserver<T>> observers;
    private IObserver<T> observer;

    internal Unsubscriber(List<IObserver<T>> observers, IObserver<T> observer)
    {
        this.observer = observer;
        this.observers = observers;
    }

    public void Dispose()
    {
        if (observers.Contains(observer))
        {
            observers.Remove(observer);
        }
    }
}