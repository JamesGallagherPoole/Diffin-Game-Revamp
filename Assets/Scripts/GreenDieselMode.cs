using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GreenDieselMode : MonoBehaviour
{
    public Animator animator;
    public Car car;
    public Button button;
    public Soundtrack soundtrack;

    private bool isAvailable = false;
    private float timerCounter = 0;

    [FMODUnity.EventRef]
    public string greenDieselAvailable = "";
    FMOD.Studio.EventInstance greenDieselAvailableEvent;

    [FMODUnity.EventRef]
    public string greenDieselEngage = "";
    FMOD.Studio.EventInstance greenDieselEngageEvent;

    // Start is called before the first frame update
    void Start()
    {
        greenDieselAvailableEvent = FMODUnity.RuntimeManager.CreateInstance(greenDieselAvailable);
        greenDieselEngageEvent = FMODUnity.RuntimeManager.CreateInstance(greenDieselEngage);
    }

    // Update is called once per frame
    void Update()
    {
        if (car.diff == true)
        {
            timerCounter += Time.deltaTime*6;
            animator.SetFloat("GreenDieselTank", timerCounter);
        }

        if (timerCounter > 100)
        {
            if (isAvailable == false)
            {
                button.interactable = true;
                greenDieselAvailableEvent.start();
                isAvailable = true;
            }
        }
    }

    public void engageGreenDieselMode()
    {
        button.interactable = false;
        animator.SetFloat("GreenDieselTank", 85f);
        greenDieselEngageEvent.start();
        car.maxSpeed = 200;
        soundtrack.startGreenDieselMode();
    }
}
