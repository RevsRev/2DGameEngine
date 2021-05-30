using System.Collections.Generic;

public class RvPopupHandler
{
    private static RvPopupHandler instance;
    private static readonly object padlock = new object();

    private List<RvPopupMenu> menus = new List<RvPopupMenu>();

    private RvPopupHandler()
    {

    }

    public static RvPopupHandler the()
    {
        if (instance == null)
        {
            lock(padlock)
            {
                if (instance == null)
                {
                    instance = new RvPopupHandler();
                }
            }
        }
        return instance;
    }

    public void addPopupMenu(RvPopupMenu popupMenu)
    {
        if (!menus.Contains(popupMenu))
        {
            menus.Add(popupMenu);
        }
    }
    public void removePopupMenu(RvPopupMenu popupMenu)
    {
        menus.Remove(popupMenu);
    }
    public bool alreadyPoppedUp(RvPopupMenuListenerI popupMenuListenerI)
    {
        for (int i=0; i<menus.Count; i++)
        {
            if (menus[i].getActionLisener() == popupMenuListenerI)
            {
                return true;
            }
        }
        return false;
    }
}