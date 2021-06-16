using Microsoft.Xna.Framework;
using System.Collections.Generic;
using System;

public class RvDialogBanner : RvPanel, IObserver<string>, IObservable<string>
{
    private const int BANNER_HEIGHT = 15;
    IObserver<string> dialog = null;

    public RvDialogBanner(RvSDialog dialog) : base(getBannerBounds(dialog))
    {
        this.dialog = dialog;
        initComponents();
    }

    // public override void init()
    // {
    //     initComponents();
    // }

    private void initComponents()
    {
        setPanelColor(Color.DarkGray);
        RvButtonImage xButton = new RvButtonImage("close", new Rectangle(0, 0, bounds.Height, bounds.Height), RvContentFiles.UI + "XButton");
        xButton.setOffset(new Vector2(bounds.Width - bounds.Height, 0));
        xButton.Subscribe(this);
        addComponent(xButton);
    }

    private static Rectangle getBannerBounds(RvSDialog dialog)
    {
        //todo
        Rectangle dialogBounds =  dialog.getBounds();
        return new Rectangle(0,0, dialogBounds.Width, BANNER_HEIGHT);
    }

    public IDisposable Subscribe(IObserver<string> dialog)
    {
        this.dialog = dialog;
        return new Unsubscriber<string>(new List<IObserver<string>>{dialog}, this);
    }

    public void OnNext(String actionStr)
    {
        dialog.OnNext(actionStr);
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