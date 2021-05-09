using System.Threading.Tasks;
using System;

public class RvScreenHandler
{
    private RvScreenHandler instance;

    private RvScreenHandler()
    {

    }

    public RvScreenHandler the()
    {
        if (instance == null)
        {
            instance = new RvScreenHandler();
        }
        return instance;
    }


    public static async Task<T> doPopup<T>(RvScreenI<T> screen)
    {
        Task<T> task = screen.doPopup();
        return await task;
    }
}