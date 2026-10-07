using System.Reflection;

namespace ItemChanger.Silksong.Util;

internal static class ReflectionUtil
{
    internal static FieldOrPropertyInfo GetFieldOrProperty(this Type type, string name, BindingFlags flags)
    {
        if (type.GetField(name, flags) is { } fieldInfo)
            return new(fieldInfo);
        if (type.GetProperty(name, flags) is { } propertyInfo)
            return new(propertyInfo);
        throw new ArgumentException($"{type.FullName} does not have a field or property '{name}'.");
    }

    internal class FieldOrPropertyInfo
    {
        private readonly FieldInfo? fieldInfo;

        private readonly PropertyInfo? propertyInfo;

        internal FieldOrPropertyInfo(FieldInfo fieldInfo) => this.fieldInfo = fieldInfo;

        internal FieldOrPropertyInfo(PropertyInfo propertyInfo) => this.propertyInfo = propertyInfo;

        internal object GetValue(object obj) => fieldInfo != null ? fieldInfo.GetValue(obj) : propertyInfo!.GetValue(obj);

        internal void SetValue(object obj, object value)
        {
            if (fieldInfo != null)
                fieldInfo.SetValue(obj, value);
            else
                propertyInfo!.SetValue(obj, value);
        }
    }
}
