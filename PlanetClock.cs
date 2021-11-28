using UnityEngine;
using System.Collections;


public class PlanetClock : MonoBehaviour
{
    [SerializeField]
    private GameObject EarthClock;
    [SerializeField]
    private GameObject MercuryClock;
    [SerializeField]
    private GameObject VenusClock;
    [SerializeField]
    private GameObject MarsClock;
    [SerializeField]
    private GameObject JupiterClock;
    [SerializeField]
    private GameObject SaturnClock;
    [SerializeField]
    private GameObject UranusClock;
    [SerializeField]
    private GameObject NeptuneClock;

    //-- set start time 00:00
    private int realminutes = 0;
    private int realhour = 0;
    private int realseconds = 0;

    private float[] minutes;
    private float[] seconds;
    private float[] hours;

    private GameObject pointerSeconds;
    private GameObject pointerMinutes;
    private GameObject pointerHours;

    /*
    private string planetName;


    //-- time speed factor
    public float clockSpeed;     // 1.0f = realtime, < 1.0f = slower, > 1.0f = faster
    */
    //-- internal vars
    float[] msecs = new float[8] { 0, 0, 0, 0, 0, 0, 0, 0 };

    void Start()
    {
        //-- set real time
        realhour = System.DateTime.Now.Hour;
        realminutes = System.DateTime.Now.Minute;
        realseconds = System.DateTime.Now.Second;

        minutes = new float[8] { realminutes , realminutes , realminutes , realminutes , realminutes , realminutes , realminutes , realminutes };
        seconds = new float[8] { realseconds, realseconds, realseconds, realseconds, realseconds, realseconds, realseconds, realseconds };
        hours = new float[8] { realhour, realhour, realhour, realhour, realhour, realhour, realhour, realhour };
    }

    public void calculatingClockSpeed(GameObject planet, float clockSpeed, int n) 
    {

        msecs[n] += Time.deltaTime * clockSpeed;
        if (msecs[n] >= 1.0f)
            {
                msecs[n] -= 1.0f;
                seconds[n]++;
                if (seconds[n] >= 60)
                {
                    seconds[n] = 0;
                    minutes[n]++;
                    if (minutes[n] > 60)
                    {
                        minutes[n] = 0;
                        hours[n]++;
                        if (hours[n] >= 24)
                            hours[n] = 0;
                    }
                }
            }

            float rotationSeconds = (360.0f / 60.0f) * seconds[n];
            float rotationMinutes = (360.0f / 60.0f) * minutes[n];
            float rotationHours = ((360.0f / 12.0f) * hours[n]) + ((360.0f / (60.0f * 12.0f)) * minutes[n]);


            //-- draw pointers
            planet.transform.GetChild(1).transform.GetChild(3).transform.localEulerAngles = new Vector3(0.0f, 0.0f, rotationSeconds);
            planet.transform.GetChild(1).transform.GetChild(1).transform.localEulerAngles = new Vector3(0.0f, 0.0f, rotationMinutes);
            planet.transform.GetChild(1).transform.GetChild(0).transform.localEulerAngles = new Vector3(0.0f, 0.0f, rotationHours);

        
    }

    void Update()
    {
        //-- calculate time
        calculatingClockSpeed(MercuryClock, 176.0f, 0);
        calculatingClockSpeed(VenusClock, 116.0f, 1);
        calculatingClockSpeed(EarthClock, 1.0f, 2);
        calculatingClockSpeed(MarsClock, 1.0f, 3);
        calculatingClockSpeed(JupiterClock, 0.38f,4);
        calculatingClockSpeed(SaturnClock, 0.42f,5);
        calculatingClockSpeed(UranusClock, 0.71f,6);
        calculatingClockSpeed(NeptuneClock, 0.67f,7);


        /*
            msecs += Time.deltaTime * clockSpeed;
            if (msecs >= 1.0f)
            {
                msecs -= 1.0f;
                seconds++;
                if (seconds >= 60)
                {
                    seconds = 0;
                    minutes++;
                    if (minutes > 60)
                    {
                        minutes = 0;
                        hour++;
                        if (hour >= 24)
                            hour = 0;
                    }
                }
            }


            //-- calculate pointer angles
            float rotationSeconds = (360.0f / 60.0f) * seconds;
            float rotationMinutes = (360.0f / 60.0f) * minutes;
            float rotationHours = ((360.0f / 12.0f) * hour) + ((360.0f / (60.0f * 12.0f)) * minutes);

            //-- draw pointers
            Зајє.transform.Getchild(1).transform.Getchild(3).transform.localEulerAngles = new Vector3(0.0f, 0.0f, rotationSeconds);
            pointerMinutes.transform.localEulerAngles = new Vector3(0.0f, 0.0f, rotationMinutes);
            pointerHours.transform.localEulerAngles = new Vector3(0.0f, 0.0f, rotationHours);
        */
    }
 }
