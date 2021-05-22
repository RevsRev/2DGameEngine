using Microsoft.Xna.Framework;
using System;
using System.Threading.Tasks;

public abstract class RvSDialog<T> : RvSAbstractScreen, IObserver<string>
{
    private const int BANNER_HEIGHT = 20;

    RvAbstractPanel banner; //contains the title, X buttons, etc...
    RvAbstractPanel contentPanel; //for putting the actual content of the panel.

    protected RvSDialog(Rectangle bounds) : base(bounds)
    {
        init();
    }

    public override void unInit()
    {
        //unimplemented.
    }

    public override void init()
    {
        base.init();
        
        //I'm going to assume we put in sensible dimensions for the time being...
        Rectangle bannerRect = new Rectangle(bounds.X, bounds.Y, bounds.Width, BANNER_HEIGHT);
        banner = new RvPanel(bannerRect);
        banner.setColor(Color.DarkGray);
        RvButtonImage xButton = new RvButtonImage("close", new Rectangle(bounds.Width - BANNER_HEIGHT, 0, BANNER_HEIGHT, BANNER_HEIGHT), RvContentFiles.UI + "XButton");
        xButton.Subscribe(this);
        banner.addComponent(xButton);

        Rectangle contentRect = new Rectangle(bounds.X, bounds.Y + BANNER_HEIGHT, bounds.Width, bounds.Height - BANNER_HEIGHT);
        contentPanel = new RvPanel(contentRect);

        base.addComponent(contentPanel);
        base.addComponent(banner);
    }

    public override void addComponent(RvAbstractComponent component)
    {
        contentPanel.addComponent(component);
    }

    public Rectangle getContentBounds()
    {
        return contentPanel.getBounds();
    }

    public void OnNext(String actionStr)
    {
        buttonPressed(actionStr);
    }
    public void buttonPressed(String btnString)
    {
        if (btnString.Equals("close"))
        {
            setOkToFinish(true);
        }
    }

    public void OnCompleted()
    {
        //do nothing
    }
    public void OnError(Exception exception)
    {
        //do nothing
    }
}