using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PointsScreenCounter : MonoBehaviour
{
    public TextMeshProUGUI textComponent;
    public Score score;
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
        totalScore = score.score;
        currentScore = 0;

        scoreSound = FMODUnity.RuntimeManager.CreateInstance(ScoreCountEvent);
        FMOD.Studio.PARAMETER_DESCRIPTION isFinishedCountingParameterDescription;
        scoreEventDescription.getParameterDescriptionByName("isFinishedCounting", out isFinishedCountingParameterDescription);
        isFinishedCountingParameterId = isFinishedCountingParameterDescription.id;
        scoreSound.start();
    }

    // Update is called once per frame
    void Update()
    {
        if (currentScore < totalScore)
        {
            currentScore = currentScore + 10;
            textComponent.text = currentScore.ToString();
        } else
        {
            scoreSound.setParameterByID(isFinishedCountingParameterId, 1.0f);
        }
    }
}
