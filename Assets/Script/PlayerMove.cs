using UnityEngine;
using System.Collections;

public class PlayerMove : MonoBehaviour {

	public Vector2 speed = new Vector2(50, 50);
	

	private Vector2 movement;
	
	void Update()
	{
		float inputX = Input.GetAxis("Horizontal");
		float inputY = Input.GetAxis("Vertical");

		movement = new Vector2(speed.x * inputX,speed.y * inputY);

		bool shoot = Input.GetButtonDown("Fire1");
		shoot |= Input.GetButtonDown("Fire2");

		if (shoot)
		{
			Weapon weapon = GetComponent<Weapon>();
			if (weapon != null)
			{
				weapon.Attack(false);
				SoundEffectsHelper.Instance.MakePlayerShotSound();
			}
		}
		var dist = (transform.position - Camera.main.transform.position).z;
		
		var leftBorder = Camera.main.ViewportToWorldPoint(
			new Vector3(0, 0, dist)
			).x;
		
		var rightBorder = Camera.main.ViewportToWorldPoint(
			new Vector3(1, 0, dist)
			).x;
		
		var topBorder = Camera.main.ViewportToWorldPoint(
			new Vector3(0, 0, dist)
			).y;
		
		var bottomBorder = Camera.main.ViewportToWorldPoint(
			new Vector3(0, 1, dist)
			).y;
		
		transform.position = new Vector3(
			Mathf.Clamp(transform.position.x, leftBorder, rightBorder),
			Mathf.Clamp(transform.position.y, topBorder, bottomBorder),
			transform.position.z
			);
	}
	
	void FixedUpdate()
	{
		GetComponent<Rigidbody2D>().velocity = movement;
	}
	void OnCollisionEnter2D(Collision2D collision)
	{
		bool damagePlayer = false;
		
		Enemy enemy = collision.gameObject.GetComponent<Enemy>();
		if (enemy != null)
		{
			Heath enemyHealth = enemy.GetComponent<Heath>();
			if (enemyHealth != null) enemyHealth.Damage(enemyHealth.hp);
			
			damagePlayer = true;
		}
		
		if (damagePlayer)
		{
			Heath playerHealth = this.GetComponent<Heath>();
			if (playerHealth != null) playerHealth.Damage(1);
		}
	}
}
