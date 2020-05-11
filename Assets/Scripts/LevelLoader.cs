﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelLoader : MonoBehaviour
{
    public Animator transition;
    public Animator loadingText;
    public Animator hitConeText;
    public StartGame startGameManager;

    public GameObject gameCamera;
    public GameObject pointsScreenCamera;
    public GameObject inGameUi;

    public float loadingLength = 1f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void StartLoadingScreen()
    {
        StartCoroutine(LoadingScreen());
    }

    public void StartHitConeLoadingScreen()
    {
        StartCoroutine(LoadingHitConeScreen());
    }

    IEnumerator LoadingScreen()
    {
        // Play Animation
        transition.SetTrigger("Start");
        loadingText.SetTrigger("Start");

        // Wait for transition
        yield return new WaitForSeconds(3);

        // Change Cameras
        startGameManager.startGame();

        // Play Animation
        transition.SetTrigger("End");
        loadingText.SetTrigger("End");
    }
    
    IEnumerator LoadingHitConeScreen()
    {
        // Play Animation
        transition.SetTrigger("Start");
        hitConeText.SetTrigger("Start");

        // Wait for transition
        yield return new WaitForSeconds(3);

        gameCamera.SetActive(false);
        pointsScreenCamera.SetActive(true);
        inGameUi.SetActive(false);

        // Play Animation
        transition.SetTrigger("End");
        hitConeText.SetTrigger("End");
    }
}
