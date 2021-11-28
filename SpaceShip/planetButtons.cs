using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VRTK;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class planetButtons : MonoBehaviour
{
    [SerializeField]
    private GameObject sun;

    [SerializeField]
    private GameObject spaceship;

    [SerializeField]
    private GameObject ovr_cameraRig;

    [SerializeField]
    private GameObject infoText;

    public string planetName = null;

    public static int planetNum;

    private void Start()
    {
        planetNum = -1;
    }

    public void clickPlanet(GameObject planet)
    {
        Vector3 distance;
        //ovr_cameraRig.transform.position = spaceship.transform.position;
        rightControllerAction.ok = -1;

        switch (planet.name)
        {
            case "Mercury":
                {
                    distance = planet.transform.localScale + new Vector3(-25.0f, 0f, -25.0f);
                    spaceship.transform.position = new Vector3(distance.x + planet.transform.position.x, 3.7f, distance.z + planet.transform.position.z);
                    spaceship.transform.LookAt(planet.transform);
                    infoText.GetComponent<Text>().text = "수성 Mercury\n온도(K):440\n대기 성분:산소, 나트륨, 수소\n대표 위성:없음\n겉보기 등급:-1.9";
                    planetName = "Mercury";
                    planetNum = 0;
                    break;
                }
            case "Venus":
                {
                    distance = planet.transform.localScale + new Vector3(-55.0f, 0f, -55.0f);
                    spaceship.transform.position = new Vector3(distance.x + planet.transform.position.x, 3.7f, distance.z + planet.transform.position.z);
                    spaceship.transform.LookAt(planet.transform);
                    infoText.GetComponent<Text>().text = "금성 Venus\n온도(K):730\n대기 성분:이산화탄소(96.5%)\n대표 위성:없음\n겉보기 등급:-4.6~-3.8";
                    planetName = "Venus";
                    planetNum = 1;
                    break;
                }
            case "Earth":
                {
                    distance = planet.transform.localScale + new Vector3(-60.0f, 0f, -60.0f);
                    spaceship.transform.position = new Vector3(distance.x + planet.transform.position.x, 3.7f, distance.z + planet.transform.position.z);
                    spaceship.transform.LookAt(planet.transform);
                    infoText.GetComponent<Text>().text = "지구 Earth\n온도(K):280\n대기 성분:질소, 산소\n대표 위성:달";
                    planetName = "Earth";
                    planetNum = 2;
                    break;
                }
            case "Mars":
                {
                    distance = planet.transform.localScale + new Vector3(-33.0f, 0f, -33.0f);
                    spaceship.transform.position = new Vector3(distance.x + planet.transform.position.x, 3.7f, distance.z + planet.transform.position.z);
                    spaceship.transform.LookAt(planet.transform);
                    infoText.GetComponent<Text>().text = "화성 Mars\n온도(K):230\n대기 성분:이산화탄소(95.3%), 질소\n대표 위성:포보스, 데이모스\n겉보기 등급:-2.91~1.8";
                    planetName = "Mars";
                    planetNum = 3;
                    break;
                }
            case "Jupiter":
                {
                    distance = planet.transform.localScale + new Vector3(-630.0f, 0f, -630.0f);
                    spaceship.transform.position = new Vector3(distance.x + planet.transform.position.x, 3.7f, distance.z + planet.transform.position.z);
                    spaceship.transform.LookAt(planet.transform);
                    infoText.GetComponent<Text>().text = "목성 Jupiter\n온도(K):125\n대기 성분:수소, 헬륨\n대표 위성:갈릴레이 위성 (외 78개)\n겉보기 등급:-2.94~-1.6";
                    planetName = "Jupiter";
                    planetNum = 4;
                    break;
                }
            case "Saturn":
                {
                    distance = planet.transform.localScale + new Vector3(-520.0f, 0f, -520.0f);
                    spaceship.transform.position = new Vector3(distance.x + planet.transform.position.x, 3.7f, distance.z + planet.transform.position.z);
                    spaceship.transform.LookAt(planet.transform);
                    infoText.GetComponent<Text>().text = "토성 Saturn\n온도(K):95\n대기 성분:수소, 헬륨\n대표 위성:타이탄 (외 81개)\n겉보기 등급:-0.24~1.2";
                    planetName = "Saturn";
                    planetNum = 5;
                    break;
                }
            case "Uranus":
                {
                    distance = planet.transform.localScale + new Vector3(-270.0f, 0f, -270.0f);
                    spaceship.transform.position = new Vector3(distance.x + planet.transform.position.x, 3.7f, distance.z + planet.transform.position.z);
                    spaceship.transform.LookAt(planet.transform);
                    infoText.GetComponent<Text>().text = "천왕성 Uranus\n온도(K):60\n대기 성분:수소, 헬륨\n대표 위성:티타니아 (외 26개)\n겉보기 등급:5.32~5.9";
                    planetName = "Uranus";
                    planetNum = 6;
                    break;
                }
            case "Neptune":
                {
                    distance = planet.transform.localScale + new Vector3(-270.0f, 0f, -270.0f);
                    spaceship.transform.position = new Vector3(distance.x + planet.transform.position.x, 3.7f, distance.z + planet.transform.position.z);
                    spaceship.transform.LookAt(planet.transform);
                    infoText.GetComponent<Text>().text = "해왕성 Neptune\n온도(K):60\n대기 성분:수소, 헬륨\n대표 위성:트리톤 (외 13개)\n겉보기 등급:7.78~8.0";
                    planetName = "Neptune";
                    planetNum = 7;
                    break;
                }
            default:
                break;
        }

    }
}
