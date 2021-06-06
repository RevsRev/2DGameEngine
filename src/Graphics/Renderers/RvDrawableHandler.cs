using System.Collections.Generic;

public class RvDrawableHandler : RvDrawableI
{
    private static RvDrawableHandler instance = null;
    private static object padlock = new object();

    private List<RvDrawableI> drawableIs = new List<RvDrawableI>();

    private RvDrawableHandler()
    {

    }

    public static RvDrawableHandler the()
    {
        if (instance == null)
        {
            lock(padlock)
            {
                if(instance == null)
                {
                    instance = new RvDrawableHandler();
                }
            }
        }
        return instance;
    }

    public void draw()
    {
        for (int i=0; i<drawableIs.Count; i++)
        {
            drawableIs[i].draw();
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