using ItemChanger.Silksong.Components;

namespace ItemChanger.Silksong.Extensions;

public static class PlayMakerFSMExtensions
{
    extension(GameObject obj)
    {
        /// <summary>
        /// If the object is active, immediately edits its fsm. If it is inactive, adds an <see cref="EditFsmOnEnable"/> to edit the fsm once it is activated.
        /// </summary>
        public void EditFsm(string fsmName, Action<PlayMakerFSM> edit)
        {
            if (obj.activeInHierarchy)
            {
                edit(obj.LocateMyFSM(fsmName));
            }
            else
            {
                EditFsmOnEnable editor = obj.AddComponent<EditFsmOnEnable>();
                editor.FsmName = fsmName;
                editor.Edit = edit;
            }
        }
    }
}
