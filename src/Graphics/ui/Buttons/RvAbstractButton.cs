using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;

public class RvAbstractButton<T> : RvAbstractComponent, IObservable<T>
{
    //Keep track of who's listening to the button
    protected List<IObserver<T>> observers = new List<IObserver<T>>();
    protected T message;

    //To prevent the button firing too many events
    private static readonly float MIN_TIME_BETWEEN_CLICKS = 0.1f;
    private float lastClick = 0.0f;

    public RvAbstractButton(T message, Rectangle bounds) : base(bounds)
    {
        this.message = message;
    }

    public override void unInit()
    {
        //to do
    }

    public override void Draw()
    {
        //just the rectangle of the button.
        RvUiDrawer.the().DrawRectangle(bounds, Color.Gray);
        RvUiDrawer.the().DrawRectangleBorder(bounds, Color.Black);
    }

    public bool enteredButton(int cursorX, int cursorY)
    {
        return bounds.X < cursorX && cursorX < bounds.X + bounds.Width
            && bounds.Y < cursorY && cursorY < bounds.Y + bounds.Height;
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
        lastClick += (float)gameTime.ElapsedGameTime.TotalSeconds;
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