using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

public class RvSpriteBatch : RvAbstractDrawer
{
    //drawing layer
    public static readonly float DEFAULT_SPRITE_DRAWING_LAYER = 0.5F;

    //this is a singleton
    private static RvSpriteBatch instance = null;
    private static readonly object padlock = new object();

    private RvSpriteBatch(ContentManager content, GraphicsDevice graphics, Vector2 centre, int width, int height) : base(content, graphics, centre, width, height)
    {
        this.drawingLayer = DEFAULT_SPRITE_DRAWING_LAYER;
    }

    public static RvSpriteBatch the()
    {
        if (instance == null)
        {
            lock(padlock)
            {
                if (instance == null)
                {
                    instance = new RvSpriteBatch(RvGame.the().Content, RvGame.the().GraphicsDevice, new Vector2(RvSystem.SCR_WIDTH/2, RvSystem.SCR_HEIGHT/2), RvSystem.SCR_WIDTH, RvSystem.SCR_HEIGHT);
                }
            }
        }
        return instance;
    }
}