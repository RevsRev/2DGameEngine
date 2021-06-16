using Microsoft.Xna.Framework;
using System;
using System.Threading.Tasks;

public abstract class RvSDialog : RvSAbstractScreen, IObserver<string>, RvMouseListenerI
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
        RvMouse.the().removeListener(this);
        base.dispose();
    }

    public override void init()
    {
        base.init();
        
        RvMouse.the().addListener(this);

        //I'm going to assume we put in sensible dimensions for the time being...
        // Rectangle bannerRect = new Rectangle(0, 0, bounds.Width, BANNER_HEIGHT);
        // banner = new RvPanel(bannerRect);
        // banner.setPanelColor(Color.DarkGray);
        // RvButtonImage xButton = new RvButtonImage("close", new Rectangle(0, 0, BANNER_HEIGHT, BANNER_HEIGHT), RvContentFiles.UI + "XButton");
        // xButton.setOffset(new Vector2(bounds.Width - BANNER_HEIGHT, 0));
        // xButton.Subscribe(this);
        banner = new RvDialogBanner(this);

        Rectangle contentRect = new Rectangle(0,0, bounds.Width, bounds.Height - BANNER_HEIGHT);
        contentPanel = new RvPanel(contentRect);
        contentPanel.setOffset(new Vector2(0, BANNER_HEIGHT));

        base.addComponent(contentPanel);
        base.addComponent(banner);
        //banner.addComponent(xButton); //an annoying quirk of how i've written this means the ui will only  look right if we add the button after the banner... fix this at some point.
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
        Rectangle bounds = getBounds();
        Vector2 offset = getOffset();
        return new Rectangle((int)(bounds.X + offset.X), (int)(bounds.Y + offset.Y), bounds.Width, bounds.Height);
    }
    public void doDrag(Vector2 mouseCoords, Vector2 anchorPoint)
    {
        Vector2 oldPos = getOffset() + anchorPoint; //where the mouse was when we clicked
        Vector2 delPos = mouseCoords - oldPos; //change in position
        move(delPos);
    }
}