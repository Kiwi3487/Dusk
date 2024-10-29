using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
//This Script is used to call Scene using using UnityEngine.SceneManagement I mainly used it for transiting between scenes using UI buttons
    public void LoadWin()
    {
        SceneManager.LoadScene("Win");
    }
    public void LoadMenu()
    {
        SceneManager.LoadScene("Title");
    }
    public void LoadGamePlay()
    {
        SceneManager.LoadScene("LevelScene");
    }

}