using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using System;

public class RvMenu : IObserver<string>, IObservable<string>
{
    public static readonly float MIN_TIME_BETWEEN_CLICKS_SECONDS = 0.1f;

    private List<IObserver<string>> observers = new List<IObserver<string>>();

    private List<RvButtonText> buttons;
    private Vector2 position;

    private int buttonHeight = 20;
    private int buttonWidth = 100;

    private bool visible = false;
    private float lastClick = 0.0f;

    private RvMenu(List<RvButtonText> buttons)
    {
        this.buttons = buttons;
    }

    public RvMenu() : this(new List<RvButtonText>())
    {
    }

    public void addButton(string buttonText)
    {
        int numButtons = buttons.Count;
        RvButtonText newButton = new RvButtonText(buttonText, new Rectangle((int)position.X, (int)position.Y + numButtons*buttonHeight, buttonWidth, buttonHeight));
        newButton.Subscribe(this);
        buttons.Add(newButton);
    }

    public void Draw(RvSpriteBatch spriteBatch)
    {
        if (!visible)
        {
            return;
        }

        for (int i=0; i<buttons.Count; i++)
        {
            buttons[i].Draw(spriteBatch);
        }
    }

    public void Update(GameTime gameTime)
    {
        lastClick = (float)Math.Min(lastClick + (float)gameTime.ElapsedGameTime.TotalSeconds, MIN_TIME_BETWEEN_CLICKS_SECONDS);

        MouseState mouse = Mouse.GetState();
        if (mouse.RightButton == ButtonState.Pressed && lastClick >= MIN_TIME_BETWEEN_CLICKS_SECONDS)
        {
            position.X = mouse.X;
            position.Y = mouse.Y;

            updateButtonPositions();

            //todo - method to update button positions!
            visible = !visible;
            lastClick = 0.0f;
        }
        // if (mouse.LeftButton == ButtonState.Pressed && visible)
        // {
        //     visible = false;
        // }

        for (int i=0; i<buttons.Count; i++)
        {
            buttons[i].Update(gameTime);
        }
    }

    private void updateButtonPositions()
    {
        for (int i=0; i<buttons.Count; i++)
        {
            buttons[i].setPosition((int)position.X, (int)position.Y + buttonHeight*i);
        }
    }

    //fire the action command all the way to the object we add the menu to (at the moment, this is just the editor!)
    public virtual void OnNext(String actionCommand)
    {
        if (!visible)
        {
            return;
        }
        foreach (var observer in observers)
        {
            observer.OnNext(actionCommand);
        }
    }

    public virtual void OnError(Exception exception)
    {
        //no implementation (yet)
    }
    public virtual void OnCompleted()
    {
        //also unimplemented!
    }

    public IDisposable Subscribe(IObserver<string> observer)
    {
        //Check if we've already added the button to the observer. If not, then add it.
        if (!observers.Contains(observer))
        {
            observers.Add(observer);
        }
        return new Unsubscriber<string>(observers, observer);
    }

    public Vector2 getPosition()
    {
        return position;
    }
}