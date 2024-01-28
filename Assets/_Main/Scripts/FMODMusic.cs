using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Music : MonoBehaviour
{
    private static FMOD.Studio.EventInstance _music;
    void Start()
    {
        _music = FMODUnity.RuntimeManager.CreateInstance("event:/BattleTheme");
        _music.setVolume(0.4f);
        _music.start();
        _music.release();
    }

    private void OnDestroy()
    {
        _music.stop(FMOD.Studio.STOP_MODE.ALLOWFADEOUT);
    }
}
