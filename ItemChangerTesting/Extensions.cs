using GenericVariableExtension;
using ItemChanger;
using ItemChanger.Costs;
using ItemChanger.Enums;
using ItemChanger.Items;
using ItemChanger.Placements;
using ItemChanger.Serialization;
using ItemChanger.Silksong.Extensions;
using ItemChanger.Silksong.UIDefs;
using ItemChanger.Tags;
using System.Reflection;
using UnityEngine;

namespace ItemChangerTesting;

internal static class Extensions
{
    public static Placement WithDebugItem(
        this Placement self, 
        IValueProvider<Sprite>? sprite = null,
        string? text = null,
        Persistence persistence = Persistence.NonPersistent)
    => self.Add(new DebugItem()
    {
        Name = $"Debug Item @ {self.Name}",
        UIDef = new MsgUIDef()
        {
            Name = new BoxedString(text ?? $"Checked {self.Name}"),
            Sprite = sprite ?? new EmptySprite(),
        }
    }.WithTag(new PersistentItemTag() { Persistence = persistence }));

    public static Item WithCost(this Item self, Cost cost)
    {
        self.AddTag(new CostTag() { Cost = cost });
        return self;
    }

    public static Item WithCosts(this Item self, params Cost[] costs) => self.WithCost(new MultiCost(costs));

    public static void SetAvailable(this FullQuestBase quest)
    {
        foreach (var q in quest.requiredCompleteQuests) q?.SetCompleted();
        foreach (var t in quest.requiredUnlockedTools)
        {
            if (t == null) continue;
            if (t.alternateUnlockedTest.IsDefined) t.alternateUnlockedTest.Fulfill();
            else
            {
                var d = t.SavedData;
                d.IsUnlocked = true;
                t.SavedData = d;
            }
        }
        foreach (var g in quest.requiredCompleteTotalGroups) g?.Fulfill();
        foreach (var b in quest.persistentBoolTests ?? []) b?.Fulfill();
        if (quest.playerDataTest.IsDefined) quest.playerDataTest.Fulfill();
    }

    public static void SetReadyToComplete(this FullQuestBase quest)
    {
        quest.SetSeen();
        quest.SetAccepted();

        foreach (FullQuestBase.QuestTarget target in quest.Targets)
        {
            if (target.AltTest.IsDefined)
            {
                target.AltTest.Fulfill();
            }
            else if (!target.Counter)
            {
                quest.ModifyCompletion((ref c) => c.CompletedCount = target.Count);
            }
            else
            {
                int needed = target.Count - target.Counter.GetCompletionAmount(quest.Completion);
                if (needed <= 0) continue;

                if (target.Counter.CanGetMultipleAtOnce)
                {
                    target.Counter.Get(needed, showPopup: false);
                }
                else
                {
                    for (int i = 0; i < needed; i++) target.Counter.Get(showPopup: false);
                }
            }
        }
    }

    public static void SetCompleted(this FullQuestBase quest)
    {
        quest.SetReadyToComplete();
        quest.ModifyCompletion((ref c) => c.SetCompleted());
    }

    public static void Fulfill(this PersistentBoolTest test)
    {
        if (!SceneData.instance.PersistentBools.TryGetValue(test.SceneName, test.ID, out PersistentItemData<bool> value))
        {
            value = new()
            {
                ID = test.ID,
                SceneName = test.SceneName,
                IsSemiPersistent = false,
            };
        }

        value.Value = test.ExpectedValue;
        SceneData.instance.PersistentBools.SetValue(value);
    }

    public static void Fulfill(this QuestCompleteTotalGroup group)
    {
        if (group.additionalTest.IsDefined) group.additionalTest.Fulfill();
        foreach (var quest in group.Quests) quest.Quest?.SetCompleted();
    }

    public static void Fulfill(this PlayerDataTest test)
    {
        if (test is null || test.TestGroups is null || test.TestGroups.Length == 0)
        {
            throw new ArgumentException("Cannot fulfill test: test has no test groups.");
        }

        test.TestGroups[0].Fulfill();
    }

    public static void Fulfill(this PlayerDataTest.TestGroup group)
    {
        foreach (PlayerDataTest.Test t in group.Tests) t.Fulfill();
    }

    public static void Fulfill(this PlayerDataTest.Test test)
    {
        if (!test.IsFulfilled(PlayerData.instance))
        {
            if (test.Type == PlayerDataTest.TestType.String && test.StringType == PlayerDataTest.StringTestType.NotEqual)
            {
                throw new NotSupportedException("PD string-ne test is not supported by TryFulfill.");
            }
            if ((test.Type == PlayerDataTest.TestType.Int || test.Type == PlayerDataTest.TestType.Float || test.Type == PlayerDataTest.TestType.Enum)
                && (test.NumType == PlayerDataTest.NumTestType.NotEqual || test.NumType == PlayerDataTest.NumTestType.LessThan))
            {
                throw new NotSupportedException("PD num-ne or num-lt test is not supported by TryFulfill.");
            }

            switch (test.Type)
            {
                case PlayerDataTest.TestType.Bool:
                    PlayerData.instance.SetBool(test.FieldName, test.BoolValue);
                    break;
                case PlayerDataTest.TestType.Enum when test.NumType == PlayerDataTest.NumTestType.Equal:
                    FieldInfo fi = typeof(PlayerData).GetField(test.FieldName);
                    PlayerData.instance.SetVariable(test.FieldName, Enum.ToObject(fi.FieldType, test.IntValue), fi.FieldType);
                    break;
                case PlayerDataTest.TestType.Int when test.NumType == PlayerDataTest.NumTestType.Equal:
                    PlayerData.instance.SetInt(test.FieldName, test.IntValue);
                    break;
                case PlayerDataTest.TestType.Int when test.NumType == PlayerDataTest.NumTestType.MoreThan:
                    PlayerData.instance.SetInt(test.FieldName, test.IntValue + 1);
                    break;
                case PlayerDataTest.TestType.Int when test.NumType == PlayerDataTest.NumTestType.LessThan:
                    PlayerData.instance.SetInt(test.FieldName, test.IntValue - 1);
                    break;
                case PlayerDataTest.TestType.String when test.StringType == PlayerDataTest.StringTestType.Equal || test.StringType == PlayerDataTest.StringTestType.Contains:
                    PlayerData.instance.SetString(test.FieldName, test.StringValue);
                    break;
                case PlayerDataTest.TestType.String:
                    throw new NotSupportedException("PD string-ne or string-nc test is not supported by TryFulfill.");
                default:
                    throw new NotSupportedException("PD num-ne or similar test is not supported by TryFulfill.");
            }
        }
    }

}
