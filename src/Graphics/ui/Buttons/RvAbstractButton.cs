using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;

public class RvAbstractButton<T> : RvAbstractComponent, IObservable<T>, RvMouseListenerI
{
    //statics
    private static readonly float CLICK_INCREMENT = 1.0f;

    //Keep track of who's listening to the button
    protected List<IObserver<T>> observers = new List<IObserver<T>>();
    protected T message;

    //To prevent the button firing too many events
    private static readonly float MIN_TIME_BETWEEN_CLICKS = 20.0f;
    private float lastClick = 0.0f;

    public RvAbstractButton(T message, Rectangle bounds) : base(bounds)
    {
        this.message = message;
        RvMouse.the().addMouseListener(this);
    }

    public override void dispose()
    {
        RvMouse.the().removeMouseListener(this);
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

    public override void Update(GameTime gameTime)
    {
        MouseState mouse = Mouse.GetState();
        if (mouse.LeftButton == ButtonState.Pressed && lastClick > MIN_TIME_BETWEEN_CLICKS)
        {
            if (enteredButton(mouse.X, mouse.Y))
            {
                buttonPressed();
            }
            lastClick = 0.0f;
        }
        lastClick += CLICK_INCREMENT;
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