using Module = ItemChanger.Modules.Module;

namespace ItemChanger.Silksong.Modules
{
    public class ProgressiveHunterCrestModule : Module
    {
        public static ProgressiveHunterCrestModule? Instance { get; private set; }

        public int Tier { get; set; }

        protected override void DoLoad()
        {
            Instance = this;
        }

        protected override void DoUnload()
        {
            Instance = null;
        }
    }
}
