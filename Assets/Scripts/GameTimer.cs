using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameTimer : MonoBehaviour
{
    public TextMeshProUGUI textComponent;
    private float gameTime;
    private float currentTimeLeft;
    private float currentTimeLeftRound;

    public GameObject gameCamera;
    public GameObject pointsScreenCamera;
    public PointsScreenCounter pointsScreenCounter;

    // Start is called before the first frame update
    void Start()
    {
        gameTime = 30;
        resetTimer();
    }

    // Update is called once per frame
    void Update()
    {
        if (currentTimeLeft > 0)
        {
            currentTimeLeft -= Time.deltaTime;
            currentTimeLeftRound = Mathf.Floor(currentTimeLeft);
            textComponent.text = currentTimeLeftRound.ToString();
        } else if (currentTimeLeft <= 0)
        {
            gameCamera.SetActive(false);
            pointsScreenCamera.SetActive(true);
            pointsScreenCounter.startCount();
        }
    }

    public void resetTimer()
    {
        currentTimeLeft = gameTime;
    }
}
