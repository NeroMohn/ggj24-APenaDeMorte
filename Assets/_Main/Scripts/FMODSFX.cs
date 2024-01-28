using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FMODSFX : MonoBehaviour
{
    private static FMOD.Studio.EventInstance _sfx;

    public void PlayMaleLaught() => SfxBuilder("MaleLaugh");
    public void PlayFemaleLaugh() => SfxBuilder("FemaleLaugh");
    public void PlayPlayerChoice() => SfxBuilder("PlayerChoice");
    public void PlayAttack() => SfxBuilder("Attack");
    public void PlayDraw() => SfxBuilder("Draw");


    private static void SfxBuilder(string sfx)
    {
        _sfx = FMODUnity.RuntimeManager.CreateInstance($"event:/{sfx}");
        _sfx.setVolume(0.4f);
        _sfx.start();
        _sfx.release();
    }

    public enum SfxMap
    {
        MaleLaugh = 1,
        FemaleLaugh = 2,
        PlayerChoice = 3,
        Attack = 4,
        Draw = 5
    };

}
