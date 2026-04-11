using ItemChanger.Events;

namespace ItemChanger.Silksong
{
    internal static class GlobalRefs
    {
        public static SilksongHost Host => SilksongHost.Instance;
        public static GameEvents GameEvents => Host.GameEvents;
        public static LifecycleEvents LifecycleEvents => Host.LifecycleEvents;
        public static Finder Finder => Host.Finder;
        public static ItemChangerProfile? ActiveProfile => Host.ActiveProfile;

        [System.Diagnostics.Conditional("DEBUG")] public static void LogFine(object data) => Host.Logger.LogFine(data?.ToString());
        public static void LogDebug(object data) => ItemChangerPlugin.Instance.Logger.LogDebug(data);
        public static void LogInfo(object data) => ItemChangerPlugin.Instance.Logger.LogInfo(data);
        public static void LogMessage(object data) => ItemChangerPlugin.Instance.Logger.LogMessage(data);
        public static void LogWarn(object data) => ItemChangerPlugin.Instance.Logger.LogWarning(data);
        public static void LogError(object data) => ItemChangerPlugin.Instance.Logger.LogError(data);
        public static void LogFatal(object data) => ItemChangerPlugin.Instance.Logger.LogFatal(data);
    }
}
