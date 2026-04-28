using ItemChanger.Locations;
using ItemChanger.Placements;
using ItemChanger.Serialization;
using ItemChanger.Silksong.Components;
using ItemChanger.Tags;
using ItemChanger.Tags.Constraints;

namespace ItemChanger.Silksong.Tags;

public interface IHintBoxTag
{
    bool DisplayTest();
    string? GetText();
    void OnDisplay(string? text);
}

[LocationTag, PlacementTag]
public class FixedTextHintBoxTag : Tag, IHintBoxTag
{
    public required IValueProvider<string> Text { get; init; }

    public bool DisplayTest() => true;

    public string? GetText() => Text.Value;

    public void OnDisplay(string? text) { }
}

/// <summary>
/// Tag to display the items at a placement.
/// </summary>
[LocationTag, PlacementTag]
public class PlacementItemsHintBoxTag : Tag, IHintBoxTag
{
    private Placement? _placement;

    protected override void DoLoad(TaggableObject parent)
    {
        if (parent is Placement p)
        {
            _placement = p;
        }
        else if (parent is Location loc)
        {
            _placement = loc.Placement;
        }
    }

    protected override void DoUnload(TaggableObject parent)
    {
        _placement = null;
    }

    public bool DisplayTest() => _placement?.AllObtained() == false;

    public string? GetText() => _placement?.GetUIName();

    public void OnDisplay(string? text)
    {
        _placement?.OnPreview(text ?? "??NONE??");
    }
}

public static class HintBoxTagExtensions
{
    public static void Apply(this HintBox box, IHintBoxTag tag)
    {
        box.DisplayTest = tag.DisplayTest;
        box.GetDisplayText = tag.GetText;
        box.OnDisplay += tag.OnDisplay;
    }
}
