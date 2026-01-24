using Benchwarp.Benches;
using ItemChanger.Modules;
using ItemChanger.Silksong.StartDefs;

namespace ItemChanger.Silksong.Modules
{
    public class StartDefModule : Module
    {
        public required StartDef StartDef { get; set; }

        protected override void DoLoad()
        {
            StartDef.LoadOnce();
            Benchwarp.Events.BenchListModifiers.OnGetStartDef.Add(OverrideStart);
        }

        protected override void DoUnload()
        {
            StartDef.UnloadOnce();
            Benchwarp.Events.BenchListModifiers.OnGetStartDef.Remove(OverrideStart);
        }

        private RespawnInfo OverrideStart(RespawnInfo orig) => StartDef.GetRespawnInfo();
    }
}
