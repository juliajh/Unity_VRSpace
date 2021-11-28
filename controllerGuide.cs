using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class controllerGuide : MonoBehaviour
{
    [SerializeField]
    private GameObject leftControllerModel;

    [SerializeField]
    private GameObject rightControllerModel;

    private GameObject Xbutton;
    private GameObject Ybutton;
    private GameObject Abutton;
    private GameObject Bbutton;
    private GameObject LTouchPadbutton;
    private GameObject RTouchPadbutton;
    private GameObject LTriggerbutton;
    private GameObject RTriggerbutton;
    private GameObject LGripButton;
    private GameObject RGripButton;

    //private List<GameObject> controllers = new List<GameObject>();
    //private bool Lflag = false;
    //private bool Rflag = false;

    //Light On Buttons
    public void XbuttonLight()
    {
        while (Xbutton == null)
            SettingXButton();

        if (Xbutton != null)
        {
            if (Xbutton.GetComponent<Outline>() == null)
            {
                Xbutton.AddComponent<Outline>();
            }
        }
    }
    public void YbuttonLight()
    {
        while (Ybutton == null)
            SettingYButton();

        if (Ybutton != null)
        {
            if (Ybutton.GetComponent<Outline>() == null)
            {
                Ybutton.AddComponent<Outline>();
            }
        }
    }
    public void AbuttonLight()
    {
        while (Abutton == null)
            SettingAButton();

        if (Abutton != null)
        {
            if (Abutton.GetComponent<Outline>() == null)
            {
                Abutton.AddComponent<Outline>();
            }
        }
    }

    public void BbuttonLight()
    {
        while (Bbutton == null)
            SettingBButton();

        if (Bbutton != null)
        {
            if (Bbutton.GetComponent<Outline>() == null)
            {
                Bbutton.AddComponent<Outline>();
            }
        }
    }
    public void LTouchPadbuttonLight()
    {
        while (LTouchPadbutton == null)
            SettingLTouchPadButton();

        if (LTouchPadbutton != null)
        {
            if (LTouchPadbutton.GetComponent<Outline>() == null)
            {
                LTouchPadbutton.AddComponent<Outline>();
            }
        }
    }
    public void RTouchPadbuttonLight()
    {
        while (RTouchPadbutton == null)
            SettingRTouchPadButton();

        if (RTouchPadbutton != null)
        {
            if (RTouchPadbutton.GetComponent<Outline>() == null)
            {
                RTouchPadbutton.AddComponent<Outline>();
            }
        }
    }

    public void LTriggerbuttonLight()
    {
        while (LTriggerbutton == null)
            SettingLTriggerButton();

        if (LTriggerbutton != null)
        {
            if (LTriggerbutton.GetComponent<Outline>() == null)
            {
                LTriggerbutton.AddComponent<Outline>();
            }
        }
    }

    public void RTriggerbuttonLight()
    {
        while (RTriggerbutton == null)
            SettingRTriggerButton();

        if (RTriggerbutton != null)
        {
            if (RTriggerbutton.GetComponent<Outline>() == null)
            {
                RTriggerbutton.AddComponent<Outline>();
            }
        }
    }

    public void LGripbuttonLight()
    {
        while (LGripButton == null)
            SettingLGripButton();

        if (LGripButton != null)
        {
            if (LGripButton.GetComponent<Outline>() == null)
            {
                LGripButton.AddComponent<Outline>();
            }
        }
    }

    public void RGripbuttonLight()
    {
        while (RGripButton == null)
            SettingRGripButton();

        if (RGripButton != null)
        {
            if (RGripButton.GetComponent<Outline>() == null)
            {
                RGripButton.AddComponent<Outline>();
            }
        }
    }

    //Light Off Buttons
    public void offXbutton()
    {
        try
        {
            Destroy(Xbutton.GetComponent<Outline>());
        }
        catch(Exception e)
        {
            Debug.Log(e.Message);
        }
    }

    public void offYbutton()
    {
        try
        {
            Destroy(Ybutton.GetComponent<Outline>());
        }
        catch (Exception e)
        {
            Debug.Log(e.Message);
        }
    }

    public void offAbutton()
    {
        try
        {
            Destroy(Abutton.GetComponent<Outline>());
        }
        catch (Exception e)
        {
            Debug.Log(e.Message);
        }
    }
    public void offBbutton()
    {
        try
        {
            Destroy(Bbutton.GetComponent<Outline>());
        }
        catch (Exception e)
        {
            Debug.Log(e.Message);
        }
    }
    public void offLTouchPadbutton()
    {
        try
        {
            Destroy(LTouchPadbutton.GetComponent<Outline>());
        }
        catch (Exception e)
        {
            Debug.Log(e.Message);
        }
    }
    public void offRTouchPadbutton()
    {
        try
        {
            Destroy(RTouchPadbutton.GetComponent<Outline>());
        }
        catch (Exception e)
        {
            Debug.Log(e.Message);
        }
    }
    public void offLTriggerbutton()
    {
        try
        {
            Destroy(LTriggerbutton.GetComponent<Outline>());
        }
        catch (Exception e)
        {
            Debug.Log(e.Message);
        }
    }
    public void offRTriggerbutton()
    {
        try
        {
            Destroy(RTriggerbutton.GetComponent<Outline>());
        }
        catch (Exception e)
        {
            Debug.Log(e.Message);
        }
    }

    public void offLGripbutton()
    {
        try
        {
            Destroy(LGripButton.GetComponent<Outline>());
        }
        catch (Exception e)
        {
            Debug.Log(e.Message);
        }
    }

    public void offRGripbutton()
    {
        try
        {
            Destroy(RGripButton.GetComponent<Outline>());
        }
        catch (Exception e)
        {
            Debug.Log(e.Message);
        }
    }

    //Find button from models - Settings
    private void SettingXButton()
    {
        if (Xbutton == null)
        {
            if (leftControllerModel.transform.childCount > 0 && leftControllerModel.transform.parent.gameObject.activeSelf)
            {
                Xbutton = leftControllerModel.transform.GetChild(8).gameObject;
            }
        }
    }
    private void SettingYButton()
    {
        if (Ybutton == null)
        {
            if (leftControllerModel.transform.childCount > 0 && leftControllerModel.transform.parent.gameObject.activeSelf)
            {
                Ybutton = leftControllerModel.transform.GetChild(9).gameObject;
            }
        }
    }
    private void SettingAButton()
    {
        if (Abutton == null)
        {
            if (rightControllerModel.transform.childCount > 0 && rightControllerModel.transform.parent.gameObject.activeSelf)
            {
                Abutton = rightControllerModel.transform.GetChild(0).gameObject;
            }
        }
    }
    private void SettingBButton()
    {
        if (Bbutton == null)
        {
            if (rightControllerModel.transform.childCount > 0 && rightControllerModel.transform.parent.gameObject.activeSelf)
            {
                Bbutton = rightControllerModel.transform.GetChild(1).gameObject;
            }
        }
    }

    private void SettingLTouchPadButton()
    {
        if (LTouchPadbutton == null)
        {
            if (leftControllerModel.transform.childCount > 0 && leftControllerModel.transform.parent.gameObject.activeSelf)
            {
                LTouchPadbutton = leftControllerModel.transform.GetChild(7).gameObject;
            }
        }
    }
    private void SettingRTouchPadButton()
    {
        if (RTouchPadbutton == null)
        {
            if (rightControllerModel.transform.childCount > 0 && rightControllerModel.transform.parent.gameObject.activeSelf)
            {
                RTouchPadbutton = rightControllerModel.transform.GetChild(9).gameObject;
            }
        }
    }
    private void SettingLTriggerButton()
    {
        if (LTriggerbutton == null)
        {
            if (leftControllerModel.transform.childCount > 0 && leftControllerModel.transform.parent.gameObject.activeSelf)
            {
                LTriggerbutton = leftControllerModel.transform.GetChild(1).gameObject;
            }
        }
    }
    private void SettingRTriggerButton()
    {
        if (RTriggerbutton == null)
        {
            if (rightControllerModel.transform.childCount > 0 && rightControllerModel.transform.parent.gameObject.activeSelf)
            {
                RTriggerbutton = rightControllerModel.transform.GetChild(3).gameObject;
            }
        }
    }
    private void SettingLGripButton()
    {
        if (LGripButton == null)
        {
            if (leftControllerModel.transform.childCount > 0 && leftControllerModel.transform.parent.gameObject.activeSelf)
            {
                LGripButton = leftControllerModel.transform.GetChild(5).gameObject;
            }
        }
    }
    private void SettingRGripButton()
    {
        if (RGripButton == null)
        {
            if (rightControllerModel.transform.childCount > 0 && rightControllerModel.transform.parent.gameObject.activeSelf)
            {
                RGripButton = rightControllerModel.transform.GetChild(7).gameObject;
            }
        }
    }

}
