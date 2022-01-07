using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class coin : MonoBehaviour
{
    [SerializeField] private int count;
    
    GameObject director;
    private void Start()
    {
        director = GameObject.Find("Director");
        //count = 0;
        
    }
    private void OnTriggerEnter(Collider other)
    {

        if (other.CompareTag("Player"))
        {
            director.GetComponent<Director>().itemAdministrator(1);
            // Director.itemAdministrator(1);
            // count = count + 1;
            Destroy(this.gameObject);

        }
        // Debug.Log(count);
    }

}
