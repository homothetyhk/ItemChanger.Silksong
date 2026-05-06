using GlobalSettings;
using ItemChanger.Containers;
using ItemChanger.Extensions;
using ItemChanger.Silksong.Components;
using ItemChanger.Silksong.Extensions;
using ItemChanger.Silksong.Modules.YNBox;
using ItemChanger.Silksong.Tags;
using Silksong.UnityHelper.Extensions;
using System.Diagnostics.CodeAnalysis;
using UnityEngine;

namespace ItemChanger.Silksong.Containers;

/// <summary>
/// The default Silksong container, modeling a collectable shiny item.
/// </summary>
public class ShinyContainer : Container
{
    public enum ShinyType
    {
        /// <summary>
        /// Regular shinies which require an interact animation to pick up.
        /// </summary>
        Normal,
        /// <summary>
        /// Shinies which are obtained on contact.
        /// </summary>
        Instant,
    }

    [Flags]
    public enum ShinyControlFlags
    {
        /// <summary>
        /// The feather effect as applied to Crawbug feather shinies.
        /// </summary>
        AddFeatherEffect = 1 << 0,
        /// <summary>
        /// Removes BreakOnHazard component, allowing shiny to remain active after touching a hazard.
        /// </summary>
        AllowHazardFloat = 1 << 1,

        Default = 0,
    }

    public enum ShinyFling
    {
        /// <summary>
        /// The shiny's fling is not modified. Existing shinies will keep their existing fling.
        /// New shinies have no fling, but fall under the gravity of their rigidbodies (essentially equivalent to Drop).
        /// </summary>
        KeepExisting,
        /// <summary>
        /// Flings shiny with speed 0, leaving it to fall under its rigidbody's gravity.
        /// </summary>
        Drop,
        /// <summary>
        /// Flings shiny left or right with equal probability.
        /// </summary>
        Random,
        /// <summary>
        /// Flings shiny at speed 22f and angle 100f.
        /// </summary>
        Left,
        /// <summary>
        /// Flings shiny at speed 22f and angle 80f.
        /// </summary>
        Right,
        /// <summary>
        /// Flings shiny left or right depending on hero position.
        /// </summary>
        AwayFromHero,
        /// <summary>
        /// Does not fling shiny, sets rigidbody as kinematic so that shiny does not fall under gravity.
        /// </summary>
        FloatInPlace,
    }

    public record ShinyControlInfo
    {
        public static ShinyControlInfo Default { get; } = new();
        public ShinyType ShinyType { get; init; } = ShinyType.Normal;
        public ShinyControlFlags ShinyControlFlags { get; init; } = ShinyControlFlags.Default;
        public ShinyFling ShinyFling { get; init; } = ShinyFling.KeepExisting;
    }

    /// <summary>
    /// A ContainerInfo which contains additional shiny-specific configuration info. Takes precedence over configuration provided through <see cref="ShinyControlTag"/>.
    /// </summary>
    public class ShinyContainerInfo : ContainerInfo
    {
        public required ShinyControlInfo ShinyInfo { get; init; }

        public ShinyContainerInfo() { }

        [SetsRequiredMembers]
        public ShinyContainerInfo(ContainerInfo containerInfo, ShinyControlInfo shinyInfo)
        {
            base.CostInfo = containerInfo.CostInfo;
            base.ContainingScene = containerInfo.ContainingScene;
            base.ContainerType = containerInfo.ContainerType;
            base.GiveInfo = containerInfo.GiveInfo;
            base.RequestedCapabilities = containerInfo.RequestedCapabilities;
            this.ShinyInfo = shinyInfo;
        }
    }

    public static ShinyContainer Instance { get; } = new();

    public override string Name => ContainerNames.Shiny;

    public override uint SupportedCapabilities => ContainerCapabilities.PayCosts; // TODO

    public override bool SupportsInstantiate => true;

    public override bool SupportsModifyInPlace => true;

