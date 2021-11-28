using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class onPlanetRightController : MonoBehaviour
{
    [SerializeField]
    private GameObject planetPanel;

    [SerializeField]
    private GameObject planetRadialMenu;

    private void Start()
    {
        planetPanel.SetActive(false);
        planetRadialMenu.SetActive(false);
    }

    public void planetPanelClick()
    {
        if (!planetPanel.activeSelf)  //켜기
        {
            planetPanel.SetActive(true);
        }
        else
        {
            planetPanel.SetActive(false);
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
            planetPanel.SetActive(false);

        }
        else  //끄기
        {
            planetRadialMenu.SetActive(false);
            planetPanel.SetActive(false);

        }
    }
}
