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

    // Start is called before the first frame update
    void Start()
    {
        jcashSoundtrackEvent = FMODUnity.RuntimeManager.CreateInstance(jcashSoundtrack);
        jcashSoundtrackEvent.start();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void startGreenDieselMode() {
        jcashSoundtrackEvent.setParameterByName("isGreenDieselMode", 1.0f);
    }

    public void startGreenDieselXtraMode() {
        jcashSoundtrackEvent.setParameterByName("isGreenDieselModeXtra", 1.0f);
    }

    public void steadyOnHey() {
        jcashSoundtrackEvent.setParameterByName("isGreenDieselModeXtra", 0.0f);
        jcashSoundtrackEvent.setParameterByName("isGreenDieselMode", 0.0f);
    }
}