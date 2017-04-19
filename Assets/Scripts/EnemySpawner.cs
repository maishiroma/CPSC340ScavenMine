using UnityEngine;
using System.Collections;

/*	This spawns the enemies by defining a set range and spawning said enemies within that range. This also has static variables that control how many enemies
 * 	can be on the screen.
 */

public class EnemySpawner : MonoBehaviour {

	public static int numbEnemiesOnScreen = 0;		//How many enemies are currently on screen?
	public static int currMaxEnemiesToSpawn = 2;	//The current limit on how many enemies could spawn
	public static int maxNumberOfEnemies = 5;		//How many enemies can the game currently spawn?
	public static float spawnEnemySpeed = 0.05f;	//How fast will the enemy move?
	public static float maxEnemySpeed = 0.09f;		//The maximum speed that the enemies in the game could move.

	public float lowerRange;					//Used to determine the lower bound of the range
	public float upperRange;					//Used to determine the upper bound of the range.
	public string spawnEnemyDirec;				//The direction that the enemies will spawn
	public GameObject vultureSpawn;				//This is going to be the enemy to spawn

	//Resets the static variables so that when the game is "reset", the game can still run
	void Start()
	{
		numbEnemiesOnScreen = 0;
		currMaxEnemiesToSpawn = 2;
		spawnEnemySpeed = 0.05f;	
	}
		
	//If there's no enemies on screen, this will randomly choose spots that will spawn the enemies.
	void Update () 
	{
		if(numbEnemiesOnScreen < currMaxEnemiesToSpawn && Random.Range(0,4) <= 2)
			SpawnEnemies();
	}

	//This spawns enemies in the given range. Depending on the spawnEnemyDirec, the enemies are placed in various places.
	void SpawnEnemies()
	{
		GameObject spawned;
		switch(spawnEnemyDirec)
		{
			case "Down":
				spawned = (GameObject)Instantiate(vultureSpawn, new Vector3(Random.Range(lowerRange,upperRange), this.gameObject.transform.position.y - 1.5f, 5), 
					vultureSpawn.transform.rotation);
				break;
			case "Up":
				spawned = (GameObject)Instantiate(vultureSpawn, new Vector3(Random.Range(lowerRange,upperRange), this.gameObject.transform.position.y + 1.5f, 5), 
					vultureSpawn.transform.rotation);
				break;
			case "Left":
				spawned = (GameObject)Instantiate(vultureSpawn, new Vector3(this.gameObject.transform.position.x - 2f, Random.Range(lowerRange,upperRange), 5), 
					vultureSpawn.transform.rotation);
				break;
			case "Right":
				spawned = (GameObject)Instantiate(vultureSpawn, new Vector3(this.gameObject.transform.position.x + 2f, Random.Range(lowerRange,upperRange), 5), 
					vultureSpawn.transform.rotation);
				break;
			default:
				spawned = (GameObject)Instantiate(vultureSpawn, new Vector3(this.gameObject.transform.position.x, this.gameObject.transform.position.y, 5), 
					vultureSpawn.transform.rotation);
				break;
		}
		spawned.GetComponent<EnemyMovement>().moveDir = spawnEnemyDirec;
		spawned.GetComponent<EnemyMovement>().moveSpeed = spawnEnemySpeed;
		numbEnemiesOnScreen++;
	}

	//When enemies run into here, the numb of enemies get decremented.
	void OnTriggerEnter(Collider other)
	{
		if(other.gameObject.tag == "Enemy")
			numbEnemiesOnScreen--;
	}

	//This function will increase the number of enemies that can spawn at max by one and slowly increase the speed of the enemies 
	public static void IncreaseDifficulty()
	{
		if(currMaxEnemiesToSpawn < maxNumberOfEnemies)
			currMaxEnemiesToSpawn++;
		if(spawnEnemySpeed < maxEnemySpeed)
			spawnEnemySpeed += 0.01f;
	}
}
