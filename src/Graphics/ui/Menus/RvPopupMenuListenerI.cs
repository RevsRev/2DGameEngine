

public interface RvPopupMenuListenerI : RvMouseListenerI
{
    public void performPopupMenuAction(string actionCommand);
    public RvPopupMenu buildPopupMenu();
    public virtual bool okToBuildMenu(int x, int y){return true;}

    private bool reallyOkToBuildMenu(RvMouseEvent e)
    {
        if (RvPopupHandler.the().alreadyPoppedUp(this))
        {
            return false;
        }
        return okToBuildMenu(e.X ,e.Y);
    }

    public static void onClick(RvMouseEvent e, RvPopupMenuListenerI pListener)
    {
        if (e.rightButton)
        {
            if (!pListener.reallyOkToBuildMenu(e))
            {
                return;
            }

            RvPopupMenu pMenu = pListener.buildPopupMenu();
            pMenu.setActionListener(pListener);
            pMenu.buildMenu(e.X, e.Y);
        }
    }
}