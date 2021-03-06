using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

public class RvButtonImage : RvAbstractButton<string>
{
    Texture2D image;

    public RvButtonImage(string actionStr, Rectangle bounds, string imagePath) : base(actionStr, bounds)
    {
        image = RvContentHandler.the().getImage(imagePath);
    }

    public override void Draw(RvAbstractDrawer drawer)
    {
        drawer.Draw(image, getDrawingRegion(), Color.White);
    }
}