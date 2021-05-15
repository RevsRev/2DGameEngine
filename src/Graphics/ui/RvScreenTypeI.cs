using System.Threading.Tasks;

public interface RvScreenTypeI<T> : RvScreenI
{
    public abstract Task<T> doPopup();
}