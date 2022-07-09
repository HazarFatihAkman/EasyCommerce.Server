using System;

namespace EasyCommerce;

public static class ObjectExtensions
{
    public static bool IsNull(this object obj) => obj is null;

    public static bool IsNotNull(this object obj) => !obj.IsNull();

    public static bool GuidEmpty(this Guid guid)
    {
        if (guid == Guid.Empty)
        {
            return true;
        }
        return false;
    }

    public static void ThrowIfNull(this object obj, string propertyName)
    {
        if (obj.IsNull())
        {
            throw new ArgumentNullException(propertyName, $"Property `{propertyName}` cannot be null.");
        }
    }
   
}
