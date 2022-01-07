using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

using Random = UnityEngine.Random;


public class lightScript : MonoBehaviour
{
    
    [SerializeField] GameObject light;
    //[SerializeField] bool light_on = false;
    //[SerializeField] bool lamp_on = false;
    //[SerializeField] bool lamp_on = false;
    //public Slider slider;
    //public float count = 30f; //420f;//8分
    private void Start()
    {
        
    }
    void Update()
    {
        lightActive();
    }
    public void lightActive()
    {
        if (Input.GetMouseButtonDown(1))
        {
            light.SetActive(!light.activeSelf);
        }
        // light.SetActive(Input.GetMouseButton(1));
    }
}
//if (Input.GetMouseButtonDown(1))//(Input.GetMouseButtonDown(1))//このアクションを起こすとスライダーを表示、非表示切り替えたい
//{
    //light.SetActive(true);
    //if (light.activeSelf)// && slider.value > 0)
    //{
    //slider.value -= 0.01f * Time.deltaTime;

    //lamp.SetActive(false);
    //count = 100f;
    //}
    //else (light.activeSelf)
    //{
    //    light.SetActive(false);
    //    //lamp.SetActive(true);
    //}
//}
//if (Input.GetKey(KeyCode.X))//(Input.GetMouseButtonDown(1))//このアクションを起こすとスライダーを表示、非表示切り替えたい
//{
//    if (lamp.activeSelf) //&& slider.value > 0)
//    {
//        lamp.SetActive(true);
//    }
//    else
//    {
//        lamp.SetActive(false);
//    }
//}