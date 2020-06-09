using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameCamera : MonoBehaviour
{
    public Animator animator;
    public Transform car;
    public Transform inCarCam;
    public Transform inFrontCam;
    public Transform onWheelCam;
    public Animator glowAnimator;

    public bool gdmXtraOn = false;

    private int shotNumber = 0;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (gdmXtraOn) {
            if (shotNumber == 0) {
                glowAnimator.SetBool("isShotZero", true);
            } else {
                glowAnimator.SetBool("isShotZero", false);
            } 
        }

        if (shotNumber == 8) {
            transform.position = inFrontCam.position;
            transform.rotation = inFrontCam.rotation;
        } else if (shotNumber == 5) {
            transform.position = inCarCam.position;
            transform.rotation = inCarCam.rotation;
        } else if (shotNumber == 3) {
            transform.position = onWheelCam.position;
            transform.rotation = onWheelCam.rotation;
        }
    }

    public void startGreenDieselMode() {
        animator.SetTrigger("GreenDieselMode");
    }

    public void changeDiffCamShot() {
        if (shotNumber < 5) {
            shotNumber += 1;
        } else {
            shotNumber = 0;
        }

        animator.SetInteger("ShotNumber", shotNumber);

        if (shotNumber == 0) {
            glowAnimator.SetBool("isShotZero", true);
        } else {
            glowAnimator.SetBool("isShotZero", false);
        }

    }

    public void resetCamera() {
        shotNumber = 0;
        animator.SetInteger("ShotNumber", shotNumber);
    }

    public void turnOnApplyRootMotion() {
        animator.applyRootMotion = true;
    }

     public void turnOffApplyRootMotion() {
        animator.applyRootMotion = false;
    }

    public IEnumerator Shake (float duration, float magnitude) {
        Vector3 originalPos = transform.localPosition;

        float elapsed = 0.0f;

        while (elapsed < duration) {
            float x = Random.Range(-1f, 1f) * magnitude;
            float y = Random.Range(-1f, 1f) * magnitude;

            transform.localPosition = new Vector3(x, y, originalPos.z);

            elapsed += Time.deltaTime;
            yield return null;
        }

        transform.localPosition = originalPos;
    }
}
