using ItemChanger.Modules;

namespace ItemChanger.Silksong.Modules
{
    /// <summary>
    /// Module which stores requested PlayerData changes and applies them when the save is loaded. Useful for ensuring those changes are applied if the same IC data is stored and loaded with a new save.
    /// </summary>
    public class PlayerDataEditModule : Module
    {
        public Queue<PDEdit> PDEdits { get; init; } = [];
        public List<PDEdit> PDEditHistory { get; init; } = [];

        protected override void DoLoad()
        {
            LifecycleEvents.OnEnterGame += OnEnterGame;
        }

        protected override void DoUnload()
        {
            LifecycleEvents.OnEnterGame -= OnEnterGame;
        }

        private void OnEnterGame()
        {
            while (PDEdits.Count > 0)
            {
                PDEdit edit = PDEdits.Dequeue();
                try
                {
                    edit.Apply();
                }
                catch (Exception e)
                {
                    Logger.LogError($"Error processing {edit}:\n{e}");
                }
                PDEditHistory.Add(edit);
            }
        }

        public void AddPDEdit(string fieldName, object value) => PDEdits.Enqueue(new(fieldName, value));

        public record PDEdit(string FieldName, object Value)
        {
            public void Apply()
            {
                switch (Value)
                {
                    case bool b:
                        PlayerData.instance.SetBool(FieldName, b);
                        return;
                    case int i:
                        PlayerData.instance.SetInt(FieldName, i);
                        return;
                    case long l:
                        PlayerData.instance.SetInt(FieldName, (int)l);
                        return;
                    case string s:
                        PlayerData.instance.SetString(FieldName, s);
                        return;
                    case float f:
                        PlayerData.instance.SetFloat(FieldName, f);
                        return;
                    case double d:
                        PlayerData.instance.SetFloat(FieldName, (float)d);
                        return;
                }
            }
        }
    }
}
