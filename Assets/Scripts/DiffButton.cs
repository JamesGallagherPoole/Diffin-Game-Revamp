using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;// Required when using Event data.

public class DiffButton : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    public GameObject diffinCar;
    private Car diffinCarScript;
    // Start is called before the first frame update
    void Start()
    {
        diffinCarScript = diffinCar.GetComponent<Car>();
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        Debug.Log(this.gameObject.name + " Was Clicked.");
        diffinCarScript.getDiffin();
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        Debug.Log(this.gameObject.name + " Was Upped.");  
        diffinCarScript.haltDiffin();
    }

    public void stopDiffin()
    {
        diffinCarScript.haltDiffin();
    }
}
