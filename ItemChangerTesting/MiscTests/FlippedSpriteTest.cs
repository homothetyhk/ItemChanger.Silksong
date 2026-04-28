using Benchwarp.Data;
using ItemChanger.Items;
using ItemChanger.Locations;
using ItemChanger.Placements;
using ItemChanger.Serialization;
using ItemChanger.Silksong.RawData;
using ItemChanger.Silksong.Serialization;
using ItemChanger.Silksong.Serialization.ModifiedSprites;
using ItemChanger.Silksong.Serialization.SpecialSprites;
using ItemChanger.Silksong.UIDefs;
using UnityEngine;

namespace ItemChangerTesting.MiscTests;

internal class FlippedSpriteTest : Test
{
    public override TestMetadata GetMetadata() => new()
    {
        Folder = TestFolder.MiscTests,
        MenuName = "Flipped Sprites",
        MenuDescription = "Tests flipped sprites",
        Revision = 2026031100,
    };

    public override void Setup(TestArgs args)
    {
        StartNear(SceneNames.Tut_02, PrimitiveGateNames.right1);
        Placement unflipped = new CoordinateLocation
        {
            Name = "Unflipped",
            SceneName = SceneNames.Tut_02,
            X = 133.6f,
            Y = 31.57f,
            FlingType = ItemChanger.Enums.FlingType.Everywhere,
            Managed = false,
            ForceDefaultContainer = true,  // Multi shiny because chests aren't implemented
        }.Wrap();

        Placement flipped = new CoordinateLocation
        {
            Name = "Flipped",
            SceneName = SceneNames.Tut_02,
            X = 129.6f,
            Y = 31.57f,
            FlingType = ItemChanger.Enums.FlingType.Everywhere,
            Managed = false,
            ForceDefaultContainer = true,  // Multi shiny because chests aren't implemented
        }.Wrap();

        Placement doubleFlipped = new CoordinateLocation
        {
            Name = "DoubleFlipped",
            SceneName = SceneNames.Tut_02,
            X = 125.6f,
            Y = 31.57f,
            FlingType = ItemChanger.Enums.FlingType.Everywhere,
            Managed = false,
            ForceDefaultContainer = true,  // Multi shiny because chests aren't implemented
        }.Wrap();
        
        Placement projected = new CoordinateLocation
        {
            Name = "Projected",
            SceneName = SceneNames.Tut_02,
            X = 121.6f,
            Y = 31.57f,
            FlingType = ItemChanger.Enums.FlingType.Everywhere,
            Managed = false,
            ForceDefaultContainer = true,  // Multi shiny because chests aren't implemented
        }.Wrap();

        Placement upsideDown = new CoordinateLocation
        {
            Name = "Upside Down",
            SceneName = SceneNames.Tut_02,
            X = 117.6f,
            Y = 31.57f,
            FlingType = ItemChanger.Enums.FlingType.Everywhere,
            Managed = false,
            ForceDefaultContainer = true,  // Multi shiny because chests aren't implemented
        }.Wrap();


        AddItem("Dash", BaseAtlasSprites.Swift_Step);
        AddItem("Cloak", BaseAtlasSprites.Faydown_Cloak);
        // Make sure this one does not have residue from the neighbouring sprite
        AddItem("Cloak2", BaseAtlasSprites.Drifter_s_Faydown_Cloak);
        AddItem("Clapper", ((MsgUIDef)(Finder.GetItem(ItemNames.Journal_Entry__Cogwork_Clapper)!.UIDef!)).Sprite);
        AddItem("BileMap", new BilewaterMapSprite());

        Profile.AddPlacement(unflipped);
        Profile.AddPlacement(flipped);
        Profile.AddPlacement(doubleFlipped);
        Profile.AddPlacement(projected);
        Profile.AddPlacement(upsideDown);

        Profile.AddPlacement(new CoordinateLocation
        {
            Name = "Compare",
            SceneName = SceneNames.Tut_02,
            X = 110.6f,
            Y = 31.57f,
            FlingType = ItemChanger.Enums.FlingType.Everywhere,
            Managed = false,
            ForceDefaultContainer = true,  // Multi shiny because chests aren't implemented
        }
        .Wrap()
        .WithDebugItem(
            BaseAtlasSprites.Swift_Step.FlipX(),
            "SingleFlip")
        .WithDebugItem(BaseAtlasSprites.Swift_Step, "NoFlip")
        .WithDebugItem(
            BaseAtlasSprites.Swift_Step.FlipX().FlipX("Dash Flipped Double"),
            "DoubleFlip"
            )
        .WithDebugItem(
            BaseAtlasSprites.Swift_Step.Project("Dash Project"),
            "Project"
            )
        // Make sure this is different to SingleFlip
        .WithDebugItem(
            BaseAtlasSprites.Swift_Step.FlipY(),
            "UpsideDown"
            ));


        void AddItem(string name, IValueProvider<Sprite> sprite)
        {
            IValueProvider<Sprite> flippedSprite = sprite.FlipX($"Flipped {name}");
            IValueProvider<Sprite> upsideDownSprite = sprite.FlipY($"UpsideDown {name}");
            IValueProvider<Sprite> doubleFlippedSprite = flippedSprite.FlipX($"Flipped/Flipped {name}");

            unflipped.Add(new DebugItem()
            {
                Name = $"Debug Item: {name}",
                UIDef = new MsgUIDef()
                {
                    Name = new BoxedString(name),
                    Sprite = sprite
                }
            });

            flipped.Add(new DebugItem()
            {
                Name = $"Debug Item: {name}",
                UIDef = new MsgUIDef()
                {
                    Name = new BoxedString("F " + name),
                    Sprite = flippedSprite
                }
            });

            doubleFlipped.Add(new DebugItem()
            {
                Name = $"Debug Item: {name}",
                UIDef = new MsgUIDef()
                {
                    Name = new BoxedString("Y " + name),
                    Sprite = doubleFlippedSprite
                }
            });

            projected.Add(new DebugItem()
            {
                Name = $"Debug Item: {name}",
                UIDef = new MsgUIDef()
                {
                    Name = new BoxedString("P " + name),
                    Sprite = sprite.Project($"Projected: {name}")
                }
            });

            upsideDown.Add(new DebugItem()
            {
                Name = $"Debug Item: {name}",
                UIDef = new MsgUIDef()
                {
                    Name = new BoxedString("U " + name),
                    Sprite = upsideDownSprite,
                }
            });
        }
    }
}
