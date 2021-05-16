using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

public class RvContentHandler
{
    private static readonly object padlock = new object();
    private static RvContentHandler instance;

    private ContentManager content;

    private RvContentHandler()
    {
        content = RvGame.the().Content;
    }

    public static RvContentHandler the()
    {
        if (instance == null)
        {
            lock(padlock)
            {
                if (instance == null)
                {
                    instance = new RvContentHandler();
                }
            }
        }
        return instance;
    }

    public Texture2D getImage(string imagePath)
    {
        return content.Load<Texture2D>(imagePath);
    }
}