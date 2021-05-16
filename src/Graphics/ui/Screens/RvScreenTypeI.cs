using System.Threading.Tasks;

public interface RvScreenTypeI<T> : RvScreenI
{
    public async Task<T> doPoupAsync()
    {
        return doPopup();
    }

    public abstract T doPopup();
}