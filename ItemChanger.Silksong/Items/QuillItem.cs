using ItemChanger.Items;

namespace ItemChanger.Silksong.Items
{
    public class QuillItem : Item
    {
        public int QuillState { get; set; } = 1;

        public override void GiveImmediate(GiveInfo info)
        {
            PlayerData.instance.SetInt("QuillState", QuillState);
            PlayerData.instance.SetBool("hasQuill", true);
            
            var quillItem = CollectableItemManager.GetItemByName("Quill");
            quillItem?.Collect(1, showPopup: false);
        }

        public override bool Redundant()
        {
            return PlayerData.instance.GetInt("QuillState") == QuillState;
        }
    }
}
