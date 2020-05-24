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
    public GameObject screenGlow;
    [HideInInspector] public bool commentaryOngoing = false;

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
            timerCounter += Time.deltaTime*3;
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
        
        if (IsPlaying(greenDieselHendyEvent)) {
            commentaryOngoing = true;
        } else if (commentaryOngoing == true & !IsPlaying(greenDieselHendyEvent)) {
            commentaryOngoing = false;
        }
    }

    public void engageGreenDieselMode()
    { 
        button.interactable = false;
        animator.SetBool("GDM_XTRA", false);
        animator.SetFloat("GreenDieselTank", 85f);
        greenDieselEngageEvent.start();
        greenDieselHendyEvent.start();
        car.maxSpeed = 220;
        car.startGreenDieselMode();
        soundtrack.startGreenDieselMode();
        diffCamButton.startGreenDieselMode();
        gameCamera.startGreenDieselMode();
        //StartCoroutine(gameCamera.Shake(1f, 1f));
        greenDieselXtraModeObject.SetActive(true);
        greenDieselXtraMode.startCounting();
        isCounting = false;
    }

    public void engageGreenDieselXtraMode()
    {
        button.interactable = false;
        animator.SetBool("GDM_XTRA", true);
        animator.SetFloat("GreenDieselTank", 85f);
        greenDieselEngageEvent.start();
        greenDieselHendyEvent.start();
        soundtrack.startGreenDieselXtraMode();
        //StartCoroutine(gameCamera.Shake(.2f, .5f));
        car.maxSpeed = 240;
        gameCamera.gdmXtraOn = true;
        setGreenSmoke();
        screenGlow.SetActive(true);
        isCounting = false;
    }

    public void startCounting() {
        isCounting = true;
    }

    public void reset() {
        screenGlow.SetActive(false);
        timerCounter = 0;
        isAvailable = false;
        gameCamera.gdmXtraOn = false;
        isCounting = true;
        button.interactable = false;
        resetSmoke();
    }

    void resetSmoke() {
        var main = smoke.colorOverLifetime;
        Gradient gradient = new Gradient();

        // Populate the color keys
        colorKey = new GradientColorKey[2];
        colorKey[0].color = Color.white;
        colorKey[0].time = 0.0f;
        colorKey[1].color = Color.white;
        colorKey[1].time = 0.50f;

        // Populate the alpha keys
        alphaKey = new GradientAlphaKey[3];
        alphaKey[0].alpha = 1.0f;
        alphaKey[0].time = 0.0f;
        alphaKey[1].alpha = 0.30f;
        alphaKey[1].time = 0.38f;
        alphaKey[2].alpha = 0.0f;
        alphaKey[2].time = 0.68f;

        gradient.SetKeys( colorKey, alphaKey);
        main.color = gradient;
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
    bool IsPlaying(FMOD.Studio.EventInstance instance) {
	    FMOD.Studio.PLAYBACK_STATE state;   
	    instance.getPlaybackState(out state);
	    return state != FMOD.Studio.PLAYBACK_STATE.STOPPED;
    } 
}
