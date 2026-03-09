using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;
using System.Collections.Generic;

 //link a tracked image to specific prefabs 
[System.Serializable]
public class CardWorld
{
    public string imageName; //name of image in the reference image library
    public GameObject worldObject; //name of game object that spawns on a specific image 
}

public class MultiCardController : MonoBehaviour
{
    //list of all 'worlds' (prefab containers/game objects) and tracked images - to be configured in the inspector 
    public List<CardWorld> cardWorlds;

    private ARTrackedImage trackedImage; //Reference to the tracked image
    private GameObject activeWorld; //The current active world for this image

    void Awake()
    {
        //Get the AR tracked image component attached to this game object 
        trackedImage = GetComponent<ARTrackedImage>();
    }

    void Start()
    {
        //Determine which world object should be active when image is detected 
        UpdateContent();
    }

    void Update()
    {
        //if no world object has been assigned, do nothing 
        if (activeWorld == null) return;

        //if an image is currently being tracked 
        if (trackedImage.trackingState == TrackingState.Tracking)
        {
            //ensure the world object is visable 
            if (!activeWorld.activeSelf)
                activeWorld.SetActive(true);
        }
        else
        {
            //if tracked image is lost, hide the world object 
            if (activeWorld.activeSelf)
                activeWorld.SetActive(false);
        }
    }

    void UpdateContent()
    {
        //get the name of the detected image 
        string imageName = trackedImage.referenceImage.name;

        //go through all configured card/world pairs (inspector)
        foreach (var card in cardWorlds)
        {
            //hide all game objects by default 
            card.worldObject.SetActive(false);

            //if the image name matches image name assigned in the inspector 
            if (card.imageName == imageName)
            {
                //Assign the corrisponding 'world' game object 
                activeWorld = card.worldObject;
            }
        }

        //If a corresponding world object was found, activate it 
        if (activeWorld != null)
            activeWorld.SetActive(true);
    }
}
