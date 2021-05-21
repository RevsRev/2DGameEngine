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

    public override void Draw()
    {
        RvUiDrawer.the().DrawString(message, new Vector2(bounds.X, bounds.Y), fontSize);
        base.Draw();
    }
}