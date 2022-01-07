using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;
using UnityEngine.UI;

public class Director : MonoBehaviour{
    public int count = 0;
    public Text countText;
    

    void Update(){
        //itemAdministrator();
    }
    public void itemAdministrator(int i){
        count+=i;
        countText = GetComponentInChildren<Text>();
        Debug.Log(count);
    }
}