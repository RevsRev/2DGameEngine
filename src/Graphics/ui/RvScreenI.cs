using System.Threading.Tasks;

public interface RvScreenI<T>
{
    public abstract Task<T> doPopup();
}