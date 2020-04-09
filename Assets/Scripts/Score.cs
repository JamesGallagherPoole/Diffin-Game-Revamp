using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Score : MonoBehaviour
{
    public TextMeshProUGUI textComponent;
    public GameObject carObject;
    private Car carScript;
    private int score;

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
        if (carScript.diff == true) {
            score += Mathf.RoundToInt(Random.Range(1.0f, 100.0f)); 
            textComponent.text = score.ToString();
        }
    }
}
