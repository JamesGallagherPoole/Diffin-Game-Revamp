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
    public string diffLoop = "";
    FMOD.Studio.EventInstance diffLoopEvent;

    // Start is called before the first frame update
    void Start()
    {
        // Coroutine for Commentary
        hendyEvent = FMODUnity.RuntimeManager.CreateInstance(hendyCommentary);
        var routine = StartCoroutine(startCommentaryTimer());

        diffLoopEvent = FMODUnity.RuntimeManager.CreateInstance(diffLoop);

        diffLoopEvent.start();
        originalPosition = transform.position;
        originalRotation = transform.rotation;
    }

    // Update is called once per frame
    void Update()
    {
        diffLoopEvent.set3DAttributes(FMODUnity.RuntimeUtils.To3DAttributes(gameObject));
        if (diff == true) {
            //Handheld.Vibrate(); // GOWAN TA FUCK
            // Accelerate
            if (currentSpeed < maxSpeed) {
                currentSpeed += 1;
            }

            // Rotate car based on phone rotation
            if (Input.acceleration.x > 0) {
                if (currentRotate < maxRotate) {
                    if (rotateIncrement < 1)
                    {
                        rotateIncrement += .05f;
                    }
                    currentRotate += rotateIncrement;
                    transform.Rotate(0, 1, 0);
                }
            } else if (Input.acceleration.x < 0) {
                if (currentRotate > -maxRotate) {
                    if (rotateIncrement > -1)
                    {
                        rotateIncrement -= .05f;
                    }
                    currentRotate -= rotateIncrement;
                    transform.Rotate(0, -1, 0);
                }
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
    }

    public void haltDiffin()
    {
        diff = false;
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
