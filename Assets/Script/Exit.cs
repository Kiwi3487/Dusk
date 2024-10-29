using UnityEngine;
using UnityEngine.SceneManagement;

public class DoorSceneChange : MonoBehaviour
{
    //Script on the a gameobject that is called when player touches it
    public string sceneName;
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Exit"))
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
            SceneManager.LoadScene(sceneName);
        }
    }
}