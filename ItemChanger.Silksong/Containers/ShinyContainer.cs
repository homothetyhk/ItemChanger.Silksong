using GlobalSettings;
using ItemChanger.Containers;
using ItemChanger.Extensions;

namespace ItemChanger.Silksong.Containers
{
    /// <summary>
    /// The default Silksong container, modeling a collectable shiny item.
    /// </summary>
    public class ShinyContainer : Container
    {
        public static ShinyContainer Instance { get; } = new();

        public override string Name => ContainerNames.Shiny;

        public override uint SupportedCapabilities => ContainerCapabilities.PayCosts; // TODO

        public override bool SupportsInstantiate => true;

        public override bool SupportsModifyInPlace => true;

        public override GameObject GetNewContainer(ContainerInfo info)
        {
            GameObject shiny = info.ContainingScene.Instantiate(Gameplay.CollectableItemPickupPrefab.gameObject);
            ModifyContainerInPlace(shiny, info);
            return shiny;
        }

        public override void ModifyContainerInPlace(GameObject obj, ContainerInfo info)
        {
            CollectableItemPickup shiny = obj.GetComponent<CollectableItemPickup>();
            SavedContainerItem item = UnityEngine.ScriptableObject.CreateInstance<SavedContainerItem>();
            item.ContainerInfo = info;
            item.ContainerTransform = shiny.transform;
            shiny.SetItem(item);
        }

        protected override void Load()
        {
        }

        protected override void Unload()
        {
        }
    }

}
