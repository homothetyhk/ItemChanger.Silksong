using ItemChanger.Containers;
using ItemChanger.Items;
using UnityEngine;

namespace ItemChanger.Silksong.Containers
{
    /// <summary>
    /// A <see cref="SavedItem"/> implemented to be compatible with ItemChanger containers.
    /// </summary>
    public class SavedContainerItem : SavedItem
    {
        public required ContainerInfo ContainerInfo { get; set; }
        public required Transform ContainerTransform { get; set; }
        public Action? Callback { get; set; } = null;

        // refer to CheckActivation() and activatedRead field of CollectableItemPickup.
        public override bool IsUnique => true;
        public override bool CanGetMore() => ContainerInfo.GiveInfo.Items.Any(i => !i.IsObtained());

        public override void Get(bool showPopup = true)
        {
            if (ReferenceEquals(ContainerInfo.GiveInfo.Items, ContainerInfo.GiveInfo.Placement.Items)) // TODO: wait for IC.Core fix
            {
                ContainerInfo.GiveInfo.Placement.GiveAll(new GiveInfo()
                {
                    Container = ContainerInfo.ContainerType,
                    FlingType = ContainerInfo.GiveInfo.FlingType,
                    MessageType = Enums.MessageType.Any,
                    Transform = ContainerTransform,
                }, Callback);
            }
            else
            {
                throw new NotImplementedException("Partial give operations are not currently supported.");
            }
        }
    }

}
