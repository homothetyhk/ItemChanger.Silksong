using ItemChanger.Items;

namespace ItemChanger.Silksong.Items
{
    public class QuillItem : Item
    {
        public int QuillState { get; set; } = 1;

        public override void GiveImmediate(GiveInfo info)
        {
            var pd = GameManager.instance?.playerData;
            if (pd == null) return;
            
            pd.SetInt("QuillState", QuillState);
            pd.SetBool("hasQuill", true);
            
            var quillItem = CollectableItemManager.GetItemByName("Quill");
            quillItem?.Collect(1, showPopup: false);
            
            string colorName = QuillState == 1 ? "White" : QuillState == 2 ? "Red" : "Purple";
            ItemChangerPlugin.Instance.Logger.LogInfo($"Obtained {colorName} Quill (State {QuillState})");
        }

        public override bool Redundant()
        {
            var pd = GameManager.instance?.playerData;
            if (pd == null) return false;
            return pd.GetInt("QuillState") == QuillState;
        }
    }
}
