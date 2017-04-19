using UnityEngine;
using System.Collections;

/* This shows a potential location of the gem, and handles having a gem spawn on there. This works with the GemSpawner script.
 * 
 * First, the GemSpawner spawns X number of gems. This number can increase depending on how far the player is in. These gems have a timer on them, in which 
 * once the timer on each gem goes to 0, the gem dissapears. Once there's no more gems on the field, due to disappearing or the player collectign them all, more
 * gems will spawn.
 */

public class GemLocation : MonoBehaviour {
	
	public bool hasGem = false;			//Does this spot "have" a gem?
	public float timeActive;			//How long this spot has a gem
	public int timeGemDissapear;		//How long will it take for the gem to dissapear?

	//If the spot is active, this takes care of the timer for the gem to dissapear. 
	void Update () {
		if(hasGem == true)
		{
			timeActive += Time.deltaTime;
			if((int)timeActive == timeGemDissapear)
				ResetSpot();
		}
	}

	//This resets the spot to be empty
	void ResetSpot()
	{
		hasGem = false;
		timeActive = 0f;
		GameObject.Find("GemSpawner").GetComponent<GemSpawner>().numbOfGemsOnField--;
	}

	//This is called in the GemSpawner in order to activate this spot.
	public void ActivateGemSpot()
	{
		if(hasGem == false)
			hasGem = true;
	}

	//This is called when the player digs at this spot
	public void FoundGem(GameObject player)
	{
		ResetSpot();
		GameObject.Find("Radar").GetComponent<Radar>().ResetRadar();
		GemCounter.gemCounter++;
	}
}
