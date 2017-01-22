using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManagerScript : MonoBehaviour {

     public AudioClip[] clips;
     public AudioSource source;
	// Use this for initialization
	void Start () {
        print(GetComponents<AudioSource>().Length);
          source = GetComponents<AudioSource>()[1];
	}
	
     public void IronEquipedSound()
     {
          source.clip = clips[3];
          source.Play();
     }

     public void GoldPickUpSound()
     {
          source.clip = clips[2];
          source.Play();
     }

     public void BoatLandingSound()
     {
          source.clip = clips[4];
          source.Play();
     }

     public void WoodMetalCollisionSound()
     {
          source.clip = clips[5];
          source.Play();
     }

     public void WoodWoodCollisionSound()
     {
          source.clip = clips[6];
          source.Play();
     }


     // Update is called once per frame
     void Update () {
		
	}
}