    public override GameObject GetNewContainer(ContainerInfo info)
    {
        ShinyControlInfo shinyInfo = GetShinyControlInfo(info);
 
        bool isInstant = shinyInfo?.ShinyType == ShinyType.Instant;
        CollectableItemPickup prefabComponent = isInstant ? Gameplay.CollectableItemPickupInstantPrefab : Gameplay.CollectableItemPickupPrefab;

        GameObject shiny = info.ContainingScene.Instantiate(prefabComponent.gameObject);
        shiny.name = info.GetGameObjectName("IC Shiny Item");
        ModifyContainerInPlace(shiny, info);
        return shiny;
    }

    public override void ModifyContainerInPlace(GameObject obj, ContainerInfo info)
    {
        info.ApplyTo(obj);

        CollectableItemPickup shiny = obj.GetComponent<CollectableItemPickup>();
        SavedContainerItem item = ScriptableObject.CreateInstance<SavedContainerItem>();
        item.ContainerInfo = info;
        item.ContainerTransform = shiny.transform;

        if (obj.GetComponent<CollectableItemPickup>().pickupTrigger != null)
        {
            // Only support small message type for instant shiny
            item.SupportedMessageTypes = Enums.MessageType.SmallPopup;
        }
        else
        {
            item.SupportedMessageTypes = Enums.MessageType.Any;
            item.RetainControl = true;
        }
        shiny.SetItem(item);

        ShinyControlInfo shinyInfo = GetShinyControlInfo(info);
        ShinyControlFlags controlFlags = shinyInfo.ShinyControlFlags;

        if (controlFlags.HasFlag(ShinyControlFlags.AddFeatherEffect))
        {
            AddFeatherEffect(obj);
        }
        if (controlFlags.HasFlag(ShinyControlFlags.AllowHazardFloat))
        {
            AllowHazardFloat(obj);
        }

        ShinyFling fling = shinyInfo.ShinyFling;
        Rigidbody2D rb = shiny.gameObject.GetOrAddComponent<Rigidbody2D>();

        if (fling == ShinyFling.KeepExisting)
        {
            // no-op
        }
        else if (fling == ShinyFling.FloatInPlace)
        {
            shiny.fling = false;
            rb.bodyType = RigidbodyType2D.Kinematic;
            shiny.pickupAnim = CollectableItemPickup.PickupAnimations.Stand;
        }
        else
        {
            shiny.fling = true;
            rb.bodyType = RigidbodyType2D.Dynamic;
            shiny.pickupAnim = CollectableItemPickup.PickupAnimations.Normal;
            shiny.flingDirection = fling switch
            {
                ShinyFling.Random => CollectableItemPickup.FlingDirection.Either,
                ShinyFling.Left => CollectableItemPickup.FlingDirection.Left,
                ShinyFling.Right => CollectableItemPickup.FlingDirection.Right,
                ShinyFling.AwayFromHero => CollectableItemPickup.FlingDirection.AwayFromHero,
                ShinyFling.Drop or _ => CollectableItemPickup.FlingDirection.Drop,
            };
        }

        if (info.GiveInfo.Placement.GetPlacementAndLocationTags().OfType<IHintBoxTag>().FirstOrDefault() is IHintBoxTag tag)
        {
            HintBox box = obj.AddComponent<HintBox>();
            box.Apply(tag);
        }

        if (info.CostInfo is not null)
        {
            CustomYNBoxInfo boxInfo = obj.AddComponent<CustomYNBoxInfo>();
            boxInfo.Cost = info.CostInfo.Cost;
            boxInfo.TextGetter = () => info.CostInfo.GetUIName();
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

    private static ShinyControlInfo GetShinyControlInfo(ContainerInfo info)
    {
        return (info as ShinyContainerInfo)?.ShinyInfo
            ?? info.GiveInfo.Placement.GetPlacementAndLocationTags().OfType<ShinyControlTag>().FirstOrDefault()?.Info
            ?? ShinyControlInfo.Default;
    }

    protected override void DoLoad()
    {
    }

    protected override void DoUnload()
    {
    }
}
