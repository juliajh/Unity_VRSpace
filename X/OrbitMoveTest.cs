using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//태양 주위로 공전하는 행성들의 틀
public class OrbitMoveTest : MonoBehaviour
{
    public Transform orbitingObject;
    public Ellipse orbitPath;

    public float rotateSpeed;  //자전속도
    public float GM; //중력상수 x 질량 
    public float radius; //장반경의 절반 
    private float distance;

    [SerializeField]
    private GameObject sun;

    //[Range(0f, 1f)]
    public float orbitProgress;
    public float orbitperiod = 1f;
    public bool orbitActive = true;

    //원래 필요한 변수. 임시 코드 돌리기 위해 잠시 주석.
    /*[Range(1f,10000f)]
    public float multiple; //테스트 용으로 우선 공전을 빠르게 보기 위해 놓은 배수값.
    */

    // Start is called before the first frame update
    void Start()
    {
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
        orbitingObject.localPosition = new Vector3(orbitPos.x, 0, orbitPos.y);
    }

    // Update is called once per frame
    void Update()
    {
        distance = Vector3.Distance(sun.transform.position, this.transform.position);
        float orbitSpeed = calculatingSpeed();
      //  Debug.Log("before progress : " + orbitProgress);  //NaN

        if (Time.deltaTime > 0)
        {
           // Debug.Log("Time.deltaTime * orbitSpeed : " + Time.deltaTime * orbitSpeed);
            orbitProgress += Time.deltaTime * orbitSpeed;
           // Debug.Log("after progress : " + orbitProgress);
            orbitProgress %= 1;
        }

       // Debug.Log("orbitProgress: " + orbitProgress);
        SetOrbitingObjectPosition();
        this.transform.RotateAround(this.transform.position, -Vector3.down, rotateSpeed * Time.deltaTime); //자전
    }

    private float calculatingSpeed()  //m/s
    {
        float speed = Mathf.Sqrt(GM * ((2 / distance) - (1 / radius)));
        return speed;
    }
}
