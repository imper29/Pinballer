using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuStart : MonoBehaviour
{
    public void Play()
    {
        SceneManager.LoadScene("Level");
    }
    public void Quit()
    {
        Application.Quit();
    }
}
