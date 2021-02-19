using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SceneLoader : MonoBehaviour
{
    public GameObject activateMain;//Going back to mainmenu
    public GameObject disable;//Disable the login/register menu
   

    //Load Avatar
    public void LoadGameAvatar(){
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
    //Load Skeletal Tracking
    public void LoadGameSkeleton()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 2);
    }
    //Load Collider
    public void LoadGameCollider()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 3);
    }
    //Load Main Menu
    public void LoadMainMenu()
    {
        SceneManager.LoadScene(0);
        activateMain.SetActive(true);
        disable.SetActive(false);
    }
    //Quit Application
    public void QuitGame(){
        Application.Quit();
        Debug.Log("Quit!");
    }
}
