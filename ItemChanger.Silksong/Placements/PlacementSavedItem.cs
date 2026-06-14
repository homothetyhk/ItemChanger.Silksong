using ItemChanger.Items;
using ItemChanger.Placements;
using TeamCherry.Localization;
using UnityEngine;

namespace ItemChanger.Silksong.Placements;

/// <summary>
/// An implementation of <see cref="SavedItem"/> that hosts an IC placement.
/// </summary>
public class PlacementSavedItem : SavedItem
{
    public required Placement Placement { get; set; }
    public required GiveInfo GiveInfo { get; set; }
    public Action? Callback { get; set; }
    public override bool IsUnique => true;
    public override bool CanGetMore() => !Placement.AllObtained();

    public override Sprite GetPopupIcon()
    {
        return Placement.Items
            .FirstOrDefault(item => !item.IsObtained())?
            .GetPreviewSprite(Placement)!;
    }

    public override string GetPopupName()
    {
        IEnumerable<string> names = Placement.Items
            .Where(item => !item.IsObtained())
            .Select(item => item.GetResolvedUIDef(Placement)?.GetPreviewName())
            .Where(st => st != null).Cast<string>();
        return string.Join(", ", names);
    }

    public override void Get(bool showPopup = true)
    {
        Placement.GiveAll(GiveInfo, Callback);
    }
}