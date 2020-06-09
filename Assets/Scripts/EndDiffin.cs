using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndDiffin : MonoBehaviour
{
    [FMODUnity.EventRef]
    public string hendyInsult = "";
    FMOD.Studio.EventInstance hendyInsultEvent;


    // Start is called before the first frame update
    void Start()
    {
        hendyInsultEvent = FMODUnity.RuntimeManager.CreateInstance(hendyInsult);       
    }

    public void playHendyInsult() {
        hendyInsultEvent.start();
    }
}
