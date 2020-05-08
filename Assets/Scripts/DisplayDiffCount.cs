using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DisplayDiffCount : MonoBehaviour
{
    public TextMeshProUGUI textComponent;
    public DiffCounter diffCounter;
    // Start is called before the first frame update
    void Start()
    {
        textComponent.text = diffCounter.diffCount.ToString();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void updateDiffCountDisplay() {
        textComponent.text = diffCounter.diffCount.ToString();       
    }
}
