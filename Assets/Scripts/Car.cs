using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Car : MonoBehaviour
{
    public GameObject cone;
    public ParticleSystem smoke;

    [HideInInspector] public bool diff = false;

    private Vector3 originalPosition;
    private Quaternion originalRotation;

    private int maxSpeed = 130;
    private float maxRotate = 360;
    private float currentSpeed = 0;
    private float currentRotate = 0;
    private float rotateIncrement = 0;
    private int frictionCounter = 10;
    private bool isHendyCounterCounting = false;

    [FMODUnity.EventRef]
    public string hendyCommentary = "";
    FMOD.Studio.EventInstance hendyEvent;

    [FMODUnity.EventRef]
    public string engineStart = "";
    FMOD.Studio.EventInstance engineStartEvent;

    [FMODUnity.EventRef]
    public string startDiff = "";
    FMOD.Studio.EventInstance startDiffEvent;

    [FMODUnity.EventRef]
    public string diffLoop = "";
    FMOD.Studio.EventInstance diffLoopEvent;

    [FMODUnity.EventRef]
    public string endDiff = "";
    FMOD.Studio.EventInstance endDiffEvent;

    // Start is called before the first frame update
    void Start()
    {
        // Load sound events
        diffLoopEvent = FMODUnity.RuntimeManager.CreateInstance(diffLoop);
        engineStartEvent = FMODUnity.RuntimeManager.CreateInstance(engineStart);
        startDiffEvent = FMODUnity.RuntimeManager.CreateInstance(startDiff);
        endDiffEvent = FMODUnity.RuntimeManager.CreateInstance(endDiff);
        hendyEvent = FMODUnity.RuntimeManager.CreateInstance(hendyCommentary);

        engineStartEvent.start();

        // Coroutine for Commentary
        var routine = StartCoroutine(startCommentaryTimer());

        originalPosition = transform.position;
        originalRotation = transform.rotation;
    }

    // Update is called once per frame
    void Update()
    {
        engineStartEvent.set3DAttributes(FMODUnity.RuntimeUtils.To3DAttributes(gameObject));
        startDiffEvent.set3DAttributes(FMODUnity.RuntimeUtils.To3DAttributes(gameObject));
        diffLoopEvent.set3DAttributes(FMODUnity.RuntimeUtils.To3DAttributes(gameObject));
        engineStartEvent.set3DAttributes(FMODUnity.RuntimeUtils.To3DAttributes(gameObject));
        endDiffEvent.set3DAttributes(FMODUnity.RuntimeUtils.To3DAttributes(gameObject));


        if (diff == true) {
            Handheld.Vibrate(); // GOWAN TA FUCK
            // Accelerate
            if (currentSpeed < maxSpeed) {
                currentSpeed += 1;
            }

            // Rotate car based on phone rotation
            if (Input.acceleration.x > 0) {
                if (rotateIncrement < 1)
                {
                    rotateIncrement += .05f;
                }
                currentRotate += rotateIncrement;
                transform.Rotate(0, 1, 0);
            } else if (Input.acceleration.x < 0) {
                if (rotateIncrement > -1)
                {
                    rotateIncrement -= .05f;
                }
                currentRotate -= rotateIncrement;
                transform.Rotate(0, -1, 0);

            }

            // Move the position of the car slightly over time
            Debug.Log(Input.acceleration.z);
            if (Input.acceleration.z > -1f & Input.acceleration.z < -.7f)
            {
                // Stay at current pos
            }
            else if (Input.acceleration.z < -1f)
            {
                transform.Translate(-0.01f, 0, 0);
                transform.Translate(0, 0, -0.01f);
            }
            else if (Input.acceleration.z > -.7f)
            {
                transform.Translate(0.01f, 0, 0);
                transform.Translate(0, 0, 0.01f);
            }

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
            Debug.Log("X: " + Input.acceleration.x + " Y: " + Input.acceleration.y + " Z: " + Input.acceleration.z);
            if (currentSpeed > 0) {
                currentSpeed -= 1;
            } else if (currentSpeed == 0) {
                smoke.Stop();
            }
        }
        transform.RotateAround(cone.transform.position, transform.up, currentSpeed * Time.deltaTime);
    }

    public void getDiffin()
    {
        diff = true;
        smoke.Play();
        startDiffEvent.start();
    }

    public void haltDiffin()
    {
        diff = false;
        startDiffEvent.stop(FMOD.Studio.STOP_MODE.ALLOWFADEOUT);
        endDiffEvent.start();
    }

    // Coroutine counter to play hendys at an interval
    private IEnumerator startCommentaryTimer()
    {
        isHendyCounterCounting = true;
        for (float i = 0; i <= 10.0f; i += Time.deltaTime)
        {
            if (i > 9.0)
            {
                hendyEvent.start();
                isHendyCounterCounting = false;
                break;
            }
            yield return null;
        }
    }

    public void reset()
    {
        transform.position = originalPosition;
        transform.rotation = originalRotation;
    }

}
