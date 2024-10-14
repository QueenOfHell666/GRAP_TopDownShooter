using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneControlManager : MonoBehaviour
{
    public void PlayGame()
    {
        // Cambiar "GameScene" por el nombre de tu escena de juego
        SceneManager.LoadSceneAsync("TopDownShooter");
    }

    public void PlayMenu()
    {
        // Cambiar "GameScene" por el nombre de tu escena de juego
        SceneManager.LoadSceneAsync("Menu");
    }
}
