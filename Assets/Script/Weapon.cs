 	using UnityEngine;
using System.Collections;

public class Weapon : MonoBehaviour {

	public Transform shotPrefab;
	
	public float shootingRate = 0.25f;

	private float shootCooldown;
	
	void Start()
	{
		shootCooldown = 0f;
	}
	
	void Update()
	{
		if (shootCooldown > 0)
		{
			shootCooldown -= Time.deltaTime;
		}
	}

	public void Attack(bool isEnemy)
	{
		if (CanAttack)
		{
			shootCooldown = shootingRate;
			
			var shotTransform = Instantiate(shotPrefab) as Transform;
			shotTransform.position = transform.position;
			
			Shot shot = shotTransform.gameObject.GetComponent<Shot>();
			if (shot != null)
			{
				shot.isEnemyShot = isEnemy;
			}
			
			Movemment move = shotTransform.gameObject.GetComponent<Movemment>();
			if (move != null)
			{
				move.direction = this.transform.right; 
			}
		}
	}
	public bool CanAttack
	{
		get
		{
			return shootCooldown <= 0f;
		}
	}
}
