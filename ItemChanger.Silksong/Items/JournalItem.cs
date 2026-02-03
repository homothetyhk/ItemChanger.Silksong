using ItemChanger.Items;
using System.Reflection;

namespace ItemChanger.Silksong.Items
{
    public class JournalItem : Item
    {
        public required string EnemyName { get; init; }

        public override void GiveImmediate(GiveInfo info)
        {
            var pd = PlayerData.instance;
            var killDataField = pd.GetType().GetField("EnemyJournalKillData");
            if (killDataField == null) return;

            var killData = killDataField.GetValue(pd);
            if (killData == null) return;

            var listField = killData.GetType().GetField("list");
            if (listField == null) return;

            var list = listField.GetValue(killData) as System.Collections.IList;
            if (list == null) return;

            foreach (var entry in list)
            {
                var nameField = entry.GetType().GetField("Name");
                if (nameField == null) continue;

                var name = nameField.GetValue(entry) as string;
                if (name != EnemyName) continue;

                var recordField = entry.GetType().GetField("Record");
                if (recordField == null) continue;

                var record = recordField.GetValue(entry);
                if (record == null) continue;

                var killsField = record.GetType().GetField("Kills");
                var seenField = record.GetType().GetField("HasBeenSeen");

                if (killsField != null)
                    killsField.SetValue(record, 1);
                if (seenField != null)
                    seenField.SetValue(record, true);

                recordField.SetValue(entry, record);
                return;
            }
        }

        public override bool Redundant()
        {
            var pd = PlayerData.instance;
            var killDataField = pd.GetType().GetField("EnemyJournalKillData");
            if (killDataField == null) return false;

            var killData = killDataField.GetValue(pd);
            if (killData == null) return false;

            var listField = killData.GetType().GetField("list");
            if (listField == null) return false;

            var list = listField.GetValue(killData) as System.Collections.IList;
            if (list == null) return false;

            foreach (var entry in list)
            {
                var nameField = entry.GetType().GetField("Name");
                if (nameField == null) continue;

                var name = nameField.GetValue(entry) as string;
                if (name != EnemyName) continue;

                var recordField = entry.GetType().GetField("Record");
                if (recordField == null) continue;

                var record = recordField.GetValue(entry);
                if (record == null) continue;

                var seenField = record.GetType().GetField("HasBeenSeen");
                if (seenField == null) return false;

                return (bool)seenField.GetValue(record);
            }

            return false;
        }
    }
}
