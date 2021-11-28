using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using VRTK;

public class tutorialOnMain : MonoBehaviour
{
    [SerializeField]
    private GameObject alarmPanel;

    [SerializeField]
    private List<string> alarmString;

    [SerializeField]
    private GameObject rightController;

    [SerializeField]
    private GameObject leftController;

    [SerializeField]
    private GameObject tourRController;

    [SerializeField]
    private GameObject spaceship;

    [SerializeField]
    private GameObject ovr_cameraRig;

    [SerializeField]
    private GameObject vrtk_sdk;

    [SerializeField]
    private GameObject inParticle;

    [SerializeField]
    private GameObject railroad;

    private int indexOfTutorial;
    private float speedofTyping;
    private bool typing;
    private bool rTriggerbuttonpressed;
    private bool yButtonpressed;
    private bool xButtonpressed;
    private bool touring;
    public static bool tutorialOver;
    private static Vector3 prevPosOfspaceship;

    // Start is called before the first frame update
    void Start()
    {
        indexOfTutorial = 0;
        speedofTyping = 0.01f;
        typing = false;
        rTriggerbuttonpressed = false;
        yButtonpressed = false;
        xButtonpressed = false;
        touring = false;

        if (tutorialOver)
        {
            spaceship.transform.position = prevPosOfspaceship;
            ovr_cameraRig.transform.localPosition = new Vector3(-0.47f, -0.116f, 0.3f);
            VRTK_BasePointerRenderer straightRenderer = rightController.transform.GetChild(0).GetComponent<VRTK_StraightPointerRenderer>();
            rightController.GetComponent<VRTK_Pointer>().pointerRenderer = straightRenderer;
            rightController.GetComponent<VRTK_Pointer>().enableTeleport = false;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (!tutorialOver)
        {
            if (indexOfTutorial < 5)
            {
                if (indexOfTutorial == 0)
                {
                    rightController.GetComponent<VRTK_Pointer>().enabled = false;
                    leftController.GetComponent<VRTK_Pointer>().enabled = false;
                }
                if (!typing)
                    StartCoroutine(Typing(alarmPanel.transform.GetChild(0).GetComponent<Text>(), alarmString[indexOfTutorial], speedofTyping));
                typing = true;
            }
            else if (indexOfTutorial <= alarmString.Count)
            {
                switch (indexOfTutorial)
                {
                    case 5: //right controller 사용자 자유 이동 방법 설명 
                        if (!typing)
                        {
                            rightController.GetComponent<VRTK_Pointer>().enabled = true;
                            leftController.GetComponent<VRTK_Pointer>().enabled = true;
                            this.gameObject.GetComponent<controllerGuide>().RTriggerbuttonLight();
                            StartCoroutine(Typing(alarmPanel.transform.GetChild(0).GetComponent<Text>(), alarmString[indexOfTutorial], speedofTyping));
                            typing = true;
                        }
                        break;
                    case 6: //우주선 안으로 이동하는 방법 설명 
                        if (rTriggerbuttonpressed)
                        {
                            if (!typing)
                            {
                                this.gameObject.GetComponent<controllerGuide>().offRTriggerbutton();
                                rTriggerbuttonpressed = false;
                                leftController.GetComponent<VRTK_ControllerEvents>().enabled = true;
                                this.gameObject.GetComponent<controllerGuide>().XbuttonLight();
                                StartCoroutine(Typing(alarmPanel.transform.GetChild(0).GetComponent<Text>(), alarmString[indexOfTutorial], speedofTyping));
                                typing = true;
                            }
                        }
                        break;
                    case 7: //우주선 조종 설명 
                        if (xButtonpressed)
                        {
                            if (!typing)
                            {
                                this.gameObject.GetComponent<controllerGuide>().offXbutton();
                                xButtonpressed = false;
                                this.gameObject.GetComponent<controllerGuide>().LTouchPadbuttonLight();
                                this.gameObject.GetComponent<controllerGuide>().RTouchPadbuttonLight();
                                StartCoroutine(Typing(alarmPanel.transform.GetChild(0).GetComponent<Text>(), alarmString[indexOfTutorial], speedofTyping));
                                typing = true;
                            }
                        }
                        break;
                    case 8: //y버튼으로 시계 panel on off 설명 
                        if (!typing)
                        {
                            this.gameObject.GetComponent<controllerGuide>().offLTouchPadbutton();
                            this.gameObject.GetComponent<controllerGuide>().offRTouchPadbutton();
                            this.gameObject.GetComponent<controllerGuide>().YbuttonLight();
                            StartCoroutine(Typing(alarmPanel.transform.GetChild(0).GetComponent<Text>(), alarmString[indexOfTutorial], speedofTyping));
                            typing = true;
                        }
                        break;
                    case 9:
                        if (yButtonpressed)
                        {
                            if (!typing)
                            {
                                this.gameObject.GetComponent<controllerGuide>().offYbutton();
                                this.gameObject.GetComponent<controllerGuide>().RTriggerbuttonLight();
                                yButtonpressed = false;
                                StartCoroutine(Typing(alarmPanel.transform.GetChild(0).GetComponent<Text>(), alarmString[indexOfTutorial], speedofTyping));
                                typing = true;
                            }
                        }
                        break;
                    case 10:
                        if (!typing)
                        {
                            StartCoroutine(Typing(alarmPanel.transform.GetChild(0).GetComponent<Text>(), alarmString[indexOfTutorial], speedofTyping));
                            typing = true;
                        }
                        break;
                    case 11:
                        if (planetButtons.planetNum < 0)
                        {
                            alarmPanel.GetComponent<Animator>().SetBool("alarming", false);
                        }
                        else
                        {
                            if (!typing)
                            {
                                alarmPanel.GetComponent<Animator>().SetBool("alarming", true);
                                this.gameObject.GetComponent<controllerGuide>().offRTriggerbutton();
                                rTriggerbuttonpressed = false;
                                StartCoroutine(Typing(alarmPanel.transform.GetChild(0).GetComponent<Text>(), alarmString[indexOfTutorial], speedofTyping));
                                typing = true;
                            }
                        }
                        break;
                    case 12:
                        if (!typing)
                        {
                            StartCoroutine(Typing(alarmPanel.transform.GetChild(0).GetComponent<Text>(), alarmString[indexOfTutorial], speedofTyping));
                            typing = true;
                        }
                        break;
                    default:
                        alarmPanel.GetComponent<Animator>().SetBool("alarming", false);
                        tutorialOver = true;
                        typing = false;
                        indexOfTutorial += 1;
                        break;
                }
            }
        }
        else
        {
            if (planetButtons.planetNum == 3)
            {
                if (!typing && rightControllerAction.ok == -1)
                {
                    alarmPanel.transform.GetChild(1).gameObject.SetActive(true);
                    alarmPanel.GetComponent<Animator>().SetBool("alarming", true);
                    rightControllerAction.ok = -2;
                    StartCoroutine(Typing(alarmPanel.transform.GetChild(0).GetComponent<Text>(), "화성 내부로 들어가시겠습니까?", speedofTyping));
                    typing = true;
                    this.gameObject.GetComponent<controllerGuide>().AbuttonLight();
                    this.gameObject.GetComponent<controllerGuide>().BbuttonLight();
                }
                else if (rightControllerAction.ok == 1)
                {
                    StartCoroutine(fadein());
                }
                else if (rightControllerAction.ok == 0)
                {
                    this.gameObject.GetComponent<controllerGuide>().offAbutton();
                    this.gameObject.GetComponent<controllerGuide>().offBbutton();
                    alarmPanel.GetComponent<Animator>().SetBool("alarming", false);
                    alarmPanel.transform.GetChild(1).gameObject.SetActive(false);
                    rightControllerAction.ok = -1;
                    typing = false;
                    planetButtons.planetNum = -1;
                }
            }

            else if (planetButtons.planetNum == 2)
            {
                if (!typing && rightControllerAction.ok == -1)
                {
                    alarmPanel.transform.GetChild(1).gameObject.SetActive(true);
                    alarmPanel.GetComponent<Animator>().SetBool("alarming", true);
                    rightControllerAction.ok = -2;
                    StartCoroutine(Typing(alarmPanel.transform.GetChild(0).GetComponent<Text>(), "달 내부로 들어가시겠습니까?", speedofTyping));
                    typing = true;
                    this.gameObject.GetComponent<controllerGuide>().AbuttonLight();
                    this.gameObject.GetComponent<controllerGuide>().BbuttonLight();
                }
                else if (rightControllerAction.ok == 1)
                {
                    StartCoroutine(fadein());
                }
                else if (rightControllerAction.ok == 0)
                {
                    this.gameObject.GetComponent<controllerGuide>().offAbutton();
                    this.gameObject.GetComponent<controllerGuide>().offBbutton();
                    alarmPanel.GetComponent<Animator>().SetBool("alarming", false);
                    alarmPanel.transform.GetChild(1).gameObject.SetActive(false);
                    rightControllerAction.ok = -1;
                    typing = false;
                    planetButtons.planetNum = -1;
                }
            }
            else
            {
                if (rightControllerAction.ok == 0)
                {
                    this.gameObject.GetComponent<controllerGuide>().offAbutton();
                    this.gameObject.GetComponent<controllerGuide>().offBbutton();
                    alarmPanel.GetComponent<Animator>().SetBool("alarming", false);
                    alarmPanel.transform.GetChild(1).gameObject.SetActive(false);
                    rightControllerAction.ok = -1;
                    typing = false;
                    touring = false;
                }
                else if (rightControllerAction.ok == 1)
                {
                    this.gameObject.GetComponent<controllerGuide>().offAbutton();
                    this.gameObject.GetComponent<controllerGuide>().offBbutton();
                    alarmPanel.transform.GetChild(1).gameObject.SetActive(false);
                    rightControllerAction.ok = -1;
                    typing = false;
                    if(touring)
                    {
                        StartCoroutine(fadein());
                    }
                }
                /*else
                {
                    if(!touring)
                    {
                        alarmPanel.transform.GetChild(0).GetComponent<Text>().text = "우주비행사 모드입니다.";
                        alarmPanel.GetComponent<Animator>().SetBool("alarming", false);
                    }
                }*/
            }
        }
    }
    
    IEnumerator Typing(Text typingText, string message, float speed)
    {
        for(int i = 0; i < message.Length; i++)
        {
            typingText.text = message.Substring(0, i + 1);
            yield return new WaitForSeconds(speed);
            if (i == message.Length-1)
            {
                if (indexOfTutorial <= alarmString.Count)
                {
                    yield return new WaitForSeconds(0.1f);
                    indexOfTutorial += 1;
                    typing = false;
                }
            }
        }
    }

    public void rTriggerpressed()
    {
        if (indexOfTutorial == 5||indexOfTutorial==6)
            rTriggerbuttonpressed = true;
    }

    public void Xbuttonpressed()
    {
        if (indexOfTutorial == 7||indexOfTutorial==6)
            xButtonpressed = true;
    }

    public void Ybuttonpressed()
    {
        if (indexOfTutorial == 9||indexOfTutorial==8)
            yButtonpressed = true;
    }

    IEnumerator fadein()
    {
        inParticle.SetActive(true);
        yield return new WaitForSeconds(1.8f);
        if (planetButtons.planetNum == 3)
        {
            rightControllerAction.ok = -1;
            planetButtons.planetNum = -1;
            prevPosOfspaceship = spaceship.transform.position;
            SceneManager.LoadScene("MarsScene");
        }
        else if (planetButtons.planetNum == 2)
        {
            rightControllerAction.ok = -1;
            planetButtons.planetNum = -1;
            prevPosOfspaceship = spaceship.transform.position;
            SceneManager.LoadScene("MoonScene");
        }
        else if(touring)
        {
            this.gameObject.GetComponent<controllerGuide>().AbuttonLight();
            vrtk_sdk.transform.SetParent(null);
            vrtk_sdk.GetComponent<VRTK_SDKManager>().scriptAliasRightController = tourRController;
            spaceship.SetActive(false);
            rightController.SetActive(false);
            leftController.SetActive(false);
            tourRController.SetActive(true);
            railroad.SetActive(true);
            touring = false;
        }
        else if(!spaceship.activeSelf)
        {
            spaceship.SetActive(true);
            this.gameObject.GetComponent<controllerGuide>().offAbutton();
            vrtk_sdk.transform.SetParent(spaceship.transform);
            vrtk_sdk.transform.localPosition = new Vector3(0, 0, 0);
            ovr_cameraRig.transform.localPosition = new Vector3(-0.47f, -0.116f, 0.3f);
            vrtk_sdk.GetComponent<VRTK_SDKManager>().scriptAliasRightController = rightController;
            tourRController.SetActive(false);
            rightController.SetActive(true);
            leftController.SetActive(true);
            railroad.SetActive(false);
        }
    }

    public void tourClick()
    {
        if (tutorialOver)
        {
            alarmPanel.transform.GetChild(1).gameObject.SetActive(true);
            alarmPanel.GetComponent<Animator>().SetBool("alarming", true);
            rightControllerAction.ok = -2;
            StartCoroutine(Typing(alarmPanel.transform.GetChild(0).GetComponent<Text>(), "투어를 시작하시겠습니까?\n(A버튼: 확인, B버튼: 취소)", speedofTyping));
            touring = true;
            this.gameObject.GetComponent<controllerGuide>().AbuttonLight();
            this.gameObject.GetComponent<controllerGuide>().BbuttonLight();
        }
    }

    public void tutorialClick()
    {
        tutorialOver = false;
        alarmPanel.GetComponent<Animator>().SetBool("alarming", true);
        if(leftControllerAction.inSpaceship)
        {
            indexOfTutorial = 7;
            xButtonpressed = true;
            yButtonpressed = false;
        }
        else
        {
            yButtonpressed = false;
            xButtonpressed = false;
            rTriggerbuttonpressed = false;
            indexOfTutorial = 0;
        }
    }

    public void tourOver()
    {
        StartCoroutine(fadein());
    }
}
