using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using VRTK;

public class tutorialPrevScript : MonoBehaviour
{
    [SerializeField]
    private GameObject alarmPanel;

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
    private List<string> listofTutorial;

    // Start is called before the first frame update
    void Start()
    {
        listofTutorial = new List<string>();
        GenerateData();

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
                    StartCoroutine(Typing(alarmPanel.transform.GetChild(0).GetComponent<Text>(), listofTutorial[indexOfTutorial], speedofTyping));
                typing = true;
            }
            else if (indexOfTutorial <= listofTutorial.Count)
            {
                switch (indexOfTutorial)
                {
                    case 5: //right controller 사용자 자유 이동 방법 설명 
                        if (!typing)
                        {
                            rightController.GetComponent<VRTK_Pointer>().enabled = true;
                            leftController.GetComponent<VRTK_Pointer>().enabled = true;
                            this.gameObject.GetComponent<controllerGuide>().RTriggerbuttonLight();
                            StartCoroutine(Typing(alarmPanel.transform.GetChild(0).GetComponent<Text>(), listofTutorial[indexOfTutorial], speedofTyping));
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
                                StartCoroutine(Typing(alarmPanel.transform.GetChild(0).GetComponent<Text>(), listofTutorial[indexOfTutorial], speedofTyping));
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
                                StartCoroutine(Typing(alarmPanel.transform.GetChild(0).GetComponent<Text>(), listofTutorial[indexOfTutorial], speedofTyping));
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
                            StartCoroutine(Typing(alarmPanel.transform.GetChild(0).GetComponent<Text>(), listofTutorial[indexOfTutorial], speedofTyping));
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
                                StartCoroutine(Typing(alarmPanel.transform.GetChild(0).GetComponent<Text>(), listofTutorial[indexOfTutorial], speedofTyping));
                                typing = true;
                            }
                        }
                        break;
                    case 10:
                        if (!typing)
                        {
                            StartCoroutine(Typing(alarmPanel.transform.GetChild(0).GetComponent<Text>(), listofTutorial[indexOfTutorial], speedofTyping));
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
                                StartCoroutine(Typing(alarmPanel.transform.GetChild(0).GetComponent<Text>(), listofTutorial[indexOfTutorial], speedofTyping));
                                typing = true;
                            }
                        }
                        break;
                    case 12:
                        if (!typing)
                        {
                            StartCoroutine(Typing(alarmPanel.transform.GetChild(0).GetComponent<Text>(), listofTutorial[indexOfTutorial], speedofTyping));
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
                    if (touring)
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
        for (int i = 0; i < message.Length; i++)
        {
            typingText.text = message.Substring(0, i + 1);
            yield return new WaitForSeconds(speed);
            if (i == message.Length - 1)
            {
                if (indexOfTutorial <= listofTutorial.Count)
                {
                    yield return new WaitForSeconds(0.1f);
                    indexOfTutorial += 1;
                    typing = false;
                }
            }
        }
    }

    void GenerateData()
    {
        listofTutorial.Add("안녕하세요 VR Space에 오신 여러분을 환영합니다.   0");
        listofTutorial.Add("현재 이곳은 실제 태양계를 반영한 우주입니다.   1");
        listofTutorial.Add("컨트롤러를 이용하여 우주를 자유롭게 이동할 수도 있고   2");
        listofTutorial.Add("행성들에 더 가까이 가고싶다면 우주선을 타고 행성으로 바로 이동하세요.   3");
        listofTutorial.Add("이어서 컨트롤러 사용법에 대해 말씀드리겠습니다.    4");
        listofTutorial.Add("오른쪽 컨트롤러의 트리거 버튼을 클릭하여 이동을 할 수 있습니다.   5");
        listofTutorial.Add("오른쪽 컨트롤러의 조종 레버를 누르면 메뉴판이 뜹니다.\n조종 레버를 돌려서 누르면 메뉴를 선택할 수 있습니다.    6");
        listofTutorial.Add("윗 버튼부터 시계 방향으로 게임 종료 버튼, 도움말 버튼, 공전/자전 배속 조절 버튼입니다.    7");
        listofTutorial.Add("공전/자전 배속 조절 버튼을 눌러보세요.    8");
        listofTutorial.Add("왼쪽 컨트롤러의 트리거 버튼을 이용하여 태양계의 공전과 자전을 조절할 수 있습니다.\n(실제: 1배속)    9");
        listofTutorial.Add("메뉴판의 버튼을 다시 눌러서 panel을 끌 수 있습니다.\n(조종 레버 다시 눌러 메뉴판 끄기)    10");
        listofTutorial.Add("이제 왼쪽 컨트롤러의 X버튼을 클릭하여 우주선으로 이동해보겠습니다.     11");
        listofTutorial.Add("우주선 안에서도 마찬가지로 조종 레버를 눌러 메뉴판을 볼 수 있습니다.\n우주선 밖에서와는 달리 투어 버튼이 있습니다.     12");
        listofTutorial.Add("투어는 투어 가이드에 따라 태양계를 투어하는 콘텐츠입니다.\n(조종레버를 다시 눌러 메뉴판 끄기)     13");
        listofTutorial.Add("다음으로 우주선 조종 방법입니다.\n양손의 게임 스틱을 이용하여 우주선을 조종해보세요.     14");
        listofTutorial.Add("위의 시계는 각 행성에서의 시간의 흐름을 나타냅니다.\n(y버튼 on/off)     15");
        listofTutorial.Add("앞에 보이는 판넬의 행성을 클릭하여 행성으로 바로 이동할 수도 있습니다.     16");
        listofTutorial.Add("오른쪽 포인터를 이용하여 원하는 행성을 클릭해보세요.     17");
        listofTutorial.Add("이제부터는 자유롭게 탐사를 해보세요.      18");
        listofTutorial.Add("우주선을 나가서, 우주선 안에서 자유롭게 탐사를 할 수 있어요.     19");
        //listofTutorial.Add("");
        //listofTutorial.Add("");

    }
    public void rTriggerpressed()
    {
        if (indexOfTutorial == 5 || indexOfTutorial == 6)
            rTriggerbuttonpressed = true;
    }

    public void Xbuttonpressed()
    {
        if (indexOfTutorial == 11 || indexOfTutorial == 12)
            xButtonpressed = true;
    }

    public void Ybuttonpressed()
    {
        if (indexOfTutorial == 15 || indexOfTutorial == 16)
            yButtonpressed = true;
    }

    public void radialpressed()
    {
        /*switch (indexOfTutorial)
        {
            case 6:
            case 7:
            case 10:
            case 11:
                radialPressed = true;
                break; ;
        }*/

    }

    public void multiplepressed()
    {
        //if (indexOfTutorial >= 8 && indexOfTutorial <= 11)
        //    multiPressed = true;
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
        else if (touring)
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
        else if (!spaceship.activeSelf)
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
        if (leftControllerAction.inSpaceship)
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

