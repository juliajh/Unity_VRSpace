using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using VRTK;
using UnityEngine.UI;

public class onPlanetLeftController : MonoBehaviour
{
    [SerializeField]
    private GameObject vrtk_sdk;

    [SerializeField]
    private GameObject ovr_camera;

    [SerializeField]
    private GameObject spaceship;

    [SerializeField]
    private GameObject rightController;

    [SerializeField]
    private GameObject alarmPanel;

    [SerializeField]
    private GameObject inParticle;

    private VRTK_StraightPointerRenderer straightRenderer;
    private float speedofTyping;
    private string sceneName;
    private bool openingFinish;
    private bool tutorialOfGrip;

    public void Start()
    {
        openingFinish = false;
        tutorialOfGrip = false;
        sceneName = SceneManager.GetActiveScene().name;
        switch (sceneName)
        {
            case "MarsScene":
                Physics.gravity = new Vector3(0, -3.721f, 0);
                Physics.clothGravity = new Vector3(0, -3.721f, 0);
                break;
            case "MoonScene":
                Physics.gravity = new Vector3(0, -1.62f, 0);
                Physics.clothGravity = new Vector3(0, -1.62f, 0);
                break;
        }
        straightRenderer = rightController.transform.GetChild(0).GetComponent<VRTK_StraightPointerRenderer>();
        speedofTyping = 0.11f;
        this.gameObject.GetComponent<controllerGuide>().XbuttonLight();
        alarmPanel.GetComponent<Animator>().SetBool("alarming", true);
        StartCoroutine(Typing(alarmPanel.transform.GetChild(0).GetComponent<Text>(), "왼쪽 controller의 x버튼을 누르면 우주선 안, 밖으로 이동할 수 있습니다.", speedofTyping));
    }

    // Start is called before the first frame update
    public void buttonOnePress() //left controller x
    {
        if(openingFinish)
        {
            //going in to spaceship
            if (rightController.GetComponent<VRTK_Pointer>().enableTeleport)  //spaceship 내부에서 눌렀을 때 조건문 안에 안들어가도록 하기 위해.
            {
                if (sceneName == "MarsScene")
                    vrtk_sdk.transform.position = new Vector3(584.45f, -167.213f, 1155.33f);
                else if (sceneName == "MoonScene")
                    vrtk_sdk.transform.position = new Vector3(135.038f, 82.57f, 811.604f);
                ovr_camera.transform.localPosition = new Vector3(0, 0, 0);
                VRTK_BasePointerRenderer renderer = rightController.transform.GetChild(0).GetComponent<VRTK_StraightPointerRenderer>();
                rightController.GetComponent<VRTK_Pointer>().pointerRenderer = renderer;
                rightController.GetComponent<VRTK_Pointer>().enableTeleport = false;
            }

            //going out
            else
            {
                if (sceneName == "MarsScene")
                    vrtk_sdk.transform.position = new Vector3(586.95f, -167.213f, 1149.59f);
                else if (sceneName == "MoonScene")
                    vrtk_sdk.transform.position = new Vector3(150.64f, 81.2f, 795.87f);
                ovr_camera.transform.localPosition = new Vector3(0, 0, 0);
                VRTK_BasePointerRenderer renderer = rightController.transform.GetChild(1).GetComponent<VRTK_BezierPointerRenderer>();
                rightController.GetComponent<VRTK_Pointer>().pointerRenderer = renderer;
                rightController.GetComponent<VRTK_Pointer>().enableTeleport = true;
                if (!tutorialOfGrip)
                {
                    this.gameObject.GetComponent<controllerGuide>().RGripbuttonLight();
                    alarmPanel.GetComponent<Animator>().SetBool("alarming", true);
                    if (sceneName == "MarsScene")
                    {
                        StartCoroutine(Typing(alarmPanel.transform.GetChild(0).GetComponent<Text>(), "오른쪽 controller의 grip버튼을 눌러서 물건들을 잡아보세요.\n화성은 중력이 3.721m/s²입니다.", speedofTyping));  
                    }
                    else if (sceneName == "MoonScene")
                    {
                        StartCoroutine(Typing(alarmPanel.transform.GetChild(0).GetComponent<Text>(), "오른쪽 controller의 grip버튼을 눌러서 물건들을 잡아보세요.\n달은 중력이 1.62m/s²입니다. ", speedofTyping));
                    }
                }
            }
        }
    }

    IEnumerator Typing(Text typingText, string message, float speed)
    {
        for (int i = 0; i < message.Length; i++)
        {
            typingText.text = message.Substring(0, i + 1);
            yield return new WaitForSeconds(speed);
            if (i == message.Length - 1)
            {
                alarmPanel.GetComponent<Animator>().SetBool("alarming", false);
                if (rightController.GetComponent<VRTK_Pointer>().enableTeleport)
                {
                    this.gameObject.GetComponent<controllerGuide>().offRGripbutton();
                    tutorialOfGrip = true;
                }
                else
                {
                    this.gameObject.GetComponent<controllerGuide>().offXbutton();
                    openingFinish = true;
                }
            }
        }
    }

    public void Update()
    {
        if(straightRenderer.getrayHit())
        {
            if(straightRenderer.getObj().name=="toSpacebtn")
            {
                StartCoroutine(scenechange());
            }
        }
    }

    IEnumerator scenechange()
    {
        inParticle.SetActive(true);
        yield return new WaitForSeconds(1.7f);
        SceneManager.LoadScene("SolarSystem");
        
    }

}
