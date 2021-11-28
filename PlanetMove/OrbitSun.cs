using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//태양 주위로 공전하는 행성들의 틀
public class OrbitSun : MonoBehaviour
{
    public Transform orbitingObject;
    public Ellipse orbitPath;

    public float radius; //장반경의 절반 
    private float distance;

    [Range(0f, 1f)]
    public float orbitProgress;
    public bool orbitActive = true;

    public float rotateSpeed;
    private float nextTime = 0.0f;
    private float displacement = 0;  //공전궤도에서 이동한 거리

    //gm 수금지화목토천해
    private List<float> gmList = new List<float> { 22032, 324859, 398600, 42828, 126686534, 37931187, 5793939, 6836529 };

    //공전궤도 길이(km)
    private List<float> orbitalCycle = new List<float> { 363860106.480f, 679908261.312f, 939997252.858f, 1432249608.73f,
        4893246747.043f, 8988906431.232f, 18134654599.988f, 28502622716.832f };

    private float GM = 132712440018;  //태양 GM

    // Start is called before the first frame update
    void Start()
    {
        rotateSpeed /= 300;

        //Add Gm+GM
        for(int i=0;i<8; i++)
        {
            gmList[i] += GM;
        }

        if (orbitingObject == null)
        {
            orbitActive = false;
            return;
        }

        orbitProgress = 0;
        SetOrbitingObjectPosition();
    }

    void SetOrbitingObjectPosition()
    {
        Vector2 orbitPos = orbitPath.Evaluate(orbitProgress);

        orbitingObject.localPosition= new Vector3(orbitPos.x, 0, orbitPos.y);

    }

    public void orbitSun()  //공전
    {
        
        float orbitSpeed = calculatingSpeed();  //활력방정식을 통해 speed 구하기. km/s

        displacement += orbitSpeed* Multiple.multipleTime *0.01f;   //displacement에 초속 이동 거리 더해서 총 이동거리

        if (this.name == "Mercury")
        {
            orbitProgress = displacement / orbitalCycle[0]; //현재 이동거리/공전궤도 길이
        }
        else if (this.name == "Venus")
        {
            orbitProgress = displacement / orbitalCycle[1];
        }
        else if (this.name == "Earth&Satellite")
        {
            orbitProgress = displacement / orbitalCycle[2];
        }
        else if (this.name == "Mars&Satellite")
        {
            orbitProgress = displacement / orbitalCycle[3];
        }
        else if (this.name == "Jupiter&Satellite")
        {
            orbitProgress = displacement / orbitalCycle[4];
        }
        else if (this.name == "Saturn")
        {
            orbitProgress = displacement / orbitalCycle[5];
        }
        else if (this.name == "Uranus")
        {
            orbitProgress = displacement / orbitalCycle[6];
        }
        else if (this.name == "Neptune")
        {
            orbitProgress = displacement / orbitalCycle[7];
        }

        orbitProgress %= 1;
        SetOrbitingObjectPosition();
    }    
    

    // Update is called once per frame
    void Update()
    {
        distance = Mathf.Sqrt(Mathf.Pow(this.transform.position.x, 2) + Mathf.Pow(this.transform.position.y, 2) + Mathf.Pow(this.transform.position.z, 2));

        if (Time.time*1000 >= nextTime)  //0.01초마다 
        {
            nextTime = Time.time *1000 + 0.01f;
            if (orbitActive)
            {
                orbitSun();
            }
        }
    }

    
    private float calculatingSpeed()  //km/s
    {
        float speed =0;

        if (this.name == "Mercury")
            speed = Mathf.Sqrt(gmList[0] * (((2 / (distance * 55000)) - ((1 / (radius * 55000)))))); 
        else if (this.name == "Venus")
            speed = Mathf.Sqrt(gmList[1] * (((2 / (distance * 55000)) - ((1 / (radius * 55000))))));
        else if (this.name == "Earth&Satellite")
            speed = Mathf.Sqrt(gmList[2] * (((2 / (distance * 55000)) - ((1 / (radius * 55000))))));
        else if (this.name == "Mars&Satellite")
            speed = Mathf.Sqrt(gmList[3] * (((2 / (distance * 55000)) - ((1 / (radius * 55000))))));
        else if (this.name == "Jupiter&Satellite")
            speed = Mathf.Sqrt(gmList[4] * (((2 / (distance * 55000)) - ((1 / (radius * 55000))))));
        else if (this.name == "Saturn")
            speed = Mathf.Sqrt(gmList[5] * (((2 / (distance * 55000)) - ((1 / (radius * 55000))))));
        else if (this.name == "Uranus")
            speed = Mathf.Sqrt(gmList[6] * (((2 / (distance * 55000)) - ((1 / (radius * 55000))))));
        else if (this.name == "Neptune")
            speed = Mathf.Sqrt(gmList[7] * (((2 / (distance * 55000)) - ((1 / (radius * 55000))))));
        
        return speed;
    }
    
}
