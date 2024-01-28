using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FMODMenuMusic : MonoBehaviour
{
    private static FMOD.Studio.EventInstance _music;

    void Start()
    {
        _music = FMODUnity.RuntimeManager.CreateInstance("event:/MainMenuTheme");
        _music.setVolume(0.4f);
        _music.start();
        _music.release();
    }

    public static void ExitMainMenu()
    {
        _music.stop(FMOD.Studio.STOP_MODE.ALLOWFADEOUT);
    }
}
