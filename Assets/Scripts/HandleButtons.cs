using UnityEngine;
using UnityEngine.SceneManagement;

public class HandleButtons : MonoBehaviour {
	
    public void LeaveGame()
    {
        Application.Quit();
    }

    public void MainMenu()
    {
        SceneManager.LoadScene("Zappy");
    }
}
