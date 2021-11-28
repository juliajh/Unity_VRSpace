using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using VRTK;

public class spaceTour : MonoBehaviour
{
    [SerializeField]
    private GameObject fadeinparticle;

    [SerializeField]
    private GameObject tourText;

    [SerializeField]
    private GameObject vrtk_sdk;

    private Animator railroadAnimator;
    //private Animator panelAnimator;
    private Dictionary<GameObject, string> planetInfo;
    private List<string> Info = new List<string>();
    private int planetNum;
    private bool tourover;

    // Start is called before the first frame update
    void Start()
    {
        Info.Add("수성\n운행이 가장 빠르기 때문에 \n발이 빠른 신의 이름을 붙인 헤르메스와 대응합니다.\n한편, 수성은 항상 태양 옆에 붙어 다니기 때문에 관측하기가 쉽지 않습니다.\n수성은 해가 진 직후, 서쪽하늘과 해가 뜨기 직전 동쪽 하늘에서만 \n볼 수 있는데 그 모습은 달과 굉장히 비슷합니다.\n(다음 행성은 금성입니다. A버튼 클릭)");
        Info.Add("금성\n가장 밝기 때문에 아름다운 여성의 이름인 비너스와 대응합니다.\n한편, 금성은 두꺼운 이산화탄소로 덮여 있기 때문에 망원경으로는 표면이 보이지 않습니다.\n금성은 해 뜨기 전 동쪽 하늘이나 해진 후 서쪽하늘에서 주로 관측됩니다.\n(다음 행성은 지구입니다. A버튼 클릭)");
        Info.Add("지구\n우리가 살고 있는 푸른 행성이 바로 지구입니다.\n녹색의 산, 푸른 바다, 갈색의 흙이 조화를 이루고 있는 아름다운 행성입니다.\n35억년 전에 지구에 원시 생명이 탄생한 것으로 추측되어 지고 있습니다.\n(다음 행성은 화성입니다. A버튼 클릭)");
        Info.Add("화성\n제2의 지구라고 불리는 행성으로 \n최근 관심을 많이 받고 있는 행성입니다.\n여러가지 에피소드에 의해 생명의 존재 가능성이 제기되어 \n많은 탐사가 이루어지고 있습니다.\n(다음 행성은 목성입니다. A버튼 클릭)");
        Info.Add("목성\n태양계에 존재하는 가장 거대한 행성입니다.\n부피 뿐만 아니라 질량도 최대이기 때문에 \n육안으로도 쉽게 관측이 가능합니다.\n대표적 특징으로는 대적점이 존재한다는 것입니다.\n(다음 행성은 토성입니다. A버튼 클릭)");
        Info.Add("토성\n아름다운 고리를 가진 행성으로 알려진 행성입니다.\n신비한 위성인 타이탄이 토성 주위를 돌고 있으며\n다른 위성 중에서 보기 힘든 짙은 대기로 감싸여 있습니다.\n(다음 행성은 천왕성입니다. A버튼 클릭)");
        Info.Add("천왕성\n청녹색을 띄는 행성으로 망원경으로 발견한 최초의 행성입니다.\n다른 행성들과 달리 자전축이 거의 황도면에 누워있는 형태로 자전합니다.\n(다음 행성은 해왕성입니다. A버튼 클릭)");
        Info.Add("해왕성\n천왕성과 굉장히 비슷한 행성지만,\n대기의 흐름이 활발하여 회오리가 생긴다는 점은 큰 차이점입니다.\n80%가 수소로 이루어져 있습니다.\n(투어 종료. A버튼 클릭)");
        planetInfo = new Dictionary<GameObject, string>();
        planetInfo.Add(GameObject.Find("Mercury"),Info[0]);
        planetInfo.Add(GameObject.Find("Venus"), Info[1]);
        planetInfo.Add(GameObject.Find("Earth"), Info[2]);
        planetInfo.Add(GameObject.Find("Mars"), Info[3]);
        planetInfo.Add(GameObject.Find("Jupiter"), Info[4]);
        planetInfo.Add(GameObject.Find("Saturn"), Info[5]);
        planetInfo.Add(GameObject.Find("Uranus"), Info[6]);
        planetInfo.Add(GameObject.Find("Neptune"), Info[7]);
        tourover = false;

        /*foreach (GameObject obj in GameObject.FindGameObjectsWithTag("AstronomicalBody"))
        {

        }*/
        planetNum = 0;
        vrtk_sdk.transform.position = new Vector3(-29.0f, 3.709f, -1883.0f);
        tourText.SetActive(true);
        tourText.GetComponent<TextMesh>().text = "vr space 투어에 오신 여러분을 환영합니다.\n 여러분은 현재 태양계에 있습니다.\n태양계는 나선 은하로 태양을 중심으로 \n수성, 금성, 지구, 화성, 목성, 토성, 천왕성, 해왕성 \n총 8개의 행성들이 공전하고 있고\n 많은 왜소행성, 소행성 등으로 이루어져 있습니다.  ";
        railroadAnimator = this.GetComponent<Animator>();
        railroadAnimator.enabled = false;
    }

