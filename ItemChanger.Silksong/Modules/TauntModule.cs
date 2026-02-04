using HutongGames.PlayMaker;
using HutongGames.PlayMaker.Actions;
using UnityEngine;
using Module = ItemChanger.Modules.Module;

namespace ItemChanger.Silksong.Modules
{
    public class TauntModule : Module
    {
        public static TauntModule? Instance { get; private set; }

        public bool HasTaunt { get; set; }

        private static bool hooksInitialized = false;

        protected override void DoLoad()
        {
            Instance = this;
            if (!hooksInitialized)
            {
                hooksInitialized = true;
                On.HeroController.SilkTaunted += OverrideSilkTaunted;
                On.HeroController.RingTaunted += OverrideRingTaunted;
                On.HeroController.Start += OnHeroStart;
            }
        }

        protected override void DoUnload()
        {
            Instance = null;
            On.HeroController.SilkTaunted -= OverrideSilkTaunted;
            On.HeroController.RingTaunted -= OverrideRingTaunted;
            On.HeroController.Start -= OnHeroStart;
            hooksInitialized = false;
        }

        private static void OnHeroStart(On.HeroController.orig_Start orig, HeroController self)
        {
            orig(self);
            SetupSilkSpecialsFSM(self);
        }

        private static void SetupSilkSpecialsFSM(HeroController hero)
        {
            foreach (var fsm in hero.GetComponents<PlayMakerFSM>())
            {
                if (fsm.FsmName != "Silk Specials") continue;
                
                foreach (var state in fsm.Fsm.States)
                {
                    if (state.Name != "Taunt Check") continue;
                    
                    var actions = state.Actions;
                    var newActions = new FsmStateAction[actions.Length + 1];
                    newActions[0] = new TauntCheckAction();
                    for (int i = 0; i < actions.Length; i++)
                        newActions[i + 1] = actions[i];
                    state.Actions = newActions;
                    return;
                }
            }
        }

        private static void OverrideSilkTaunted(On.HeroController.orig_SilkTaunted orig, HeroController self)
        {
            if (Instance?.HasTaunt ?? true) orig(self);
        }

        private static void OverrideRingTaunted(On.HeroController.orig_RingTaunted orig, HeroController self)
        {
            if (Instance?.HasTaunt ?? true) orig(self);
        }
    }

    public class TauntCheckAction : FsmStateAction
    {
        public override void OnEnter()
        {
            if (TauntModule.Instance != null && !TauntModule.Instance.HasTaunt)
            {
                Fsm.Event("CANCEL TAUNT");
            }
            Finish();
        }
    }
}
