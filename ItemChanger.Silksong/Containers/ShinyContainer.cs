using GlobalSettings;
using ItemChanger.Containers;
using ItemChanger.Extensions;
using ItemChanger.Silksong.Tags;
using UnityEngine;

namespace ItemChanger.Silksong.Containers
{
    /// <summary>
    /// The default Silksong container, modeling a collectable shiny item.
    /// </summary>
    public class ShinyContainer : Container
    {
        public enum ShinyType
        {
            Normal,
            Instant,
        }

        [Flags]
        public enum ShinyControlFlags
        {
            AddFeatherEffect = 1 << 0,
            AllowHazardFloat = 1 << 1,

            Default = AllowHazardFloat,
        }

        public static ShinyContainer Instance { get; } = new();

        public override string Name => ContainerNames.Shiny;

        public override uint SupportedCapabilities => ContainerCapabilities.PayCosts; // TODO

        public override bool SupportsInstantiate => true;

        public override bool SupportsModifyInPlace => true;

        public override GameObject GetNewContainer(ContainerInfo info)
        {
            ShinyControlTag? tag = info.GiveInfo.Placement.GetPlacementAndLocationTags().OfType<ShinyControlTag>().FirstOrDefault();
            bool isInstant = tag?.ShinyType == ShinyType.Instant;
            CollectableItemPickup prefabComponent = isInstant ? Gameplay.CollectableItemPickupInstantPrefab : Gameplay.CollectableItemPickupPrefab;

            GameObject shiny = info.ContainingScene.Instantiate(prefabComponent.gameObject);
            ModifyContainerInPlace(shiny, info);
            return shiny;
        }

        public override void ModifyContainerInPlace(GameObject obj, ContainerInfo info)
        {
            CollectableItemPickup shiny = obj.GetComponent<CollectableItemPickup>();
            SavedContainerItem item = ScriptableObject.CreateInstance<SavedContainerItem>();
            item.ContainerInfo = info;
            item.ContainerTransform = shiny.transform;
            shiny.SetItem(item);

            ShinyControlTag? tag = info.GiveInfo.Placement.GetPlacementAndLocationTags().OfType<ShinyControlTag>().FirstOrDefault();
            ShinyControlFlags controlFlags = tag?.ShinyControlFlags ?? ShinyControlFlags.Default;

            if (controlFlags.HasFlag(ShinyControlFlags.AddFeatherEffect))
            {
                AddFeatherEffect(obj);
            }
            if (controlFlags.HasFlag(ShinyControlFlags.AllowHazardFloat))
            {
                AllowHazardFloat(obj);
            }
        }

        /// <summary>
        /// Add the default feather effect as applied to the Crawbug feather shinies.
        /// </summary>
        /// <returns>A <see cref="FeatherPhysics" /> component.</returns>
        public static FeatherPhysics AddFeatherEffect(GameObject go)
        {
            FeatherPhysics feather = go.AddComponent<FeatherPhysics>();
            feather.body = go.GetComponent<Rigidbody2D>();
            feather.transitionTime = 0.3f;
            feather.curveTime = 5f;
            feather.velocityMagnitudeX = 4.0f;
            feather.velocityMagnitudeY = -1.5f;
            feather.doNotAnimateY = false;
            feather.groundRayOrigin = Vector2.zero;
            feather.groundRayLength = 0.2f;

            feather.velocityCurveX = new(
                new Keyframe(0f, 1f, 0f, 0f, 0.3333333f, 0.3333333f),
                new Keyframe(0.2454834f, -0.005889893f, -0.377949f, -0.377949f, 0.3333333f, 0.3333333f),
                new Keyframe(0.5f, -1.00885f, 0f, 0f, 0.3333333f, 0.3333333f),
                new Keyframe(0.75f, 0.01470588f, -0.3526232f, -0.3526232f, 0.3333333f, 0.3333333f),
                new Keyframe(1f, 1f, 0f, 0f, 0.3333333f, 0.3333333f)
            )
            {
                preWrapMode = WrapMode.Loop,
                postWrapMode = WrapMode.Loop,
            };
            feather.velocityCurveY = new(
                new Keyframe(0f, 1f, 0f, 0f, 0.3333333f, 0.3333333f),
                new Keyframe(0.2501446f, 1.311527f, 0f, 0f, 0.3333333f, 0.3333333f),
                new Keyframe(0.7323256f, 0.6387825f, -0.002980232f, -0.002980232f, 0.3333333f, 0.3333333f),
                new Keyframe(1f, 1f, 0f, 0f, 0.3333333f, 0.3333333f)
            )
            {
                preWrapMode = WrapMode.ClampForever,
                postWrapMode = WrapMode.ClampForever,
            };

            return feather;
        }

        public static void AllowHazardFloat(GameObject go)
        {
            UObject.Destroy(go.GetComponent<BreakOnHazard>());
        }

        protected override void Load()
        {
        }

        protected override void Unload()
        {
        }
    }

}
