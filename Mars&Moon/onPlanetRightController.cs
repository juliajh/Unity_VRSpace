using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class onPlanetRightController : MonoBehaviour
{
    [SerializeField]
    private GameObject planetHelpPanel;

    [SerializeField]
    private GameObject planetRadialMenu;

    private void Start()
    {
        planetHelpPanel.SetActive(false);
        planetRadialMenu.SetActive(false);
    }

    public void planetHelpBtnClick()
    {
        if (!planetHelpPanel.activeSelf)  //켜기
        {
            planetHelpPanel.SetActive(true);
        }
        else
        {
            planetHelpPanel.SetActive(false);
        }
    }

    public void gameOff()
    {
        UnityEditor.EditorApplication.isPlaying = false;
        Application.Quit();
    }

    public void touchPadClick()
    {
        if (!planetRadialMenu.activeSelf) //켜기
        {
            planetRadialMenu.SetActive(true);
            planetHelpPanel.SetActive(false);

        }
        else  //끄기
        {
            planetRadialMenu.SetActive(false);
            planetHelpPanel.SetActive(false);

        }
    }
}
