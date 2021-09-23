using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Scene_move : MonoBehaviour
{
    public string scene_name;

     void OnCollisionEnter(Collision collision)
    {
        SceneManager.LoadScene(scene_name);
    }
}
