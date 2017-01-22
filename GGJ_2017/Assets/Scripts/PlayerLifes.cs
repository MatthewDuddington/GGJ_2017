using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerLifes : MonoBehaviour {

    public int lives = 5; 

    public void loseLife()
    {
        lives--; 
    }

    public int getLife()
    {
        return lives;
    }
}
