using System.Collections.Generic;

public class RvMiscDrawableHandler : RvDrawableI
{
    private static RvMiscDrawableHandler instance = null;
    private static object padlock = new object();

    private List<RvDrawableI> drawableIs = new List<RvDrawableI>();

    private RvMiscDrawableHandler()
    {

    }

    public static RvMiscDrawableHandler the()
    {
        if (instance == null)
        {
            lock(padlock)
            {
                if(instance == null)
                {
                    instance = new RvMiscDrawableHandler();
                }
            }
        }
        return instance;
    }

    public void Draw()
    {
        for (int i=0; i<drawableIs.Count; i++)
        {
            drawableIs[i].Draw();
        }
    }

    public void addDrawable(RvDrawableI drawableI)
    {
        if (!drawableIs.Contains(drawableI))
        {
            drawableIs.Add(drawableI);
        }
    }

    public void removeDrawable(RvDrawableI drawableI)
    {
        drawableIs.Remove(drawableI);
    }
}