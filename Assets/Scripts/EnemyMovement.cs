using UnityEngine;
using System.Collections;

/*	This is the script for controlling the vultures. They spawn on the edges of the screen and move in a straight line. Once they go off the screen, the vulture
 *  dissapears. There can be X number of vultures on screen, which slowly increases as the game goes on longer. If the player hits a vulture, they take damage.
 * They also slowly speed up as the game increases. If two vultures happen to hit each other, they reverse their direction
 */

public class EnemyMovement : MonoBehaviour {

	public float moveSpeed;		//How fast the enemy moves.
	public string moveDir;		//What direction is the enemy moving?
	
	//This is going to control the direction of the vulture.
	void Update () 
	{
		Vector3 newPos;
		switch(moveDir)
		{
			case "Down":
				newPos = new Vector2(0,moveSpeed * -1);
				break;
			case "Up":
				newPos = new Vector2(0,moveSpeed);
				break;
			case "Left":
				newPos = new Vector2(moveSpeed * -1, 0);
				break;
			case "Right":
				newPos = new Vector2(moveSpeed, 0);
				break;
			default:
				newPos = new Vector2(0,0);
				break;
		}
		this.gameObject.transform.position += newPos;
	}

	//If the enemy hits the edge of the screen, they get destroyed. If they hit another enemy, they both switch directions.
	void OnTriggerEnter(Collider other)
	{
		if(other.gameObject.tag == "Wall")
			Destroy(this.gameObject);
		else if(other.gameObject.tag == "Enemy")
			ChangeDirection();
	}

	//This is going to change the direction of the enemy to be the opposite
	void ChangeDirection()
	{
		switch(moveDir)
		{
			case "Down":
				moveDir = "Up";
				break;
			case "Up":
				moveDir = "Down";
				break;
			case "Left":
				moveDir = "Right";
				break;
			case "Right":
				moveDir = "Left";
				break;
		}
	}
}
