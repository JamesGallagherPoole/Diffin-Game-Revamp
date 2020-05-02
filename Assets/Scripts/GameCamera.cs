using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameCamera : MonoBehaviour
{
    public Animator animator;
    public Transform car;
    public Transform inCarCam;

    private int shotNumber = 0;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (shotNumber == 3) {
            transform.LookAt(car);
        } else if (shotNumber == 6) {
            transform.position = inCarCam.position;
            transform.rotation = inCarCam.rotation;
        }
    }

    public void startGreenDieselMode() {
        animator.SetTrigger("GreenDieselMode");
    }

    public void changeDiffCamShot() {
        if (shotNumber < 6) {
            shotNumber += 1;
        } else {
            shotNumber = 0;
        }
        animator.SetInteger("ShotNumber", shotNumber);
    }

    public void turnOnApplyRootMotion() {
        animator.applyRootMotion = true;
    }

     public void turnOffApplyRootMotion() {
        animator.applyRootMotion = false;
    }
}
