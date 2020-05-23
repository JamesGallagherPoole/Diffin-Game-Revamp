using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Car : MonoBehaviour
{
    public GameObject cone;
    public ParticleSystem smoke;
    public Rigidbody rigidBody;
    public GameObject carPhysics;
    public GameObject resetButton;
    public GreenDieselMode greenDieselMode;
    public GreenDieselMode greenDieselXtraMode;
    public StartGame startGame;

    [HideInInspector] public bool diff = false;

    private Vector3 originalPosition, originalPhysicsPosition;
    private Quaternion originalRotation, originalPhysicsRotation;

    public int maxSpeed = 400;
    private int maxRotate = 60;
    private float currentSpeed = 0;
    private float currentRotate = 0;
    private float rotateIncrement = 0;
    private int frictionCounter = 10;
    private bool isHendyCounterCounting = false;
    private bool isGreenDieselMode = false;

    [FMODUnity.EventRef]
    public string hendyCommentary = "";
    FMOD.Studio.EventInstance hendyEvent;

    [FMODUnity.EventRef]
    public string engineStart = "";
    FMOD.Studio.EventInstance engineStartEvent;

    [FMODUnity.EventRef]
    public string idleEngine = "";
    FMOD.Studio.EventInstance idleEngineEvent;

    [FMODUnity.EventRef]
    public string startDiff = "";
    FMOD.Studio.EventInstance startDiffEvent;

    [FMODUnity.EventRef]
    public string diffLoop = "";
    FMOD.Studio.EventInstance diffLoopEvent;

    [FMODUnity.EventRef]
    public string limiterLoop = "";
    FMOD.Studio.EventInstance limiterLoopEvent;

    [FMODUnity.EventRef]
    public string startDiffWithNoise = "";
    FMOD.Studio.EventInstance startDiffWithNoiseEvent;

    [FMODUnity.EventRef]
    public string endDiff = "";
    FMOD.Studio.EventInstance endDiffEvent;

    [FMODUnity.EventRef]
    public string gravelSound = "";
    FMOD.Studio.EventInstance gravelSoundEvent;
   
    [FMODUnity.EventRef]
    public string gravelBreakSound = "";
    FMOD.Studio.EventInstance gravelBreakSoundEvent;

    //Audio state vars
    private bool isFirstDiff = true;

    // Start is called before the first frame update
    void Start()
    {
        // Load sound events
        diffLoopEvent = FMODUnity.RuntimeManager.CreateInstance(diffLoop);
        idleEngineEvent = FMODUnity.RuntimeManager.CreateInstance(idleEngine);
        engineStartEvent = FMODUnity.RuntimeManager.CreateInstance(engineStart);
        startDiffEvent = FMODUnity.RuntimeManager.CreateInstance(startDiff);
        limiterLoopEvent = FMODUnity.RuntimeManager.CreateInstance(limiterLoop);
        startDiffWithNoiseEvent = FMODUnity.RuntimeManager.CreateInstance(startDiffWithNoise);
        endDiffEvent = FMODUnity.RuntimeManager.CreateInstance(endDiff);
        gravelSoundEvent = FMODUnity.RuntimeManager.CreateInstance(gravelSound);
        gravelBreakSoundEvent = FMODUnity.RuntimeManager.CreateInstance(gravelBreakSound);
        hendyEvent = FMODUnity.RuntimeManager.CreateInstance(hendyCommentary);

        engineStartEvent.start();

        // Coroutine for Commentary
        var routine = StartCoroutine(startCommentaryTimer());

        originalPosition = transform.position;
        originalRotation = transform.rotation;
        originalPhysicsPosition = carPhysics.transform.position;
        originalPhysicsRotation = carPhysics.transform.rotation;
    }

    // Update is called once per frame
    void Update()
    {
        engineStartEvent.set3DAttributes(FMODUnity.RuntimeUtils.To3DAttributes(gameObject));
        idleEngineEvent.set3DAttributes(FMODUnity.RuntimeUtils.To3DAttributes(gameObject));
        startDiffEvent.set3DAttributes(FMODUnity.RuntimeUtils.To3DAttributes(gameObject));
        diffLoopEvent.set3DAttributes(FMODUnity.RuntimeUtils.To3DAttributes(gameObject));
        limiterLoopEvent.set3DAttributes(FMODUnity.RuntimeUtils.To3DAttributes(gameObject));
        startDiffWithNoiseEvent.set3DAttributes(FMODUnity.RuntimeUtils.To3DAttributes(gameObject));
        engineStartEvent.set3DAttributes(FMODUnity.RuntimeUtils.To3DAttributes(gameObject));
        endDiffEvent.set3DAttributes(FMODUnity.RuntimeUtils.To3DAttributes(gameObject));
        gravelSoundEvent.set3DAttributes(FMODUnity.RuntimeUtils.To3DAttributes(gameObject));
        gravelBreakSoundEvent.set3DAttributes(FMODUnity.RuntimeUtils.To3DAttributes(gameObject));

        if (diff == true) {
            Handheld.Vibrate(); // GWAN TA FUCK
            // Accelerate
            if (currentSpeed < maxSpeed) {
                currentSpeed += 2;
            }

            // Rotate car based on phone rotation
            if (Input.acceleration.x > 0) {
                if (currentRotate < maxRotate )
                {
                    currentRotate += 1;
                }
                transform.Rotate(0, 1, 0);
            } else if (Input.acceleration.x < 0) {
                if (currentRotate > -maxRotate)
                {
                    currentRotate -= 1;
                }
                transform.Rotate(0, -1, 0);
            }

            /*
            // Move the position of the car slightly over time
            Debug.Log(Input.acceleration.z);
            if (Input.acceleration.z > -.9f & Input.acceleration.z < -.7f)
            {
                Debug.Log("STAY");
                // Stay at current pos
            }
            else if (Input.acceleration.z < -.9f)
            {
                Debug.Log("Move out");
                transform.Translate(-.8f, 0, 0);
                transform.Translate(0, 0, -.8f);
            }
            else if (Input.acceleration.z > -.7f)
            {
                Debug.Log("Move in");
                transform.Translate(.5f, 0, 0);
                transform.Translate(0, 0, .5f);
            }
            */

            // Create some friction
            if (frictionCounter == 1) {
                transform.Translate(0,.02f,0);
                transform.Rotate(0, 0, .4f);
                frictionCounter -= 1;
            } else if (frictionCounter == 0) {
                transform.Translate(0,-.02f, 0);
                transform.Rotate(0, 0, -.4f);
                frictionCounter = 10;
            } else {
                frictionCounter -= 1;
            }

            // Start timer again to count down to next commentary
            if (isHendyCounterCounting == false)
            {
                var routine = StartCoroutine(startCommentaryTimer());
            }

        } else if (diff == false) {
            if (currentSpeed > 0) {
                currentSpeed -= 3;
            } else if (currentSpeed == 0) {
                smoke.Stop();
            }
        }
        transform.RotateAround(cone.transform.position, transform.up, currentSpeed * Time.deltaTime);

        if (carPhysics.transform.rotation.x > .015 & resetButton.activeSelf == false) {
           // resetButton.SetActive(true);
        }
    }

    public void deactivateResetButton() {
        resetButton.SetActive(false);
    }
    public void getDiffin()
    {
        startDiffEvent.start();
        diff = true;
        smoke.Play();
        gravelSoundEvent.start();

        if (isGreenDieselMode == true) {
            limiterLoopEvent.start();
        }
        idleEngineEvent.stop(FMOD.Studio.STOP_MODE.ALLOWFADEOUT);
        engineStartEvent.stop(FMOD.Studio.STOP_MODE.ALLOWFADEOUT);
    }

    public void haltDiffin()
    {
        diff = false;

        startDiffEvent.stop(FMOD.Studio.STOP_MODE.ALLOWFADEOUT);
        gravelSoundEvent.stop(FMOD.Studio.STOP_MODE.ALLOWFADEOUT);
        limiterLoopEvent.stop(FMOD.Studio.STOP_MODE.ALLOWFADEOUT);
        endDiffEvent.start();
        gravelBreakSoundEvent.start();

        idleEngineEvent.start();
    }

    public void startGreenDieselMode() {
        isGreenDieselMode = true;
        limiterLoopEvent.start();
    }

    // Coroutine counter to play hendys at an interval
    private IEnumerator startCommentaryTimer()
    {
        isHendyCounterCounting = true;
        for (float i = 0; i <= 9.0f; i += Time.deltaTime)
        {
            if (i > 8.0)
            {
                // Make sure start or green diesel mode commentary is not going on
                if (startGame.commentaryOngoing == false & greenDieselMode.commentaryOngoing == false & greenDieselXtraMode.commentaryOngoing == false) {
                    hendyEvent.start();
                    isHendyCounterCounting = false;
                    break;
                } else {
                    isHendyCounterCounting = false;
                    break;
                }
            }
            yield return null;
        }
    }

    public void reset()
    {
        transform.position = originalPosition;
        transform.rotation = originalRotation;
        carPhysics.transform.position = originalPhysicsPosition;
        carPhysics.transform.rotation = originalPhysicsRotation;
        rigidBody.isKinematic = true;
        rigidBody.isKinematic = false;
        isGreenDieselMode = false;
        maxSpeed = 180;
        
    }
    bool IsPlaying(FMOD.Studio.EventInstance instance) {
	    FMOD.Studio.PLAYBACK_STATE state;   
	    instance.getPlaybackState(out state);
	    return state != FMOD.Studio.PLAYBACK_STATE.STOPPED;
    }

}
