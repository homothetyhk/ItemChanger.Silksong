using ItemChanger.Events;
using ItemChanger.Logging;

namespace ItemChanger.Silksong
{
    internal static class GlobalRefs
    {
        public static SilksongHost Host { get => field ??= (SilksongHost)ItemChangerHost.Singleton; }
        public static GameEvents GameEvents => Host.GameEvents;
        public static LifecycleEvents LifecycleEvents => Host.LifecycleEvents;
        public static Finder Finder => Host.Finder;
        public static ItemChangerProfile? ActiveProfile => Host.ActiveProfile;
        public static ILogger Logger => Host.Logger;
    }
}
