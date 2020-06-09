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
    public BoxCollider boxCollider;

    [FMODUnity.EventRef]
    public string ladsJeer = "";
    FMOD.Studio.EventInstance ladsJeerEvent;

    [FMODUnity.EventRef]
    public string hendyInsult = "";
    FMOD.Studio.EventInstance hendyInsultEvent;

    // Start is called before the first frame update
    void Start()
    {
        ladsJeerEvent = FMODUnity.RuntimeManager.CreateInstance(ladsJeer);
        hendyInsultEvent = FMODUnity.RuntimeManager.CreateInstance(hendyInsult);
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
            boxCollider.enabled = false;
            ladsJeerEvent.start();
            hendyInsultEvent.start();
            hitConeEndGame();
            Debug.Log("COLLIDED!!");
        }
    }

    public void hitConeEndGame() {
        levelLoader.StartHitConeLoadingScreen();
        soundtrack.steadyOnHey();
        displayDiffCount.updateDiffCountDisplay();
    }

    public void Reset()
    {
        boxCollider.enabled = true;
        transform.position = originalPosition;
        transform.rotation = originalRotation;
        rigidbody.isKinematic = true;
        rigidbody.isKinematic = false;
    }
}
