using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using VRTK;

public class leftControllerAction : MonoBehaviour
{
    //user Object
    [SerializeField]
    private GameObject ovr_cameraRig;

    [SerializeField]
    private GameObject rightController;

    [SerializeField]
    private GameObject leftController;

    [SerializeField]
    private GameObject rightTouchPad;

    [SerializeField]
    private GameObject leftTouchPad;

    [SerializeField]
    private GameObject[] planetList;

    [SerializeField]
    private GameObject clockPanel;

    [SerializeField]
    private GameObject remote; //Remote GameObject


    //radial Menu Panels
    [SerializeField]
    private GameObject spaceRadialMenu;

    [SerializeField]
    private GameObject spaceShipRadialMenu;

    [SerializeField]
    private GameObject multiplePanel;

    public static bool inSpaceship = false;

    private GameObject slider;

    public void Start()
    {
        slider = multiplePanel.transform.GetChild(2).gameObject;
    }
    public void buttonOnePress() //left controller x
    {
        //in to spaceship
        if (rightController.GetComponent<VRTK_Pointer>().enableTeleport)  //spaceship 내부에서 눌렀을 때 조건문 안에 안들어가도록 하기 위해.
        {
            ovr_cameraRig.transform.localPosition = new Vector3(-0.17f, -0.295f, 0.3f);
            inSpaceship = true;
            VRTK_BasePointerRenderer rrenderer = rightController.transform.GetChild(0).GetComponent<VRTK_StraightPointerRenderer>();
            rightController.GetComponent<VRTK_Pointer>().pointerRenderer = rrenderer;
            rightController.GetComponent<VRTK_Pointer>().enableTeleport = false;
            VRTK_BasePointerRenderer lrenderer = leftController.transform.GetChild(0).GetComponent<VRTK_StraightPointerRenderer>();
            leftController.GetComponent<VRTK_Pointer>().pointerRenderer = lrenderer;
            leftController.GetComponent<VRTK_Pointer>().enableTeleport = false;

            //change the multiple to 1
            Multiple.multipleTime = 1.0f;
            slider.transform.GetComponent<Slider>().value = 1;

            spaceRadialMenu.SetActive(false);
            spaceShipRadialMenu.SetActive(false);
            multiplePanel.SetActive(false);

            Debug.Log("in");

        }

        //out
        else
        {
            ovr_cameraRig.transform.localPosition = new Vector3(-0.47f,0.116f,3.24f);
            inSpaceship = false;
            VRTK_BasePointerRenderer rrenderer = rightController.transform.GetChild(1).GetComponent<VRTK_BezierPointerRenderer>();
            rightController.GetComponent<VRTK_Pointer>().pointerRenderer = rrenderer;
            rightController.GetComponent<VRTK_Pointer>().enableTeleport = true;
            VRTK_BasePointerRenderer lrenderer = leftController.transform.GetChild(1).GetComponent<VRTK_BezierPointerRenderer>();
            leftController.GetComponent<VRTK_Pointer>().pointerRenderer = lrenderer;
            leftController.GetComponent<VRTK_Pointer>().enableTeleport = true;

            spaceRadialMenu.SetActive(false);
            spaceShipRadialMenu.SetActive(false);
            multiplePanel.SetActive(false);

        }
    }

    public void buttonTwoPress() //left controller y
    {
        if (clockPanel.activeSelf)
        {
            clockPanel.SetActive(false);
        }
        else
        {
            clockPanel.SetActive(true);
        }
    }

}
