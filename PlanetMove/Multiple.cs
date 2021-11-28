using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Multiple : MonoBehaviour
{
    public static float multipleTime;

    [SerializeField]
    private GameObject slider;

    // Start is called before the first frame update
    void Start()
    {
        multipleTime = slider.GetComponent<Slider>().value;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void slideValueChange()
    {
        multipleTime = slider.GetComponent<Slider>().value;
    }
}
