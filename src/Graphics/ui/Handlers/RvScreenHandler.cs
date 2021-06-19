using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Threading.Tasks;
using System.Collections.Generic;
using System;

public sealed class RvScreenHandler : RvDrawableI
{
    private static RvScreenHandler instance;
    private static readonly object padlock = new object();

    private List<RvSAbstractScreen> screens = new List<RvSAbstractScreen>();

    private RvScreenHandler()
    {

    }

    public static RvScreenHandler the()
    {
        if (instance == null)
        {
            lock(padlock)
            {
                if (instance == null)
                {
                    instance = new RvScreenHandler();
                }
            }
        }
        return instance;
    }

    public void Update(GameTime gameTime)
    {
        for (int i=screens.Count-1; i>=0; i--)
        {
            RvSAbstractScreen screen = screens[i];
            screen.Update(gameTime);
            if (screen.isOkToFinish())
            {
                screen.doFinish();
                removeScreen(screen);
            }
        }
    }
    public void draw()
    {
        
        RvUiDrawer.the().Begin(SpriteSortMode.BackToFront, null);
        for (int i=screens.Count-1; i>=0; i--)
        {
            RvScreenI screen = screens[i];
            screen.Draw(RvUiDrawer.the());
        }
        RvUiDrawer.the().End();
    }

    public void addScreen(RvSAbstractScreen screen)
    {
        screens.Add(screen);
    }
    public void removeScreen(RvSAbstractScreen screen)
    {
        screens.Remove(screen);
    }

    public RvSAbstractScreen doPopup(String screenName)
    {
        RvSAbstractScreen screen = (RvSAbstractScreen)RvClassLoader.createByName(screenName);
        screen.init();
        RvScreenHandler.the().addScreen(screen);
        return screen;
    }
}