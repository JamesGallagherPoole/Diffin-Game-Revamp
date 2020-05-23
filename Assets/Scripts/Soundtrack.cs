using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Soundtrack : MonoBehaviour
{
    [FMODUnity.EventRef]
    public string jcashSoundtrack = "";
    FMOD.Studio.EventInstance jcashSoundtrackEvent;
    FMOD.Studio.PARAMETER_ID isGreenDieselModeParameterId;
    FMOD.Studio.PARAMETER_ID isGreenDieselModeXtraParameterId;

    [FMODUnity.EventRef]
    public string gdmXtraSoundtrack = "";
    FMOD.Studio.EventInstance gdmeXtraSoundtrackEvent;

    // Start is called before the first frame update
    void Start()
    {
        jcashSoundtrackEvent = FMODUnity.RuntimeManager.CreateInstance(jcashSoundtrack);
        gdmeXtraSoundtrackEvent = FMODUnity.RuntimeManager.CreateInstance(gdmXtraSoundtrack);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void startGreenDieselMode() {
        jcashSoundtrackEvent.setParameterByName("isGreenDieselMode", 1.0f);
    }

    public void startGreenDieselXtraMode() {
        jcashSoundtrackEvent.stop(FMOD.Studio.STOP_MODE.ALLOWFADEOUT);
        gdmeXtraSoundtrackEvent.start();
    }

    public void startSoundtrack() {
        jcashSoundtrackEvent.start();
    }

    public void steadyOnHey() {
        jcashSoundtrackEvent.setParameterByName("isGreenDieselModeXtra", 0.0f);
        jcashSoundtrackEvent.setParameterByName("isGreenDieselMode", 0.0f);
        gdmeXtraSoundtrackEvent.stop(FMOD.Studio.STOP_MODE.ALLOWFADEOUT);
        jcashSoundtrackEvent.start();
    }
}