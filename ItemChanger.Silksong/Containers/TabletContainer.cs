using ItemChanger.Containers;
using ItemChanger.Extensions;
using ItemChanger.Silksong.Assets;
using ItemChanger.Silksong.Components;
using ItemChanger.Silksong.Extensions;
using ItemChanger.Silksong.Modules.YNBox;
using ItemChanger.Silksong.Util;
using MonoDetour.DetourTypes;
using TeamCherry.Localization;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace ItemChanger.Silksong.Containers;

public class TabletContainer : Container
{
    public override string Name => ContainerNames.Tablet;

    public override uint SupportedCapabilities => ContainerCapabilities.PayCosts;

    public override bool SupportsInstantiate => false;

    public override bool SupportsModifyInPlace => true;

    public override void ModifyContainerInPlace(GameObject obj, ContainerInfo info)
    {
        info.ApplyTo(obj);

        BasicNPC npc = obj.GetComponent<BasicNPC>();
        npc.GiveOnFirstTalk.Clear(); // This is not strictly necessary

        if (info.GiveInfo.Items.All(x => x.IsObtained()))
        {
            npc.Deactivate(false);
        }

        obj.AddComponent<ItemParticles>().items = info.GiveInfo.Items;
    }

    protected override void DoLoad()
    {
        Using(Md.BasicNPCBase.OnStartDialogue.ControlFlowPrefix(OverrideStartLoreDialogue));
    }

    protected override void DoUnload() { }

    // I'm doing this rather than setting the items in the tablet because
    // we need to avoid showing text
    private ReturnFlow OverrideStartLoreDialogue(BasicNPCBase self)
    {
        // TODO - use ContainerInfo in IC.Core
        // ContainerInfo? info = ContainerInfo.FindContainerInfo(self.gameObject);

        ContainerInfo? info = ContainerInfo.FindContainerInfo(self.gameObject);
        
        if (info == null)
        {
            return ReturnFlow.None;
        }

        self.DisableInteraction();

        SavedContainerItem item = ScriptableObject.CreateInstance<SavedContainerItem>();
        item.ContainerInfo = info;
        item.ContainerTransform = self.transform;

        item.SupportedMessageTypes = Enums.MessageType.Any;
        item.Callback = () =>
        {
            self.EndDialogue();
            self.Deactivate(false);
        };
        
        if (info.CostInfo is null)
        {
            item.Get();
        }
        else
        {
            CustomYNEnableModule.Open(() => item.Get(), self.EndDialogue, info.CostInfo.Cost, info.CostInfo.GetUIName());
        }
        
        return ReturnFlow.SkipOriginal;
    }

    /// <summary>
    /// Instantiates a Weaver lore tablet (like the one found in Weaver_08).
    /// Its text is determined by invoking the message provider each time the tablet is read.
    /// </summary>
    /// <returns>
    /// The spawned lore tablet.
    /// </returns>
    public static GameObject InstantiateWeaverTablet(Scene scene, Func<string> messageProvider)
    {
        GameObject tablet = GameObjectKeys.LORE_TABLET_WEAVER.InstantiateAsset(scene);

        string modKey = "IC_WEAVER_TABLET";
        LocalisedString s = new(Localization.Sheet, modKey);
        BasicNPC npc = tablet.FindChild("Inspect Region (1)")!.GetComponent<BasicNPC>();
        npc.StartingDialogue += () =>
        {
            Language._currentEntrySheets[Localization.Sheet][modKey] = messageProvider();
        };
        npc.talkText = [s];
        npc.repeatText = s;
        npc.returnText = s;
        return tablet;
    }
}
