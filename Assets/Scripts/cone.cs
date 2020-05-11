using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cone : MonoBehaviour
{
    public Rigidbody rigidbody;
    private Vector3 originalPosition;
    private Quaternion originalRotation;
    public GameObject gameCamera;
    public GameObject pointsScreenCamera;
    public GameObject inGameUi;
    public Soundtrack soundtrack;
    public PointsScreenCounter pointsScreenCounter;
    public DisplayDiffCount displayDiffCount;
    public LevelLoader levelLoader;

    [FMODUnity.EventRef]
    public string ladsJeer = "";
    FMOD.Studio.EventInstance ladsJeerEvent;


    // Start is called before the first frame update
    void Start()
    {
        ladsJeerEvent = FMODUnity.RuntimeManager.CreateInstance(ladsJeer);
        originalPosition = transform.position;
        originalRotation = transform.rotation;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //Detect collisions between the GameObjects with Colliders attached
    void OnCollisionEnter(Collision collision)
    {
        //Check for a match with the specified name on any GameObject that collides with your GameObject
        if (collision.gameObject.name == "CarPhysics")
        {
            ladsJeerEvent.start();
            hitConeEndGame();
            Debug.Log("COLLIDED!!");
        }
    }

    public void hitConeEndGame() {
        levelLoader.StartHitConeLoadingScreen();
        soundtrack.steadyOnHey();
        pointsScreenCounter.startCount();
        displayDiffCount.updateDiffCountDisplay();
    }

    public void Reset()
    {
        transform.position = originalPosition;
        transform.rotation = originalRotation;
        rigidbody.isKinematic = true;
        rigidbody.isKinematic = false;
    }
}
