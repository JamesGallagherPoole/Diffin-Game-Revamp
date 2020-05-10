using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PointsScreenCounter : MonoBehaviour
{
    public TextMeshProUGUI textComponent;
    public DiffCounter diffCount;
    private int totalScore;
    private int currentScore;

    [FMODUnity.EventRef]
    public string ScoreCountEvent = "";
    FMOD.Studio.EventInstance scoreSound;
    FMOD.Studio.EventDescription scoreEventDescription;
    FMOD.Studio.PARAMETER_ID isFinishedCountingParameterId;

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        if (currentScore < totalScore)
        {
            currentScore = currentScore + 1;
            textComponent.text = currentScore.ToString();
        } else
        {
            endCount();
        }
    }

    public void startCount()
    {
        totalScore = diffCount.currentDiffCount;
        currentScore = 0;
        scoreSound = FMODUnity.RuntimeManager.CreateInstance(ScoreCountEvent);
        scoreSound.start();
    }

    public void endCount()
    {
        scoreSound.setParameterByName("isFinishedCounting", 1.0f);
    }
}
