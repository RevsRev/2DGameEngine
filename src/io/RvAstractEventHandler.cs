using System.Collections.Generic;

public abstract class RvAbstractEventHandler<T> where T : RvEventListenerI
{
    protected RvAbstractEventHandler()
    {
    }

    public abstract void addListener(T eventListener);
    public abstract void removeListener(T eventListener);
}