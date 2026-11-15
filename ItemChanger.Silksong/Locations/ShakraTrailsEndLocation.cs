using ItemChanger.Locations;
using HutongGames.PlayMaker;
using HutongGames.PlayMaker.Actions;
using Silksong.FsmUtil;
using ItemChanger.Silksong.Extensions;
using UnityEngine;

namespace ItemChanger.Silksong.Locations;

// TODO: add module to modify Trail's End start condition and to enforce Needolin in quest requirement.
public class ShakraTrailsEndLocation : AutoLocation
{
    /// <summary>
    /// Shortens the cutscene by around 20 seconds, by reducing waits and truncating the song.
    /// </summary>
    public bool Hasten { get; init; }

    protected override void DoLoad()
    {
        Using(new FsmEditGroup()
        {
            {new(UnsafeSceneName, "Mapper Master NPC", "Dialogue"), HookShakra},
        });
    }

    protected override void DoUnload() {}

    private void HookShakra(PlayMakerFSM fsm)
    {
        FsmState rewardState = fsm.MustGetState("Reward");
        int i = rewardState.IndexFirstActionOfType<SavedItemGet>();
        rewardState.RemoveAction(i);
        rewardState.InsertLambdaMethod(i, this.CreateGiveAllDelegate(fsm.transform));

        if (Hasten) HastenCutscene(fsm);
    }

    private void HastenCutscene(PlayMakerFSM fsm)
    {
        const float delay = 0.1f;

        FsmState sing = fsm.MustGetState("Sing");
        sing.GetFirstActionOfType<Wait>()!.time = delay; // 2f, second Wait of length 2f ignored

        FsmState playingNeedolin = fsm.MustGetState("Playing Needolin");
        playingNeedolin.GetFirstActionOfType<Wait>()!.time = delay; // 4f

        FsmState waitForLoopEnd = fsm.MustGetState("Wait For Loop End");
        waitForLoopEnd.InsertMethod(0, _ => fsm.SendEvent("NEXT")); // around 10f depending on clip time

        FsmState songEnd = fsm.MustGetState("Song End");
        AudioPlayRandomVoice[] audios = songEnd.GetActionsOfType<AudioPlayRandomVoice>();
        songEnd.RemoveActionsOfType<AudioPlayRandomVoice>();
        int songEndWait = songEnd.IndexFirstActionOfType<Wait>();
        songEnd.InsertMethod(songEndWait, _ =>
        {
            AudioSource voice = fsm.Fsm.GetOwnerDefaultTarget(audios[0].gameObject).GetComponent<AudioSource>();
            AudioSource needolin = fsm.Fsm.GetOwnerDefaultTarget(audios[1].gameObject).GetComponent<AudioSource>();
            voice.Stop();
            needolin.Stop();
            voice.loop = false;
            needolin.loop = false;
            voice.clip = audios[0].audioClips[0];
            needolin.clip = audios[1].audioClips[0];
            voice.Play();
            needolin.Play();
            voice.time = 7.5f;
            needolin.time = 7.5f;
            
        });
        songEnd.GetFirstActionOfType<Wait>()!.time = 4f; // 12f

        //FsmState singEnd = fsm.MustGetState("Sing End");
        //singEnd.GetFirstActionOfType<Wait>()!.time = delay; // 1f, second Wait of length 0.3f ignored

        FsmState singEndIdle = fsm.MustGetState("Sing End Idle");
        singEndIdle.GetFirstActionOfType<Wait>()!.time = delay; // 2.5f

        FsmState completeQuest = fsm.MustGetState("Complete Quest");
        completeQuest.GetFirstActionOfType<Wait>()!.time = delay; // 0.5f

        FsmState reward = fsm.MustGetState("Reward");
        reward.GetFirstActionOfType<Wait>()!.time = delay; // 0.5f, second Wait of length 1f ignored.
    }
}