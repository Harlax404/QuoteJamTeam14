using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Menu : MonoBehaviour
{
    [SerializeField]
    private string MainScene;

    [SerializeField]
    private GameObject mainPanel;

    [SerializeField]
    private GameObject creditPanel;

    private bool mainpanelActiveSelf = true;
    private bool creditpanelActiveSelf = false;

    void Start()
    {
        if (!mainPanel.activeSelf)
        {
            mainPanel.SetActive(true);
        }

        if (creditPanel.activeSelf)
        {
            creditPanel.SetActive(false);
        }
    }

    public void LaunchGame()
    {
        SceneManager.LoadScene(MainScene);
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    public void DisplayCredit()
    {
        mainpanelActiveSelf = !mainpanelActiveSelf;
        creditpanelActiveSelf = !creditpanelActiveSelf;

        mainPanel.SetActive(mainpanelActiveSelf);
        creditPanel.SetActive(creditpanelActiveSelf);
    }

}
