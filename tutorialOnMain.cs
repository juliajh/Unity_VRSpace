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
    private bool radialPressed;
    private bool multiPressed;
    private bool touring;
    public static bool tutorialOver;
    private static Vector3 prevPosOfspaceship;
    private List<string> listofTutorial;
    private Animator alarmAni;

    // Start is called before the first frame update
    void Start()
    {
        listofTutorial = new List<string>();
        GenerateData();

        indexOfTutorial = 0;
        speedofTyping = 0.03f;
        typing = false;
        rTriggerbuttonpressed = false;
        yButtonpressed = false;
        xButtonpressed = false;
        multiPressed = false;
        touring = false;

        alarmAni = alarmPanel.GetComponent<Animator>();
        if (tutorialOver)
        {
            spaceship.transform.position = prevPosOfspaceship;
            ovr_cameraRig.transform.localPosition = new Vector3(-0.47f, -0.116f, 0.3f);
            VRTK_BasePointerRenderer straightRenderer = rightController.transform.GetChild(0).GetComponent<VRTK_StraightPointerRenderer>();
            rightController.GetComponent<VRTK_Pointer>().pointerRenderer = straightRenderer;
            rightController.GetComponent<VRTK_Pointer>().enableTeleport = false;
            alarmAni.SetBool("alarming", false);
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
                    rightController.GetComponent<VRTK_ControllerEvents>().enabled = false;
                    leftController.GetComponent<VRTK_ControllerEvents>().enabled = false;
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
                            rightController.GetComponent<VRTK_ControllerEvents>().enabled = true;
                            leftController.GetComponent<VRTK_ControllerEvents>().enabled = true;
                            this.gameObject.GetComponent<controllerGuide>().RTriggerbuttonLight();
                            StartCoroutine(Typing(alarmPanel.transform.GetChild(0).GetComponent<Text>(), listofTutorial[indexOfTutorial], speedofTyping));
                            typing = true;
                        }
                        break;
                    case 6: //외부에서 radial menu 키는 방법 설명
                        if (rTriggerbuttonpressed)
                        {
                            if (!typing)
                            {
                                this.gameObject.GetComponent<controllerGuide>().offRTriggerbutton();
                                rTriggerbuttonpressed = false;
                                this.gameObject.GetComponent<controllerGuide>().RTouchPadbuttonLight();
                                StartCoroutine(Typing(alarmPanel.transform.GetChild(0).GetComponent<Text>(), listofTutorial[indexOfTutorial], speedofTyping));
                                typing = true;
                            }
                        }
                        break;
                    case 7: //외부 radial menu에 대한 설명 
                        if (radialPressed)
                        {
                            if (!typing)
                            {
                                radialPressed = false;
                                StartCoroutine(Typing(alarmPanel.transform.GetChild(0).GetComponent<Text>(), listofTutorial[indexOfTutorial], speedofTyping));
                                typing = true;
                            }
                        }
                        break;
                    case 8: //공전,자전 버튼 설명 시작
                        if (!typing)
                        {
                            StartCoroutine(Typing(alarmPanel.transform.GetChild(0).GetComponent<Text>(), listofTutorial[indexOfTutorial], speedofTyping));
                            typing = true;
                        }
                        break;
                    case 9: //공전, 자전 컨트롤러 설명
                        if(multiPressed)
                        {
                            if (!typing)
                            {
                                this.gameObject.GetComponent<controllerGuide>().LTriggerbuttonLight();
                                multiPressed = false;
                                StartCoroutine(Typing(alarmPanel.transform.GetChild(0).GetComponent<Text>(), listofTutorial[indexOfTutorial], speedofTyping));
                                typing = true;
                            }
                        }
                        break;
                    case 10: //공전, 자전 판넬 끄기
                        if (!typing)
                        {
                            radialPressed = false;
                            this.gameObject.GetComponent<controllerGuide>().offLTriggerbutton();
                            StartCoroutine(Typing(alarmPanel.transform.GetChild(0).GetComponent<Text>(), listofTutorial[indexOfTutorial], speedofTyping));
                            typing = true;
                        }
                        break;
                    case 11: //우주선 안으로 이동하는 방법 설명 
                        if (radialPressed)
                        {
                            if (!typing)
                            {
                                this.gameObject.GetComponent<controllerGuide>().offRTouchPadbutton();
                                radialPressed = false;
                                this.gameObject.GetComponent<controllerGuide>().XbuttonLight();
                                StartCoroutine(Typing(alarmPanel.transform.GetChild(0).GetComponent<Text>(), listofTutorial[indexOfTutorial], speedofTyping));
                                typing = true;
                            }
                        }
                        break;
                    case 12: //우주선 안에서도 가이드
                        if (xButtonpressed)
                        {
                            if (!typing)
                            {
                                radialPressed = false;
                                this.gameObject.GetComponent<controllerGuide>().offXbutton();
                                xButtonpressed = false;
                                this.gameObject.GetComponent<controllerGuide>().RTouchPadbuttonLight();
                                StartCoroutine(Typing(alarmPanel.transform.GetChild(0).GetComponent<Text>(), listofTutorial[indexOfTutorial], speedofTyping));
                                typing = true;
                            }
                        }
                        break;
                    case 13: //우주선 안 가이드 설명
                        if (radialPressed)
                        {
                            if (!typing)
                            {
                                radialPressed = false;
                                StartCoroutine(Typing(alarmPanel.transform.GetChild(0).GetComponent<Text>(), listofTutorial[indexOfTutorial], speedofTyping));
                                typing = true;
                            }
                        }
                        break;
                    case 14: //우주선 조종 설명 
                        if (radialPressed)
                        {
                            if (!typing)
                            {
                                radialPressed = false;
                                this.gameObject.GetComponent<controllerGuide>().LTouchPadbuttonLight();
                                this.gameObject.GetComponent<controllerGuide>().RTouchPadbuttonLight();
                                StartCoroutine(Typing(alarmPanel.transform.GetChild(0).GetComponent<Text>(), listofTutorial[indexOfTutorial], speedofTyping));
                                typing = true;
                            }
                        }
                        break;
                    case 15: //y버튼으로 시계 panel on off 설명 
                        if (!typing)
                        {
                            this.gameObject.GetComponent<controllerGuide>().offLTouchPadbutton();
                            this.gameObject.GetComponent<controllerGuide>().offRTouchPadbutton();
                            this.gameObject.GetComponent<controllerGuide>().YbuttonLight();
                            StartCoroutine(Typing(alarmPanel.transform.GetChild(0).GetComponent<Text>(), listofTutorial[indexOfTutorial], speedofTyping));
                            typing = true;
                        }
                        break;
                    case 16:
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
                    case 17:
                        if (!typing)
                        {
                            StartCoroutine(Typing(alarmPanel.transform.GetChild(0).GetComponent<Text>(), listofTutorial[indexOfTutorial], speedofTyping));
                            typing = true;
                        }
                        break;
                    case 18:
                        if (planetButtons.planetNum >= 0)
                        {
                            if (!typing)
                            {
                                this.gameObject.GetComponent<controllerGuide>().offRTriggerbutton();
                                rTriggerbuttonpressed = false;
                                StartCoroutine(Typing(alarmPanel.transform.GetChild(0).GetComponent<Text>(), listofTutorial[indexOfTutorial], speedofTyping));
                                typing = true;
                            }
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
                    alarmAni.SetBool("alarming", true);
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
                    alarmAni.SetBool("alarming", false);
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
                    alarmAni.SetBool("alarming", true);
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
                    alarmAni.SetBool("alarming", false);
                    alarmPanel.transform.GetChild(1).gameObject.SetActive(false);
                    rightControllerAction.ok = -1;
                    typing = false;
                    planetButtons.planetNum = -1;
                }
            }
            if (touring)
            {
                if (rightControllerAction.ok == 0)
                {
                    this.gameObject.GetComponent<controllerGuide>().offAbutton();
                    this.gameObject.GetComponent<controllerGuide>().offBbutton();
                    alarmAni.SetBool("alarming", false);
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
                    alarmAni.SetBool("alarming", false);
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
                yield return new WaitForSeconds(0.1f);
                if (indexOfTutorial < listofTutorial.Count)
                    indexOfTutorial += 1;
                typing = false;
            }
        }

    }

    void GenerateData()
    {
        listofTutorial.Add("안녕하세요 VR Space에 오신 여러분을 환영합니다.");
        listofTutorial.Add("현재 이곳은 실제 태양계를 반영한 우주입니다.");
        listofTutorial.Add("컨트롤러를 이용하여 우주를 자유롭게 탐사해보세요.");
        listofTutorial.Add("양손을 들어서 컨트롤러를 확인해보세요.");
        listofTutorial.Add("튜토리얼에 따라 흰 테두리 버튼을 누르면 됩니다.");
        listofTutorial.Add("양손 컨트롤러의 트리거 버튼을 클릭하여 이동을 할 수 있습니다.");
        listofTutorial.Add("오른손 컨트롤러의 게임 스틱를 누르면 메뉴판이 뜹니다.");
        listofTutorial.Add("게임 스틱를 돌려서 누르면 메뉴를 선택할 수 있습니다.\n시계 방향으로 게임종료 버튼, 도움말 버튼, 공전/자전 배속 조절 버튼입니다.");
        listofTutorial.Add("공전/자전 배속 조절 버튼을 눌러보세요.");
        listofTutorial.Add("왼손 컨트롤러의 트리거 버튼을 눌러 태양계의 공전과 자전을 조절할 수 있습니다.\n(실제: 1배속)");
        listofTutorial.Add("게임 스틱을 다시 눌러서 panel을 끌 수 있습니다.");
        listofTutorial.Add("이제 왼손 컨트롤러의 X버튼을 클릭하여 우주선으로 이동해보겠습니다.");
        listofTutorial.Add("조종 레버를 눌러 메뉴판을 볼 수 있습니다.\n우주선 밖에서와는 달리 투어 버튼이 있습니다.");
        listofTutorial.Add("투어는 투어 가이드에 따라 태양계를 투어하는 콘텐츠입니다.\n게임 스틱을 다시 눌러서 panel을 끌 수 있습니다.");
        listofTutorial.Add("다음으로 우주선 조종 방법입니다.\n양손의 게임 스틱을 이용하여 우주선을 조종해보세요.");
        listofTutorial.Add("위의 시계는 각 행성에서의 시간의 흐름을 나타냅니다.\n(y버튼 on/off)");
        listofTutorial.Add("앞에 보이는 판넬의 행성을 클릭하여 행성으로 바로 이동할 수 있습니다.");
        listofTutorial.Add("양손 트리거 버튼을 눌러 원하는 행성을 클릭해보세요.");
        listofTutorial.Add("컨트롤러 사용법 설명을 마치겠습니다.");
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
        switch (indexOfTutorial)
        {
            case 6:
            case 7:
            case 9:
            case 11:
            case 12:
            case 13:
            case 14:
                radialPressed = true;
                break; ;
        }
    } 

    public void multiplepressed()
    {
        if (indexOfTutorial == 8 || indexOfTutorial == 9)
            multiPressed = true;
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
            alarmAni.SetBool("alarming", true);
            rightControllerAction.ok = -2;
            StartCoroutine(Typing(alarmPanel.transform.GetChild(0).GetComponent<Text>(), "투어를 시작하시겠습니까?\n(A버튼: 확인, B버튼: 취소)", speedofTyping));
            touring = true;
            this.gameObject.GetComponent<controllerGuide>().AbuttonLight();
            this.gameObject.GetComponent<controllerGuide>().BbuttonLight();
        }
    }

    public void tourOver()
    {
        StartCoroutine(fadein());
    }
}
