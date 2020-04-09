using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Car : MonoBehaviour
{
    private Vector3 cone = new Vector3(0.0f, 0.0f, 0.0f);

    public bool diff = false;

    public GameObject engineStartSoundObject;
    public GameObject idleSoundObject;
    public GameObject diffSoundObject;
    public GameObject limiterSoundObject;
    public GameObject diffLoopSoundObject;

    private AudioSource engineStartSound;
    private AudioSource idleSound;
    private AudioSource diffSound;
    private AudioSource limiterSound;
    private AudioSource diffLoopSound;

    private int maxSpeed = 130;
    private float maxRotate = 90;
    private float currentSpeed = 0;
    private int currentRotate = 0;

    private int frictionCounter = 10;

    private int limiterCounter = 300;

    // Start is called before the first frame update
    void Start()
    {
        idleSound = idleSoundObject.GetComponent<AudioSource>();
        diffSound = diffSoundObject.GetComponent<AudioSource>();
        diffLoopSound = diffLoopSoundObject.GetComponent<AudioSource>();
        limiterSound = limiterSoundObject.GetComponent<AudioSource>();
        engineStartSound = engineStartSoundObject.GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        if (diff == true) {
            limiterCounter -= 1;
            if (idleSound.volume > .4f) {
                idleSound.volume -= .1f;
            }

            if (diffLoopSound.volume < 1) {
                diffLoopSound.volume += .1f;
            }

            if (limiterCounter == 0) {
                diffLoopSound.volume = .4f;
                if (limiterSound.volume < 1) {
                    limiterSound.volume = 1;
                }
            }

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
        } else if (diff == false) {
            limiterCounter = 1000;
            limiterSound.volume = 0;
            if (idleSound.volume < .8f) {
                idleSound.volume += .2f;
            }
            if (diffLoopSound.volume > 0) {
                diffLoopSound.volume -= .1f;
            }
            if (currentSpeed > 0) {
                currentSpeed -= 5;
            }
        }
        transform.RotateAround(cone, Vector3.up, currentSpeed * Time.deltaTime);
    }

    public void getDiffin()
    {
        diff = true;
        diffSound.Play();
    }

    public void haltDiffin()
    {
        diff = false;
    }
}
