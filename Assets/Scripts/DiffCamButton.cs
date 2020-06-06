using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DiffCamButton : MonoBehaviour
{
    public Button button;
    public Animator animator;

    [FMODUnity.EventRef]
    public string swoosh = "";
    FMOD.Studio.EventInstance swooshEvent;

    // Start is called before the first frame update
    void Start()
    {
        swooshEvent = FMODUnity.RuntimeManager.CreateInstance(swoosh);
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void startGreenDieselMode() {
        button.interactable = true;
        animator.SetBool("GreenDieselMode", true);
    }

    public void playCameraSwitchSound() {
        swooshEvent.start();
    }
}
