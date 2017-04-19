using UnityEngine;
using System.Collections;

/* This script controls the radar feature. This detects any gems in the vicinity of the player. Once the player gets close to a gem, the radar's graphic changes
 * to represent how close the player is to the gem. 
 */
using System;

public class Radar : MonoBehaviour {

	public Sprite[] iconStage;		//The image that represents how the radar is shown
	public bool canSenseGem;		//Is the radar sensing anything?
	public bool onGem;				//Is the radar directly over a gem?
	public GameObject gemSpawner;	//The gameobject that represents the GemSpawner
	private GameObject gemSpot;		//The gameobject that represents the current gemSpot the radar's looking for

	//When the game starts, the radar immediatly finds a spot to hunt down
	void Start()
	{
		FindSpot();
	}

	//By calculating the disitance from said spot to the player, this "tracks" down the gem spots
	void Update () 
	{
		if(canSenseGem == true)
		{
			float disitance = Math.Abs(Vector3.Distance(this.gameObject.transform.parent.transform.position, gemSpot.transform.position));
			if(onGem == true)
				this.gameObject.GetComponent<SpriteRenderer>().sprite = iconStage[3];
			else if(disitance < 5f && disitance > 3f)
				this.gameObject.GetComponent<SpriteRenderer>().sprite = iconStage[1];
			else if(disitance < 3f && disitance > 1f)
				this.gameObject.GetComponent<SpriteRenderer>().sprite = iconStage[2];
			else
				this.gameObject.GetComponent<SpriteRenderer>().sprite = iconStage[0];
		}
		else
		{
			if(gemSpawner.GetComponent<GemSpawner>().numbOfGemsOnField != 0)
				FindSpot();
		}
	}

	//This resets the radar back to normal.
	public void ResetRadar()
	{
		if(canSenseGem == true)
			canSenseGem = false;
	}

	//This finds a spot on the field that has a gem in it.
	void FindSpot()
	{
		for(int i = 0; i < gemSpawner.GetComponent<GemSpawner>().gemLocations.Length; i++)
		{
			if(gemSpawner.GetComponent<GemSpawner>().gemLocations[i].GetComponent<GemLocation>().hasGem == true)
			{
				canSenseGem = true;
				gemSpot = gemSpawner.GetComponent<GemSpawner>().gemLocations[i];
				break;
			}
		}
	}
}
