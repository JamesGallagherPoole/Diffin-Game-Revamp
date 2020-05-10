using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Score : MonoBehaviour
{
    public TextMeshProUGUI textComponent;
    public GameObject carObject;
    public DiffCounter diffCounter;
    private Car carScript;
    [HideInInspector] public int score;

    void Start()
    {
        carScript = carObject.GetComponent<Car>();
        score = 0;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void FixedUpdate()
    {
        textComponent.text = diffCounter.currentDiffCount.ToString();
    }

    public void setToZero()
    {
        score = 0;
        textComponent.text = score.ToString();
    }
}
