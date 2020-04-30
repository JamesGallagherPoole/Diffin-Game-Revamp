using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelLoader : MonoBehaviour
{
    public Animator transition;
    public StartGame startGameManager;

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
        Debug.Log("HEY!");
        StartCoroutine(LoadingScreen());
    }

    IEnumerator LoadingScreen()
    {
        // Play Animation
        transition.SetTrigger("Start");

        // Wait for transition
        yield return new WaitForSeconds(1);

        // Change Cameras
        startGameManager.startGame();
        // Play Animation
        transition.SetTrigger("End");
    }
}
