using Microsoft.Xna.Framework;

public class RvButtonText : RvAbstractButton<string>
{
    private float fontSize = 15.0f;

    public RvButtonText(string text, Rectangle bounds) : base(text, bounds)
    {
    }
    public RvButtonText(string text, float fontSize, Rectangle bounds) : this(text, bounds)
    {
        this.fontSize = fontSize;
    }

    public override void Draw(RvAbstractDrawer drawer)
    {
        drawer.DrawString(message, new Vector2(getDrawingRegion().X, getDrawingRegion().Y), fontSize);
        base.Draw(drawer);
    }
}