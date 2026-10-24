using Newtonsoft.Json;
using System.Diagnostics.CodeAnalysis;
using System.Reflection;

namespace ItemChanger.Silksong.Util;

// Name JsonUtils to avoid a name clash with IC.Serialization.JsonUtil
public static class JsonUtils
{
    internal static bool TryDeserializeEmbeddedResource<T>(string resourceName, [NotNullWhen(true)] out T? result)
    {
        Assembly asm = Assembly.GetExecutingAssembly();
        return TryDeserializeEmbeddedResource<T>(asm, resourceName, out result);
    }

    /// <summary>
    /// Deserialize the json from embedded resources with the default settings.
    /// </summary>
    /// <typeparam name="T">The type of the object to deserialize.</typeparam>
    /// <param name="asm">The assembly containing the resource.</param>
    /// <param name="resourceName">The name of the resource.</param>
    /// <param name="result">The deserialized object.</param>
    /// <returns>True if a non-null object was successfully deserialized.</returns>
    public static bool TryDeserializeEmbeddedResource<T>(Assembly asm, string resourceName, [NotNullWhen(true)] out T? result)
    {
        // TODO: This should use `asm` according to the documentation?
        using Stream? stream = asm.GetManifestResourceStream(resourceName);
        if (stream == null)
        {
            // Log warning at debug level, and let the caller handle the error
            ItemChangerPlugin.Instance.Logger.LogDebug($"Failed to find resource {resourceName} in {asm.FullName}");
            result = default;
            return false;
        }

        try
        {
            using StreamReader sr = new(stream);
            using JsonTextReader jtr = new(sr);

            JsonSerializer serializer = JsonSerializer.CreateDefault();
            result = serializer.Deserialize<T>(jtr);

            return result != null;
        }
        catch (JsonException ex)
        {
            ItemChangerPlugin.Instance.Logger.LogDebug($"Failed to deserialize resource at {resourceName}\n" + ex);
            result = default;
            return false;
        }
    }

}
