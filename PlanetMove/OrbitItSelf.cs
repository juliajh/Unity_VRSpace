using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OrbitItSelf : MonoBehaviour
{
    public Transform orbitingObject;


    private float nextTime = 0.0f;
    public float rotateAngle=0;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Time.time*1000 >= nextTime)
        {
            nextTime = Time.time*1000 + 0.01f;
            orbitItself();
        }
    }

    public void orbitItself() //자전 
    {
        if (orbitingObject != null)
        {
            if (this.name == "Mercury")
                orbitingObject.Rotate (-this.transform.up, rotateAngle * 0.01f* Multiple.multipleTime);
            else if (this.name == "Venus")
                orbitingObject.Rotate(this.transform.up, rotateAngle * 0.01f * Multiple.multipleTime);
            else if (this.name == "Earth")
                orbitingObject.Rotate(-this.transform.up, rotateAngle * 0.01f * Multiple.multipleTime);
            else if (this.name == "Mars")
                orbitingObject.Rotate(-this.transform.up, rotateAngle * 0.01f * Multiple.multipleTime);
            else if (this.name == "Jupiter")
                orbitingObject.Rotate(-this.transform.up, rotateAngle * 0.01f * Multiple.multipleTime);
            else if (this.name == "Saturn")
                orbitingObject.Rotate(-this.transform.up, rotateAngle * 0.01f * Multiple.multipleTime);
            else if (this.name == "Uranus")
                orbitingObject.Rotate(this.transform.up, rotateAngle * 0.01f * Multiple.multipleTime);
            else if (this.name == "Neptune")
                orbitingObject.Rotate(-this.transform.up, rotateAngle * 0.01f * Multiple.multipleTime);
            
            //위성
            else if (this.name == "Moon")
                orbitingObject.Rotate(-this.transform.up, rotateAngle * 0.01f * Multiple.multipleTime);
            else if (this.name == "Phobos")
                orbitingObject.Rotate(-this.transform.up, rotateAngle * 0.01f * Multiple.multipleTime);
            else if (this.name == "Deimos")
                orbitingObject.Rotate(-this.transform.up, rotateAngle * 0.01f * Multiple.multipleTime);
            else if (this.name == "Io")
                orbitingObject.Rotate(-this.transform.up, rotateAngle * 0.01f * Multiple.multipleTime);
            else if (this.name == "Europa")
                orbitingObject.Rotate(-this.transform.up, rotateAngle * 0.01f * Multiple.multipleTime);

        }
    }
}
