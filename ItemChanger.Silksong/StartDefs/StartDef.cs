using Benchwarp.Benches;
using Newtonsoft.Json;

namespace ItemChanger.Silksong.StartDefs
{
    /// <summary>
    /// Base class for an object to provide a spawn location at the start of the game.
    /// </summary>
    public abstract class StartDef : TaggableObject, IRespawnInfo
    {
        public abstract RespawnInfo GetRespawnInfo();

        [JsonIgnore]
        public bool Loaded { get; private set; }

        /// <summary>
        /// Method allowing derived start defs to initialize and place hooks. Called once during loading.
        /// </summary>
        protected virtual void DoLoad() { }

        /// <summary>
        /// Loads the start def. If the start def is already loaded, does nothing.
        /// </summary>
        public void LoadOnce()
        {
            if (!Loaded)
            {
                try
                {
                    LoadTags();
                    DoLoad();
                }
                catch (Exception e)
                {
                    Logger.LogError($"Error loading start def {ToString()}:\n{e}");
                }
                Loaded = true;
            }
        }

        /// <summary>
        /// Method allowing derived start defs to dispose hooks. Called once during unloading.
        /// </summary>
        protected virtual void DoUnload() { }

        /// <summary>
        /// Unloads the start def. If the start def is not loaded, does nothing.
        /// </summary>
        public void UnloadOnce()
        {
            if (Loaded)
            {
                try
                {
                    UnloadTags();
                    DoUnload();
                }
                catch (Exception e)
                {
                    Logger.LogError($"Error unloading start def {ToString()}:\n{e}");
                }
                Loaded = false;
            }
        }

        public override string ToString()
        {
            try
            {
                return $"{base.ToString()} {{ {GetRespawnInfo()} }}";
            }
            catch
            {
                return base.ToString();
            }
        }
    }
}
