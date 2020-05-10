using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wheel : MonoBehaviour
{
    public GameObject car;
    private Car carScript;
    // Start is called before the first frame update
    void Start()
    {
       carScript = car.GetComponent<Car>(); 
    }

    // Update is called once per frame
    void Update()
    {
        if (carScript.diff == true) {
           transform.Rotate(30f, 0f, 0f);
        }
    }
}
