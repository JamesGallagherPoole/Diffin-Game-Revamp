using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ground : MonoBehaviour
{
    public GameObject car;
    private Car carScript;
    public AudioClip[] audioClips;
    public AudioSource audioSource;
    // Start is called before the first frame update
    void Start()
    {
       carScript = car.GetComponent<Car>(); 
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
