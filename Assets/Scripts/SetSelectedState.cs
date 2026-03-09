using UnityEngine;

public class SetSelectedState : MonoBehaviour
{
    //public because we want to set it from the inspector 
    public Material SelectedMaterial;
    //private variable to store the origional material 
    private Material originalMaterial;

    //Reference to the mesh renderer component attached to this object
    //This allows the object to return to its default appearence when unselected 
    private MeshRenderer meshRenderer;

    //Boolean value used to track whether the object is currently selected 
    //defaults to false becuase the object starts in an unselected state
    private bool isSelected = false;

    //'Awake' is called when the script is being loaded
    //Used here insead of 'start' so that object state is set before any interaction occurs
    void Awake()
    {
       //find the mesh renderer component attached to this object
        meshRenderer = GetComponent<MeshRenderer>();

        //if a mesh renderer component still is not found, return
        if (meshRenderer == null)
        return;

        // Store the original material to be restored later
        originalMaterial = meshRenderer.material;

    }

    // This script is called from XR simple interactable - select entered event on the prefab
    // It toggles the prefab between selected and unselected states
    public void ToggleSelected()
    {
        //check that required mesh renderer references exist before attempting to change material
        if (meshRenderer == null || SelectedMaterial == null)
        return;

        // Reverse the current selected state. i.e. if true, it becomes false and vice versa 
        isSelected = !isSelected;

        // Apply correct material depending on selected state 
        //If isSelected is true, apply the selected material, if false revert to origonal material
        meshRenderer.material = isSelected ? SelectedMaterial : originalMaterial;
        
    }

}
