using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class get_player_score : MonoBehaviour {


    public GameObject player1;

    private Text txtRef;

    private void Awake()
    {

        txtRef = GetComponent<Text>();//or provide from somewhere else (e.g. if you want via find GameObject.Find("CountText").GetComponent<Text>();)
    }



    // Use this for initialization
    void Start() {

    }

    // Update is called once per frame
	void Update () {
        //then where you need:
        txtRef.text = "" + player1.GetComponent<PlayerStats>().get_score();
	}
}
