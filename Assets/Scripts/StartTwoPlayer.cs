using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartTwoPlayer : MonoBehaviour
{
    // Start is called before the first frame update
    public void StartGame()
    {
        SceneManager.LoadScene(1);
    }
    public void MainMenu()
    {
        SceneManager.LoadScene(0);
    }
    public void RandomGame()
    {
        SceneManager.LoadScene(2);
    }
    public void GreedyGame()
    {
        SceneManager.LoadScene(3);
    }
}
