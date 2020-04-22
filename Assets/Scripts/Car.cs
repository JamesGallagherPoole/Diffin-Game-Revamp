using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Car : MonoBehaviour
{
    public GameObject cone;
    public ParticleSystem smoke;

    [HideInInspector] public bool diff = false;

    private int maxSpeed = 130;
    private float maxRotate = 90;
    private float currentSpeed = 0;
    private int currentRotate = 0;
    private int frictionCounter = 10;
    private bool isHendyCounterCounting = false;

    [FMODUnity.EventRef]
    public string hendyCommentary = "";
    FMOD.Studio.EventInstance hendyEvent;

    // Start is called before the first frame update
    void Start()
    {
        // Coroutine for Commentary
        hendyEvent = FMODUnity.RuntimeManager.CreateInstance(hendyCommentary);
        var routine = StartCoroutine(startCommentaryTimer());
    }

    // Update is called once per frame
    void Update()
    {
        if (diff == true) {
            Handheld.Vibrate(); // GOWAN TA FUCK
            // Accelerate
            if (currentSpeed < maxSpeed) {
                currentSpeed += 1;
            }

            // Rotate car based on phone rotation
            if (Input.acceleration.x > 0) {
                if (currentRotate < maxRotate) {
                    currentRotate += 1;
                    transform.Rotate(0, 1, 0);
                }
            } else if (Input.acceleration.x < 0) {
                if (currentRotate > -maxRotate) {
                    currentRotate -= 1;
                    transform.Rotate(0, -1, 0);
                }
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
            Debug.Log(i);
            if (i > 9.0)
            {
                hendyEvent.start();
                isHendyCounterCounting = false;
                break;
            }
            yield return null;
        }
    }

}
