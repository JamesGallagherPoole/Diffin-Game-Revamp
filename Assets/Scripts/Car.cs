using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Car : MonoBehaviour
{
    public GameObject cone;

    public bool diff = false;

    private int maxSpeed = 130;
    private float maxRotate = 90;
    private float currentSpeed = 0;
    private int currentRotate = 0;

    private int frictionCounter = 10;

    // Start is called before the first frame update
    void Start()
    {

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
        } else if (diff == false) {
            if (currentSpeed > 0) {
                currentSpeed -= 1;
            }
        }
        transform.RotateAround(cone.transform.position, transform.up, currentSpeed * Time.deltaTime);
    }

    public void getDiffin()
    {
        diff = true;
    }

    public void haltDiffin()
    {
        diff = false;
    }
}
