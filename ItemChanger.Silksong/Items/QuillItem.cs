using ItemChanger.Items;

namespace ItemChanger.Silksong.Items
{
    /*
     * Regarding how Quill is handled in the base game:
     * In Library_13b, an fsm manages which quill object is activated. 
       * The quill object gives a PlayerDataCollectable which sets QuillState.
       * The fsm subsequently sets QuillState (again) and hasQuill.
     * Shakra sells the ShopItem "Mapper Quill"
       * The shop item sets QuillState and hasQuill. There is no SavedItem associated to the ShopItem.
     * The inventory uses the CollectableItemStates "Quill" to manage which of the 7 map + quill pair icons to display.
       * This is not meant to be given, so it does not set any PD fields.
     */ 

    /// <summary>
    /// Item which sets the PlayerData fields necessary to give Quill, and which exposes a property to easily access the color of the quill item.
    /// </summary>
    public class QuillItem : Item
    {
        /// <summary>
        /// Controls the color of the quill: 1=White, 2=Red, 3=Purple.
        /// </summary>
        public int QuillState { get; init; } = 1;

        public override void GiveImmediate(GiveInfo info)
        {
            PlayerData.instance.SetInt(nameof(PlayerData.QuillState), QuillState);
            PlayerData.instance.SetBool(nameof(PlayerData.hasQuill), true);
        }

        public override bool Redundant()
        {
            return PlayerData.instance.GetInt(nameof(PlayerData.QuillState)) == QuillState && PlayerData.instance.GetBool(nameof(PlayerData.hasQuill));
        }
    }
}
