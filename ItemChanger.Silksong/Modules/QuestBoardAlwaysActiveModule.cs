using HarmonyLib;
using ItemChanger.Modules;
using MonoMod.RuntimeDetour;

namespace ItemChanger.Silksong.Modules;

/// <summary>
/// Module that forces all <see cref="QuestBoardInteractable"/> instances to be
/// active regardless of the game's normal activation conditions. Without this,
/// certain boards only appear once a story flag is set (e.g. the Bone Bottom
/// board requires <c>defeatedBellBeast</c>, the Belltown board requires
/// <c>spinnerDefeated</c>).
/// </summary>
/// <remarks>
/// The hook runs after <c>QuestBoardInteractable.Start()</c>. If the board was
/// deactivated by its <c>activeCondition</c>, we call <c>Activate()</c> and
/// restore the <c>activeObjects</c>/<c>inactiveObjects</c> visibility to the
/// "board is live" state.
/// </remarks>
[SingletonModule]
public sealed class QuestBoardAlwaysActiveModule : ItemChanger.Modules.Module
{
    protected override void DoLoad()
    {
        Using(new Hook(
            AccessTools.Method(typeof(QuestBoardInteractable), "Start"),
            (Action<QuestBoardInteractable> orig, QuestBoardInteractable self) =>
            {
                orig(self);

                if (!self.IsDisabled)
                    return;

                // The board's activeCondition was not fulfilled — re-enable it.
                self.Activate();

                // Flip the visual game objects to the "active board" state.
                foreach (GameObject obj in self.activeObjects)
                    obj?.SetActive(true);

                foreach (GameObject obj in self.inactiveObjects)
                    obj?.SetActive(false);
            }
        ));
    }

    protected override void DoUnload() { }
}
