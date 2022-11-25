using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
public class MainMenu : MonoBehaviour
{
    [SerializeField]
    TextMeshProUGUI textMeshProUGUI;
    // Start is called before the first frame updat

    public void LsystemScene()
    {
        SceneManager.LoadScene(1);
    }

    public void FractalsScene()
    {
        SceneManager.LoadScene(2);
    }
    public void StochasticLsystemScene()
    {
        SceneManager.LoadScene(3);
    }
    public void MainMenuScene()
    {
        SceneManager.LoadScene(0);
    }
    public void Quit()
    {
        Application.Quit();
    }
}
