using ItemChanger.Containers;

namespace ItemChanger.Silksong.Containers
{
    public class ChestContainer : Container
    {
        public static ChestContainer Instance { get; } = new();

        public override string Name => ContainerNames.Chest;

        public override uint SupportedCapabilities => ContainerCapabilities.None;

        public override bool SupportsInstantiate => true;

        public override bool SupportsModifyInPlace => true;

        public override GameObject GetNewContainer(ContainerInfo info)
        {
            throw new NotImplementedException();
        }

        public override void ModifyContainerInPlace(GameObject obj, ContainerInfo info)
        {
            throw new NotImplementedException();
        }

        protected override void Load()
        {
        }

        protected override void Unload()
        {
        }
    }

}
