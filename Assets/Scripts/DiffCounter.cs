using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DiffCounter : MonoBehaviour
{
    public int diffCount;
    // Start is called before the first frame update
    void Start()
    {
        Load();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //When the Primitive collides with the walls, it will reverse direction
    private void OnTriggerEnter(Collider other)
    {
        Debug.Log(diffCount);
        diffCount += 1;
        Save();
    }

    private void Load()
    {
        if(PlayerPrefs.HasKey("diffCount"))
        {
            diffCount = PlayerPrefs.GetInt("diffCount");
        }
    }

    private void Save()
    {
        PlayerPrefs.SetInt("diffCount", diffCount);
        PlayerPrefs.Save();
    }
}
