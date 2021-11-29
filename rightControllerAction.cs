using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VRTK;

public class rightControllerAction : MonoBehaviour
{
    [SerializeField]
    private GameObject joyStick;

    [SerializeField]
    GameObject spaceShip;

    [SerializeField]
    private GameObject spaceRadialMenu;

    [SerializeField]
    private GameObject spaceShipRadialMenu;

    [SerializeField]
    private GameObject multiplePanel;

    [SerializeField]
    private GameObject leftController;

    public float spaceshipSpeed;

    public static int ok = -1;

    // Start is called before the first frame update

    public void Start()
    {
        spaceRadialMenu.SetActive(false);
        spaceShipRadialMenu.SetActive(false);
        multiplePanel.SetActive(false);
    }

    public void touchPadClick()
    {
        if (!leftControllerAction.inSpaceship)   //우주에서
        {
            if (!spaceRadialMenu.activeSelf) //켜기
            {
                spaceRadialMenu.SetActive(true);
            }
            else  //끄기
            {
                spaceRadialMenu.SetActive(false);
            }
        }
        else
        {
            if (!spaceShipRadialMenu.activeSelf)
            {
                spaceShipRadialMenu.SetActive(true);
                multiplePanel.SetActive(false);
            }
            else
            {
                spaceShipRadialMenu.SetActive(false);
                multiplePanel.SetActive(false);
            }
        }
    }

    public void gameOff()
    {
        UnityEditor.EditorApplication.isPlaying = false;
        Application.Quit();
    }

    public void multiplePanelClick()
    {
        if (!multiplePanel.activeSelf)  //켜기
        {
            multiplePanel.SetActive(true);
            spaceRadialMenu.SetActive(false);
            VRTK_BasePointerRenderer straightRenderer = leftController.transform.GetChild(0).GetComponent<VRTK_StraightPointerRenderer>();
            leftController.GetComponent<VRTK_Pointer>().pointerRenderer = straightRenderer;
            leftController.GetComponent<VRTK_Pointer>().enableTeleport = false;
        }
        else
        {
            multiplePanel.SetActive(false);
            leftToBezierRenderer();
        }
    }

    public void okAction() //A버튼 클릭시 
    {
        ok = 1;
    }

    public void cancelAction() //B버튼 클릭시 
    {
        ok = 0;
    }

    private void leftToBezierRenderer()
    {
        VRTK_BasePointerRenderer bezierRenderer = leftController.transform.GetChild(1).GetComponent<VRTK_BezierPointerRenderer>();
        leftController.GetComponent<VRTK_Pointer>().pointerRenderer = bezierRenderer;
        leftController.GetComponent<VRTK_Pointer>().enableTeleport = true;
    }

}
