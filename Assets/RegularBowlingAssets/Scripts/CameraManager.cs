using UnityEngine;

public class CameraManager : MonoBehaviour
{
    public Camera[] cameras; // Array to hold different camera angles
    private int currentCameraIndex = 0; // Index to keep track of the current camera

    void Start()
    {
        // Make sure only the first camera is enabled at the start
        if (cameras.Length > 0)
        {
            SetActiveCamera(currentCameraIndex);
        }
    }

    public void SwitchCamera()
    {
        // Disable the current camera
        cameras[currentCameraIndex].gameObject.SetActive(false);

        // Update the index to the next camera
        currentCameraIndex = (currentCameraIndex + 1) % cameras.Length;

        // Enable the new camera
        SetActiveCamera(currentCameraIndex);
    }

    public void SetActiveCamera(int index)
    {
        if (index >= 0 && index < cameras.Length)
        {
            cameras[index].gameObject.SetActive(true);
            currentCameraIndex = index;
        }
    }
}
