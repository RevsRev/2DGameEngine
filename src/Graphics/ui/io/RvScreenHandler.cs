using System.Threading.Tasks;
using System.Collections.Generic;
using System;

public class RvScreenHandler
{
    private static RvScreenHandler instance;

    private List<RvScreenI> screens = new List<RvScreenI>();

    private RvScreenHandler()
    {

    }

    public static RvScreenHandler the()
    {
        if (instance == null)
        {
            instance = new RvScreenHandler();
        }
        return instance;
    }
    public void addScreen(RvScreenI screen)
    {
        screens.Add(screen);
    }
    public void removeScreen(RvScreenI screen)
    {
        screens.Remove(screen);
    }
    public void Draw(RvSpriteBatch spriteBatch)
    {
        foreach (var screen in screens)
        {
            screen.Draw(spriteBatch);
        }
    }


    public static async Task<T> doPopup<T>(String screenName)
    {
        RvScreenTypeI<T> screen = (RvScreenTypeI<T>)RvClassLoader.createByName(screenName);
        RvScreenHandler.the().addScreen(screen);
        return await screen.doPopup();
    }
}