using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;

//This script controls visual and sound effects for tracked AR image 
public class TrackedImageFX : MonoBehaviour
{
    [Header("Visual Content")]
    [SerializeField] GameObject visuals;
    //parent object to tracked image prefabs that show when the AR image is detected 

    [Header("Particle Effects")]
    [SerializeField] GameObject spawnEffectPrefab;
    [SerializeField] GameObject despawnEffectPrefab;
    //Sprite sheets aniamtions for spawm/despawn effect when the AR image is detected/removed

    [Header("Sound Effects")]
    [SerializeField] AudioClip spawnSFX;
    [SerializeField] AudioClip despawnSFX;
    //audio sound effect for when spawn/despawn effect plays 

    ARTrackedImage trackedImage; //reference to the TrackedImageContent parent object this script is attached to
    AudioSource audioSource; //sound effect player 

    bool wasTracking = false; //tracks whether the image was previously visable

    void Awake()
    {
        // Get the ARTrackedImage component from the parent object that this prefab is attached to
        trackedImage = GetComponentInParent<ARTrackedImage>();

        // Add AudioSource for sound effects 
        audioSource = gameObject.AddComponent<AudioSource>();
        audioSource.playOnAwake = false;

        // Ensure visuals start hidden
        if (visuals != null)
            visuals.SetActive(false);
    }

    void Update()
    {
        if (trackedImage == null)
            return;

        // Check if the AR camera currently detects image 
        bool isTracking =
            trackedImage.trackingState == TrackingState.Tracking;

        // When the image is detected
        if (isTracking && !wasTracking)
        {
            PlaySpawn();
        }

        // When image is lost 
        if (!isTracking && wasTracking)
        {
            PlayDespawn();
        }

        // Update tracking state 
        wasTracking = isTracking;
    }

    // Play spawn effects (render visuals, sound effect and animation)
    void PlaySpawn()
    {
        if (visuals != null)
            visuals.SetActive(true);

        if (spawnEffectPrefab != null)
        {
            // Play the imported sprite animtion prefab at the tracked image position
            Instantiate(spawnEffectPrefab,
                        transform.position,
                        transform.rotation);
        }

        if (spawnSFX != null)
        {
            //play audio sound effect 
            audioSource.PlayOneShot(spawnSFX);
        }
    }

    // Play despawn effects (hide visuals, play sound effect and animation)
    void PlayDespawn()
    {
        if (despawnEffectPrefab != null)
        {
            // Play the sprite sheet animation prefab at the tracked image position 
            Instantiate(despawnEffectPrefab,
                        transform.position,
                        transform.rotation);
        }

        if (despawnSFX != null)
        {
            audioSource.PlayOneShot(despawnSFX);
        }

        if (visuals != null)
            visuals.SetActive(false);
    }
}