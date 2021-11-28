using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OrbitSatellite : MonoBehaviour
{
    public Transform orbitingObject;
    public Ellipse orbitPath;
    public float radius; //장반경의 절반 
    private float distance;

    [Range(0f, 1f)]
    public float orbitProgress;

    public bool orbitActive = true;

    private float nextTime = 0.0f;
    private float displacement = 0;  //공전궤도에서 이동한 거리

    //Earth Gm + moon gm
    private float moongm = 403505f;

    //Phobos, Deimos, Io, Europa는 gm 정보 X

    //moon, phobos, deimos, IO, Europa
    private List<float> orbitalCycle = new List<float> { 2414915.0784f, 58957.488f, 147458.18016f, 2649356.2944f, 4215733.6205f};

    // Start is called before the first frame update
    void Start()
    {
        SetOrbitingObjectPosition();
    }
    void SetOrbitingObjectPosition()
    {
        Vector2 orbitPos = orbitPath.Evaluate(orbitProgress);
        orbitingObject.localPosition = new Vector3(orbitPos.x, 0, orbitPos.y);

    }
    // Update is called once per frame
    void Update()
    {
        distance = Mathf.Sqrt(Mathf.Pow(this.transform.localPosition.x, 2) + Mathf.Pow(this.transform.localPosition.y, 2) + Mathf.Pow(this.transform.localPosition.z, 2));
        if (Time.time*1000 > nextTime)
        {
            nextTime = Time.time*1000 + 0.01f;

            if (orbitActive)
            {
                satelliteOrbit();
            }
        }
    }

    private void satelliteOrbit()
    {
        if (this.name == "Moon")
        {
            float orbitSpeed = calculatingMoonSpeed();  //활력방정식을 통해 moon Speed 구하기. km/s
            displacement += orbitSpeed * 0.01f *Multiple.multipleTime;   //displacement에 초속 이동 거리 더해서 총 이동거리

            orbitProgress = displacement / orbitalCycle[0]; //현재 이동거리/공전궤도 길이
        }

        else if(this.name== "Phobos")
        {
            displacement += 2.138f * 0.01f * Multiple.multipleTime;
            orbitProgress = displacement / orbitalCycle[1];
        }

        else if (this.name == "Deimos")
        {
            displacement += 1.3513f* 0.01f * Multiple.multipleTime;
            orbitProgress = displacement / orbitalCycle[2];
        }

        else if (this.name == "Io")
        {
            displacement += 17.334f * 0.01f * Multiple.multipleTime;
            orbitProgress = displacement / orbitalCycle[3];
        }

        else if (this.name == "Europa")
        {
            displacement += 13.740f* 0.01f * Multiple.multipleTime;
            orbitProgress = displacement / orbitalCycle[4];
        }

        orbitProgress %= 1;
        SetOrbitingObjectPosition();
    }

    private float calculatingMoonSpeed()  //km/s
    {
        float speed = 0;
        speed = Mathf.Sqrt((moongm) * (((2 / (distance * 55000)) - ((1 / (radius * 55000))))));
        return speed;
    }
}
