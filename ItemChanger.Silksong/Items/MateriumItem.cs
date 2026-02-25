using ItemChanger.Items;

namespace ItemChanger.Silksong.Items
{
    public class MateriumItem : Item
    {
        public required string MateriumName { get; init; }

        public override void GiveImmediate(GiveInfo info)
        {
            var materiumData = PlayerData.instance.MateriumCollected;
            if (materiumData == null) return;

            for (int i = 0; i < materiumData.savedData.Count; i++)
            {
                var entry = materiumData.savedData[i];
                if (entry.Name != MateriumName) continue;
                var data = entry.Data;
                data.IsCollected = true;
                entry.Data = data;
                materiumData.savedData[i] = entry;
                return;
            }
        }

        public override bool Redundant()
        {
            var materiumData = PlayerData.instance.MateriumCollected;
            if (materiumData == null) return false;

            foreach (var entry in materiumData.savedData)
            {
                if (entry.Name != MateriumName) continue;
                return entry.Data.IsCollected;
            }

            return false;
        }
    }
}
