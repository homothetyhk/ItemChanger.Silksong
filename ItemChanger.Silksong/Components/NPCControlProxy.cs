namespace ItemChanger.Silksong.Components;

internal class NPCControlProxy : NPCControlBase
{
    private static NPCControlProxy? _instance;

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
