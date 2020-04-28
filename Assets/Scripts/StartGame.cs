using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartGame : MonoBehaviour
{
    public GameObject menuCamera;
    public GameObject gameCamera;
    public GameObject car;

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
        
    }

    public void startGame()
    {
        menuCamera.SetActive(false);
        gameCamera.SetActive(true);
        car.SetActive(true);
    }

    public void playDiffVoice()
    {
        diffVoiceEvent.start();
    }
}
