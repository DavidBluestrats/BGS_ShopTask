using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_Application : MonoBehaviour
{
    [Header("Quit application button")]
    public Button quitButton;

    void Start()
    {
        quitButton.onClick.AddListener(OnQuitButtonClick);
    }

    private void OnQuitButtonClick()
    {
        Application.Quit();
    }

    
}
