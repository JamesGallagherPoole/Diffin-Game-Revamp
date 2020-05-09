using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DiffAgainButton : MonoBehaviour
{
    public Score score;
    public GameObject gdmXtraButton;
    public GreenDieselMode greenDieselXtraMode;
    public GreenDieselMode greenDieselMode;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void diffAgain() {
        greenDieselXtraMode.reset();
        gdmXtraButton.SetActive(false);
        greenDieselMode.reset();
        greenDieselMode.startCounting();
    }
}
