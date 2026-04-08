using ItemChanger.Containers;
using ItemChanger.Silksong.Components;
using MonoDetour.DetourTypes;
using System;
using System.Collections.Generic;
using System.Text;
using Unity.Burst.CompilerServices;
using UnityEngine;

namespace ItemChanger.Silksong.Containers;

public class TabletContainer : Container
{
    public override string Name => ContainerNames.Tablet;

    public override uint SupportedCapabilities => ContainerCapabilities.None;

    public override bool SupportsInstantiate => false;

    public override bool SupportsModifyInPlace => true;

    private class ContainerInfoComponent : MonoBehaviour
    {
        public ContainerInfo? info;
    }

    public override void ModifyContainerInPlace(GameObject obj, ContainerInfo info)
    {
        obj.AddComponent<ContainerInfoComponent>().info = info;

        if (info.GiveInfo.Items.All(x => x.IsObtained()))
        {
            obj.GetComponent<BasicNPCBase>().Deactivate(false);
        }

        obj.AddComponent<ItemParticles>().items = info.GiveInfo.Items;
    }

    protected override void DoLoad()
    {
        Using(Md.BasicNPCBase.OnStartDialogue.ControlFlowPrefix(OverrideStartLoreDialogue));
    }

    protected override void DoUnload() { }

    private ReturnFlow OverrideStartLoreDialogue(BasicNPCBase self)
    {
        // ContainerInfo? info = ContainerInfo.FindContainerInfo(self.gameObject);
        ContainerInfo? info = null;
        ContainerInfoComponent? infoCpt = self.gameObject.GetComponent<ContainerInfoComponent>();
        if (infoCpt != null)
        {
            info = infoCpt.info;
        }

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
        
        item.Get();
        return ReturnFlow.SkipOriginal;
    }
}
