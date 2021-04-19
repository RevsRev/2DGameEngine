using Microsoft.Xna.Framework;

public class RvDialogPanel : RvPanel
{
    private const int BANNER_HEIGHT = 20;

    RvPanel banner; //contains the title, X buttons, etc...
    RvPanel contentPanel; //for putting the actual content of the panel.

    public RvDialogPanel(Rectangle bounds) : base(bounds)
    {
        init();
    }

    public void init()
    {
        //I'm going to assume we put in sensible dimensions for the time being...
        Rectangle bannerRect = new Rectangle(bounds.X, bounds.Y, bounds.Width, BANNER_HEIGHT);
        banner = new RvPanel(bannerRect);
        banner.setColor(Color.DarkGray);

        Rectangle contentRect = new Rectangle(bounds.X, bounds.Y + BANNER_HEIGHT, bounds.Width, bounds.Height -BANNER_HEIGHT);
        contentPanel = new RvPanel(contentRect);

        components.Add(banner);
        components.Add(contentPanel);
    }

    public override void addComponent(RvAbstractComponent component)
    {
        contentPanel.addComponent(component);
    }

    public Rectangle getContentBounds()
    {
        return contentPanel.getBounds();
    }
}