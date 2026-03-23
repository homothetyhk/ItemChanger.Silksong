using ItemChanger.Serialization;
using Silksong.DataManager;

namespace ItemChanger.Silksong;

public partial class ItemChangerPlugin : IRawSaveDataMod
{
    // used by SilksongHost to invoke OnLeaveGame.
    internal event Action? BeforeProfileDispose;

    bool IRawSaveDataMod.HasSaveData => Host.ActiveProfile != null;

    void IRawSaveDataMod.WriteSaveData(Stream saveFile)
    {
        // WriteSaveData is never called if Host.ActiveProfile is null.
        SerializationHelper.Serialize(saveFile, Host.ActiveProfile!);
    }

    void IRawSaveDataMod.ReadSaveData(Stream? saveFile)
    {
        // Can't just overwrite Host.ActiveProfile, because the profile needs to be manually
        // Disposed. This applies both when returning to the main menu, and also when using
        // Benchwarp (which reloads the file without passing through the main menu).
        if (Host.ActiveProfile != null)
        {
            BeforeProfileDispose?.Invoke();
            Host.ActiveProfile.Dispose();
        }
        if (saveFile != null)
        {
            _ = ItemChangerProfile.FromStream(Host, saveFile);
        }
    }
}
