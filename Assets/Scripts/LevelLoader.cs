using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelLoader : MonoBehaviour
{
    public Animator transition;
    public Animator loadingText;
    public Animator hitConeText;
    public Animator loadingStatsText;
    public Animator instructions;
    public StartGame startGameManager;
    public PointsScreenCounter pointsScreenCounter;

    public GameObject gameCamera;
    public GameObject pointsScreenCamera;
    public GameObject inGameUi;
    public GameObject car;
    public Car carScript;

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
    
    public void StartLoadingStatsScreen()
    {
        StartCoroutine(LoadingStatsScreen());
    }

    IEnumerator LoadingScreen()
    {
        // Play Animation
        transition.SetTrigger("Start");
        loadingText.SetTrigger("Start");
        instructions.SetTrigger("Start");

        // Wait for transition
        yield return new WaitForSeconds(3);

        // Change Cameras
        startGameManager.startGame();

        // Play Animation
        instructions.SetTrigger("End");
        transition.SetTrigger("End");
        loadingText.SetTrigger("End");
    }
    
    IEnumerator LoadingHitConeScreen()
    {
        // Play Animation
        transition.SetTrigger("Start");
        hitConeText.SetTrigger("Start");
        inGameUi.SetActive(false);
        carScript.disableCar();
        car.SetActive(false);

        // Wait for transition
        yield return new WaitForSeconds(5);

        gameCamera.SetActive(false);
        pointsScreenCamera.SetActive(true);

        // Play Animation
        transition.SetTrigger("End");
        hitConeText.SetTrigger("End");
        pointsScreenCounter.startCount();
    }

    IEnumerator LoadingStatsScreen()
    {
        // Play Animation
        transition.SetTrigger("Start");
        loadingStatsText.SetTrigger("Start");
        inGameUi.SetActive(false);
        carScript.disableCar();
        car.SetActive(false);

        // Wait for transition
        yield return new WaitForSeconds(3);

        gameCamera.SetActive(false);
        pointsScreenCamera.SetActive(true);

        // Play Animation
        transition.SetTrigger("End");
        loadingStatsText.SetTrigger("End");
        pointsScreenCounter.startCount();
    }
}
