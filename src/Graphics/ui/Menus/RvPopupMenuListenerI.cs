

public interface RvPopupMenuListenerI : RvMouseListenerI
{
    public void performPopupMenuAction(string actionCommand);
    public RvPopupMenu buildPopupMenu();

    public static void onClick(RvMouseEvent e, RvPopupMenuListenerI pListener)
    {
        if (e.rightButton)
        {
            RvPopupMenu pMenu = pListener.buildPopupMenu();
            pMenu.setActionListener(pListener);
            pMenu.buildMenu(e.X, e.Y);
        }
    }
}