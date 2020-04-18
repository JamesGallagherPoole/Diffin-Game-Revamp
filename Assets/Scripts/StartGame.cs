using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartGame : MonoBehaviour
{
    public GameObject menuCamera;
    public GameObject gameCamera;
    public GameObject car;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void startGame()
    {
        menuCamera.SetActive(false);
        gameCamera.SetActive(true);
        car.SetActive(true);
    }


}
