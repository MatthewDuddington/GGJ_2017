using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.SceneManagement;

public class respawn_collider : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {

	}

    private void OnTriggerEnter(Collider collision)
    {
        if (collision.GetComponent<PlayerStats>() != null)
        {
            collision.GetComponent<PlayerStats>().loseLife();
            if (collision.GetComponent<PlayerStats>().getLife() == 0)
            {
                // Change game mode to End Game
                SceneManager.LoadScene(4, LoadSceneMode.Single);
            }  
        }
           
        
    }
}
