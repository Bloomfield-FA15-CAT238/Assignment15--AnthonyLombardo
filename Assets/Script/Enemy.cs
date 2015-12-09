using UnityEngine;

public class Enemy : MonoBehaviour {
	private bool hasSpawn;
	private Movemment moveScript;
	private Weapon[] weapons;
	
	void Awake()
	{
		// Retrieve the weapon only once
		weapons = GetComponentsInChildren<Weapon>();
		
		// Retrieve scripts to disable when not spawn
		moveScript = GetComponent<Movemment>();
	}
	
	// 1 - Disable everything
	void Start()
	{
		hasSpawn = false;
		
		// Disable everything
		// -- collider
		GetComponent<Collider2D>().enabled = false;
		// -- Moving
		moveScript.enabled = false;
		// -- Shooting
		foreach (Weapon weapon in weapons)
		{
			weapon.enabled = false;
		}
	}
	
	void Update()
	{
		// 2 - Check if the enemy has spawned.
		if (hasSpawn == false)
		{
			if (GetComponent<Renderer>().IsVisibleFrom(Camera.main))
			{
				Spawn();
			}
		}
		else
		{
			// Auto-fire
			foreach (Weapon weapon in weapons)
			{
				if (weapon != null && weapon.enabled && weapon.CanAttack)
				{
					weapon.Attack(true);
					SoundEffectsHelper.Instance.MakeEnemyShotSound();
				}
			}
			
			// 4 - Out of the camera ? Destroy the game object.
			if (GetComponent<Renderer>().IsVisibleFrom(Camera.main) == false)
			{
				Destroy(gameObject);
			}
		}
	}
	
	// 3 - Activate itself.
	private void Spawn()
	{
		hasSpawn = true;
		
		// Enable everything
		// -- Collider
		GetComponent<Collider2D>().enabled = true;
		// -- Moving
		moveScript.enabled = true;
		// -- Shooting
		foreach (Weapon weapon in weapons)
		{
			weapon.enabled = true;
		}
	}
}
