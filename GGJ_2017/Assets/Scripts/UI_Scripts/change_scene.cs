using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class change_scene : MonoBehaviour {
    public void set_scene_ID(int ID)
    {
        SceneManager.LoadScene(ID, LoadSceneMode.Single);
    }
}