    IEnumerator moving()
    {
        railroadAnimator.Play("trainLeft");
        railroadAnimator.SetBool("trainStart", false);
        tourText.SetActive(false);
        fadeinparticle.SetActive(true);
        yield return new WaitForSeconds(1.05f);
        planetSummary(planetNum);
        yield return new WaitForSeconds(5.0f);
        fadeinparticle.SetActive(false);
        railroadAnimator.enabled = false;
    }

    public void next()
    {
        if(!tourover)
        {
            if(!railroadAnimator.enabled)
            {
                railroadAnimator.enabled = true;
                StartCoroutine(moving());
            }
        }
        else
        {
            planetNum = 0;
            GameObject.Find("GameManager").GetComponent<tutorialOnMain>().tourOver();
        }
    }

    public void planetSummary(int num)
    {
        Vector3 distance = new Vector3();
        GameObject planet = new GameObject();

        switch(num)
        {
            case 0:
                {
                    planet = GameObject.Find("Mercury");
                    distance = planet.transform.localScale + new Vector3(-60.0f, 0f, -60.0f);
                    break;
                }
            case 1:
                {
                    planet = GameObject.Find("Venus");
                    distance = planet.transform.localScale + new Vector3(-100.0f, 0f, -100.0f);
                    break;
                }
            case 2:
                {
                    planet = GameObject.Find("Earth");
                    distance = planet.transform.localScale + new Vector3(-90.0f, 0f, -90.0f);
                    break;
                }
            case 3:
                {
                    planet = GameObject.Find("Mars");
                    distance = planet.transform.localScale + new Vector3(-90.0f, 0f, -90.0f);
                    break;
                }
            case 4:
                {
                    planet = GameObject.Find("Jupiter");
                    distance = planet.transform.localScale + new Vector3(-720.0f, 0f, -720.0f);
                    break;
                }
            case 5:
                {
                    planet = GameObject.Find("Saturn");
                    distance = planet.transform.localScale + new Vector3(-650.0f, 0f, -650.0f);
                    break;
                }
            case 6:
                {
                    planet = GameObject.Find("Uranus");
                    distance = planet.transform.localScale + new Vector3(-320.0f, 0f, -320.0f);
                    break;
                }
            case 7:
                {
                    planet = GameObject.Find("Neptune");
                    distance = planet.transform.localScale + new Vector3(-320.0f, 0f, -320.0f);
                    break;
                }

            default:
                {
                    vrtk_sdk.transform.position = new Vector3(-29.0f, 3.709f, -1883.0f);
                    tourText.SetActive(true);
                    vrtk_sdk.transform.eulerAngles = new Vector3(vrtk_sdk.transform.eulerAngles.x, 60.0f, vrtk_sdk.transform.eulerAngles.z);
                    tourText.GetComponent<TextMesh>().text = "투어가 끝났습니다.\n태양계에 대해서 조금이라도 알게 되는 시간이셨길 바랍니다.\n감사합니다.(A버튼: 종료)";
                    tourover = true;
                    break;
                }

        }
        if(!tourover)
        {
            vrtk_sdk.transform.position = new Vector3(distance.x + planet.transform.position.x, 3.7f, distance.z + planet.transform.position.z);
            tourText.SetActive(true);
            vrtk_sdk.transform.eulerAngles = new Vector3(vrtk_sdk.transform.eulerAngles.x, 60.0f, vrtk_sdk.transform.eulerAngles.z);
            tourText.GetComponent<TextMesh>().text = planetInfo[planet];
            planetNum += 1;
        }
    }
}
