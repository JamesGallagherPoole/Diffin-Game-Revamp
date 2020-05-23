using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DiffAgainButton : MonoBehaviour
{
    public Score score;
    public PointsScreenCounter pointsScreenCounter;
    public GameObject gdmXtraButton;
    public GreenDieselMode greenDieselXtraMode;
    public GreenDieselMode greenDieselMode;
    public DiffCounter diffCounter;
    public GameCamera gameCamera;

    [FMODUnity.EventRef]
    public string kingOfTheCone = "";
    FMOD.Studio.EventInstance kingOfTheConeEvent;

    // Start is called before the first frame update
    void Start()
    {
        kingOfTheConeEvent = FMODUnity.RuntimeManager.CreateInstance(kingOfTheCone);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void diffAgain() {
        gameCamera.resetCamera();
        greenDieselXtraMode.reset();
        gdmXtraButton.SetActive(false);
        greenDieselMode.reset();
        greenDieselMode.startCounting();
        pointsScreenCounter.endCount();
        kingOfTheConeEvent.start();
        diffCounter.currentDiffCount = 0;
    }
}
