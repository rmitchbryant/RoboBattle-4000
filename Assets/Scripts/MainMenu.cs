using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

// This script controls the main menu allowing the player to start the game
// They player can also quit the game

public class MainMenu : MonoBehaviour
{
    
    public void PlayGame()
    {
        SceneManager.LoadScene(1);
    }

    public void QuitGame()
    {
        Debug.Log("Quitter...");
        Application.Quit();
    }

    public void PlaySound(string name)
    {
        FindObjectOfType<AudioManager>().Play(name);

    }

}
