using ItemChanger;
using ItemChanger.Silksong;
using ItemChanger.Silksong.Items;
using ItemChanger.Silksong.RawData;
using ItemChanger.Silksong.StartDefs;
using Silksong.ModMenu.Elements;
using Silksong.ModMenu.Screens;
using UnityEngine;

namespace ItemChangerTesting.ShinyTests;

internal abstract class AbstractShinyPlacementTest<T> : Test where T : AbstractShinyPlacementTest<T>, new()
{
    public enum ItemType
    {
        Shiny,
        Rosaries,
        Assortment,
        Persistent,
        Flea,
    }
    public ItemType CurrentItemType = ItemType.Shiny;

    protected virtual string DefaultShinyItem => ItemNames.Surgeon_s_Key;

    protected abstract string MenuName { get; }
    protected abstract string MenuDescription { get; }
    protected abstract int Revision { get; }

    public sealed override TestMetadata GetMetadata() => new()
    {
        Folder = TestFolder.ShinyTests,
        MenuName = MenuName,
        MenuDescription = MenuDescription,
        Revision = Revision
    };

    public int StartDefIndex = 0;
    protected abstract IReadOnlyList<(StartDef StartDef, IReadOnlyList<string> LocationNames)> LocationTests { get; }

    public sealed override void Setup(TestArgs args)
    {
        var tests = LocationTests;
        StartAt(tests[StartDefIndex].StartDef);

        foreach (var locationName in tests.SelectMany(t => t.LocationNames))
        {
            if (Finder.GetLocation(locationName) is not { } location)
            {
                ItemChangerTestingPlugin.Instance.Logger.LogError($"Could not find location '{locationName}'!");
                continue;
            }
                
            var placement = location.Wrap();
            switch (CurrentItemType)
            {
                case ItemType.Shiny:
                    placement.Add(Finder.GetItem(DefaultShinyItem)!);
                    break;
                case ItemType.Rosaries:
                    placement.Add(RosariesItem.MakeRosariesItem(99));
                    break;
                case ItemType.Assortment:
                    placement.WithVariousItems();
                    break;
                case ItemType.Persistent:
                    placement.WithVariousItems().WithAllPersistent();
                    break;
                case ItemType.Flea:
                    placement.Add(Finder.GetItem(ItemNames.Flea)!);
                    placement.Add(RosariesItem.MakeRosariesItem(123));
                    break;
            }
            Profile.AddPlacement(placement);
        }
    }

    private AbstractMenuScreen? _subMenuScreen;
    private AbstractMenuScreen SubMenuScreen => _subMenuScreen ??= CreateSubMenuScreen();

    private void UpdateStartAndWarp()
    {
        StartAt(LocationTests[StartDefIndex].StartDef);
        WarpToStart();
    }

    private AbstractMenuScreen CreateSubMenuScreen()
    {
        ScrollingMenuScreen screen = new($"{MenuName} Sub Tests");

        var tests = LocationTests;
        for (int i = 0; i < tests.Count; i++)
        {
            TextButton b = new(tests[i].LocationNames.First());
            var copy = i;
            b.OnSubmit += () =>
            {
                StartDefIndex = copy;
                UpdateStartAndWarp();
            };
            screen.Add(b);
        }

        screen.OnDispose += () => _subMenuScreen = null;
        return screen;
    }

    public sealed override IEnumerable<(string, Action)> TestMethods()
    {
        var tests = LocationTests;
        if (tests.Count > 1)
        {
            yield return ("Next Start", () =>
            {
                StartDefIndex = (StartDefIndex + 1) % tests.Count;
                UpdateStartAndWarp();
            });
            yield return ("Choose Start", () => MenuScreenNavigation.Show(SubMenuScreen));
        }

        foreach (ItemType itemType in Enum.GetValues(typeof(ItemType)))
        {
            var copy = itemType;
            yield return ($"Item: {itemType}", () => TestDispatcher.StartTest(new T()
            {
                CurrentItemType = copy,
                StartDefIndex = StartDefIndex
            }));
        }
    }
}
