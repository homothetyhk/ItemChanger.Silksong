using ItemChanger.Enums;
using ItemChanger.Serialization;
using ItemChanger.Silksong.Assets;
using ItemChanger.Silksong.Extensions;
using ItemChanger.Silksong.Modules;
using ItemChanger.Silksong.Serialization;
using UnityEngine;

namespace ItemChanger.Silksong.UIDefs.BigUIDefs;

/// <summary>
/// UIDef for crests. This supports all base game crests, excluding cursed/cloakless (which don't exist)
/// and upgrades (which use a different prefab).
/// 
/// Certain base game crests are missing certain language-related fields on the ToolCrest item, so the
/// <see cref="CrestUIMsgRepairModule"/> is needed to be installed for those.
/// </summary>
public class CrestUIDef : ControlRelinquishedUIDef
{
    public override MessageType RequiredMessageType => MessageType.LargePopup;

    public required IValueProvider<ToolCrest> Crest { get; init; }

    public string PrefabKey { get; init; } = GameObjectKeys.CREST_GET_PROMPT;

    public float HastenFactor { get; init; } = 2.4f;

    public override void DoSendMessage(Action? callback)
    {
        // The prefab is instantiated by the UIMsgBase.Spawn function, so we don't need to instantiate it ourselves
        GameObject prefab = PrefabKey.GetGameObjectPrefab();

        GameObject spawned = UIMsgBase<ToolCrest>.Spawn(Crest.Value, prefab.GetComponent<ToolCrestUIMsg>(), callback).gameObject;

        spawned.AddComponent<UIProxyControlBlockerComponent>();

        spawned.GetComponent<Animator>().speed = HastenFactor;
        spawned.GetComponent<ToolCrestUIMsg>().startPauseTime = 1f / HastenFactor;

        // Remove the backboard?
        // spawned.FindChild("backboard")!.GetComponent<SpriteRenderer>().sprite = new EmptySprite().Value;
        spawned.AddComponent<RemoveBackboardWhenDamaged>().BackboardName = "backboard";
    }

    public static CrestUIDef Create(string id, UIDef fallback, string prefabKey = GameObjectKeys.CREST_GET_PROMPT)
    {
        BaseGameSavedItem item = new() { Id = id, Type = BaseGameSavedItem.ItemType.ToolCrest };
        IValueProvider<ToolCrest> crestProvider = item.Downcast<SavedItem, ToolCrest>();

        return new() { Crest = crestProvider, Fallback = fallback, PrefabKey = prefabKey };
    }
}
