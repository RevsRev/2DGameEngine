using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;

public class RvUiDrawer : RvAbstractDrawer
{
    //drawing layer
    public static readonly float DEFAULT_UI_DRAWING_LAYER = 0.2f;
    public static readonly float DEFAULT_UI_BORDER_LAYER = 0.1f;
    public static readonly float DEFAULT_UI_STRING_LAYER = 0.00f;

    //this is a singleton
    private static RvUiDrawer instance = null;
    private static readonly object padlock = new object();

    private RvUiDrawer(ContentManager content, GraphicsDevice graphics, Vector2 centre, int width, int height) : base(content, graphics, centre, width, height)
    {
        this.drawingLayer = DEFAULT_UI_DRAWING_LAYER;
    }

    public static RvUiDrawer the()
    {
        if (instance == null)
        {
            lock(padlock)
            {
                if (instance == null)
                {
                    instance = new RvUiDrawer(RvGame.the().Content, RvGame.the().GraphicsDevice, new Vector2(RvSystem.SCR_WIDTH/2, RvSystem.SCR_HEIGHT/2), RvSystem.SCR_WIDTH, RvSystem.SCR_HEIGHT);
                }
            }
        }
        return instance;
    }

    public override void DrawRectangleBorder(Rectangle destinationRectangle, Color color)
    {
        base.DrawRectangleBorder(destinationRectangle, color, DEFAULT_UI_BORDER_LAYER);
    }
    public override void DrawString(string text, Vector2 position, float size)
    {
        base.DrawString(text, position, size, DEFAULT_UI_STRING_LAYER);
    }
}