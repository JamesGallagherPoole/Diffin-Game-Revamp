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
    public ParticleSystem smoke;
    public GameCamera gameCamera;
    public DiffCamButton diffCamButton;
    public GameObject greenDieselXtraModeObject;
    public GreenDieselMode greenDieselXtraMode;

    private GradientColorKey[] colorKey;
    private GradientAlphaKey[] alphaKey;

    private bool isAvailable = false;
    private float timerCounter = 0;
    private bool isCounting = false;

    [FMODUnity.EventRef]
    public string greenDieselAvailable = "";
    FMOD.Studio.EventInstance greenDieselAvailableEvent;

    [FMODUnity.EventRef]
    public string greenDieselEngage = "";
    FMOD.Studio.EventInstance greenDieselEngageEvent;

    [FMODUnity.EventRef]
    public string greenDieselHendy = "";
    FMOD.Studio.EventInstance greenDieselHendyEvent;

    // Start is called before the first frame update
    void Start()
    {
        greenDieselAvailableEvent = FMODUnity.RuntimeManager.CreateInstance(greenDieselAvailable);
        greenDieselEngageEvent = FMODUnity.RuntimeManager.CreateInstance(greenDieselEngage);
        greenDieselHendyEvent = FMODUnity.RuntimeManager.CreateInstance(greenDieselHendy);
    }

    // Update is called once per frame
    void Update()
    {
        if (car.diff == true & isCounting)
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
        greenDieselHendyEvent.start();
        car.maxSpeed = 200;
        soundtrack.startGreenDieselMode();
        gameCamera.startGreenDieselMode();
        diffCamButton.startGreenDieselMode();
        setGreenSmoke();
        greenDieselXtraModeObject.SetActive(true);
        greenDieselXtraMode.startCounting();

    }

    public void engageGreenDieselXtraMode()
    {
        button.interactable = false;
        animator.SetFloat("GreenDieselTank", 85f);
        greenDieselEngageEvent.start();
        car.maxSpeed = 240;
        soundtrack.startGreenDieselMode();
    }

    public void startCounting() {
        isCounting = true;
    }

    void setGreenSmoke() {
        var main = smoke.colorOverLifetime;
        Gradient gradient = new Gradient();

        // Populate the color keys
        colorKey = new GradientColorKey[2];
        colorKey[0].color = Color.green;
        colorKey[0].time = 0.0f;
        colorKey[1].color = Color.white;
        colorKey[1].time = 0.50f;

        // Populate the alpha keys
        alphaKey = new GradientAlphaKey[3];
        alphaKey[0].alpha = 1.0f;
        alphaKey[0].time = 0.0f;
        alphaKey[1].alpha = 0.79f;
        alphaKey[1].time = 0.38f;
        alphaKey[2].alpha = 0.0f;
        alphaKey[2].time = 0.68f;

        gradient.SetKeys( colorKey, alphaKey);
        main.color = gradient;
    }
}
