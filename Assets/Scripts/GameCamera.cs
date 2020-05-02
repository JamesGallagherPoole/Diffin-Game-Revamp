using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameCamera : MonoBehaviour
{
    public Animator animator;
    public Transform car;

    private int shotNumber = 0;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (shotNumber == 3) {
            Debug.Log("HERE");
            transform.LookAt(car);
        }
    }

    public void startGreenDieselMode() {
        animator.SetTrigger("GreenDieselMode");
    }

    public void changeDiffCamShot() {
        shotNumber += 1;
        animator.SetInteger("ShotNumber", shotNumber);
    }
}
