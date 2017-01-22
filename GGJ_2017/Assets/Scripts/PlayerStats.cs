using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : MonoBehaviour {

    public int lives = 5;
    public int coins_Score = 0;

    public GameObject[] player_skulls; 

    public void pickup_coin()
    {
        coins_Score++;
    }
    public int get_score()
    {
        return coins_Score;
    }


    public void subtract_coins(int amount)
    {
        coins_Score -= amount;
    }


    public void loseLife()
    {
   
        lives--;
        player_skulls[((lives - 1) / 2) + 1].SetActive(false);
    }

    public int getLife()
    {
        return lives;
    }
}
