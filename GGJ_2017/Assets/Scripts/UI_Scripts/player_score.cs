using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class get_player_score : MonoBehaviour {

    public float player1_score = 0.1f;

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
    txtRef.text = "Player 1: " + player1_score;
	}
}
