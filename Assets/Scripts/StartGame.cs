using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartGame : MonoBehaviour
{
    public GameObject menuCamera;
    public GameObject gameCamera;
    public GameObject car;
    public GreenDieselMode greenDieselMode;
    public bool commentaryOngoing = false;

    [FMODUnity.EventRef]
    public string diffVoice = "";
    FMOD.Studio.EventInstance diffVoiceEvent;

    // Start is called before the first frame update
    void Start()
    {
        diffVoiceEvent = FMODUnity.RuntimeManager.CreateInstance(diffVoice);
    }

    // Update is called once per frame
    void Update()
    {
        if (IsPlaying(diffVoiceEvent)) {
            commentaryOngoing = true;
        } else if (commentaryOngoing == true & !IsPlaying(diffVoiceEvent)) {
            commentaryOngoing = false;
        }
    }

    public void startGame()
    {
        commentaryOngoing = false;
        menuCamera.SetActive(false);
        gameCamera.SetActive(true);
        car.SetActive(true);
        greenDieselMode.startCounting();
    }

    public void playDiffVoice()
    {
        diffVoiceEvent.start();
    }

    bool IsPlaying(FMOD.Studio.EventInstance instance) {
	    FMOD.Studio.PLAYBACK_STATE state;   
	    instance.getPlaybackState(out state);
	    return state != FMOD.Studio.PLAYBACK_STATE.STOPPED;
    } 

}
