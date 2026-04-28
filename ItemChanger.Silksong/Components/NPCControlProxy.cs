namespace ItemChanger.Silksong.Components;

/// <summary>
/// Fake NPC used to instigate dialogue/lore conversations.
/// 
/// If a conversation is instigated by this component, Hornet will not be animated up
/// or control returned at the end of the conversation (see the SkipEndDialogueAnimModule),
/// so this should be done independently (typically by a container/location).
/// </summary>
internal class NPCControlProxy : NPCControlBase
{
    private static NPCControlProxy? _instance;

    public override bool AutoEnd => false;

    public static NPCControlProxy Instance
    {
        get
        {
            if (_instance == null)
            {
                GameObject go = new("NPC Control Proxy");
                DontDestroyOnLoad(go);
                _instance = go.AddComponent<NPCControlProxy>();
            }

            return _instance;
        }
    }

}
