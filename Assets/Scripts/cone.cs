﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cone : MonoBehaviour
{
    public Rigidbody rigidbody;
    private Vector3 originalPosition;
    private Quaternion originalRotation;
    public GameObject gameCamera;
    public GameObject pointsScreenCamera;
    public GameObject inGameUi;
    public PointsScreenCounter pointsScreenCounter;
    public DisplayDiffCount displayDiffCount;

    [FMODUnity.EventRef]
    public string ladsJeer = "";
    FMOD.Studio.EventInstance ladsJeerEvent;


    // Start is called before the first frame update
    void Start()
    {
        ladsJeerEvent = FMODUnity.RuntimeManager.CreateInstance(ladsJeer);
        originalPosition = transform.position;
        originalRotation = transform.rotation;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //Detect collisions between the GameObjects with Colliders attached
    void OnCollisionEnter(Collision collision)
    {
        //Check for a match with the specified name on any GameObject that collides with your GameObject
        if (collision.gameObject.name == "CarPhysics")
        {
            ladsJeerEvent.start();
            hitConeEndGame();
            Debug.Log("COLLIDED!!");
        }
    }

    public void hitConeEndGame() {
        gameCamera.SetActive(false);
        pointsScreenCamera.SetActive(true);
        inGameUi.SetActive(false);
        pointsScreenCounter.startCount();
        displayDiffCount.updateDiffCountDisplay();
    }

    public void Reset()
    {
        transform.position = originalPosition;
        transform.rotation = originalRotation;
        rigidbody.isKinematic = true;
        rigidbody.isKinematic = false;
    }
}
