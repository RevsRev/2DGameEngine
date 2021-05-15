using System;

public class RvClassLoader
{
    public static object createByName(string typeName)
    {
        System.Reflection.Assembly[] assemblies = AppDomain.CurrentDomain.GetAssemblies();

        foreach (var assembly in assemblies)
        {
            Type[] types = assembly.GetTypes();
            foreach (var type in types)
            {
                string tName = type.Name;
                if (tName.Equals(typeName))
                {
                    return Activator.CreateInstance(type);
                }
            }
        }
        throw new InvalidOperationException("Type " + typeName + " not found");
    }
}