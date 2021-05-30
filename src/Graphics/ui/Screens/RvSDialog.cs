using Microsoft.Xna.Framework;
using System;
using System.Threading.Tasks;

public abstract class RvSDialog<T> : RvSAbstractScreen, IObserver<string>, RvMouseListenerI
{
    private const int BANNER_HEIGHT = 20;

    RvAbstractPanel banner; //contains the title, X buttons, etc...
    RvAbstractPanel contentPanel; //for putting the actual content of the panel.

    protected RvSDialog(Rectangle bounds) : base(bounds)
    {
        init();
    }

    public override void dispose()
    {
        RvMouse.the().removeMouseListener(this);
        base.dispose();
    }

    public override void init()
    {
        base.init();
        
        RvMouse.the().addMouseListener(this);

        //I'm going to assume we put in sensible dimensions for the time being...
        Rectangle bannerRect = new Rectangle(bounds.X, bounds.Y, bounds.Width, BANNER_HEIGHT);
        banner = new RvPanel(bannerRect);
        banner.setPanelColor(Color.DarkGray);
        RvButtonImage xButton = new RvButtonImage("close", new Rectangle(0, 0, BANNER_HEIGHT, BANNER_HEIGHT), RvContentFiles.UI + "XButton");
        xButton.setOffset(new Vector2(bounds.Width - BANNER_HEIGHT, 0));
        xButton.Subscribe(this);
        banner.addComponent(xButton);

        Rectangle contentRect = new Rectangle(0,0, bounds.Width, bounds.Height - BANNER_HEIGHT);
        contentPanel = new RvPanel(contentRect);
        contentPanel.setOffset(new Vector2(0, BANNER_HEIGHT));

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

    //stuff for mouse listener
    public Rectangle getClickableRegion()
    {
        return getBounds();
    }
    public void doDrag(Vector2 mouseCoords, Vector2 anchorPoint)
    {
        Vector2 pos = mouseCoords - anchorPoint;
        move(pos);
    }
}