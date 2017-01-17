using UnityEngine;
using System.Collections;

public class enemyBulletMove : MonoBehaviour {

	public float speed = 0.05f;
	Vector3 direction;
	Vector3 targetPos;
	Vector3 d;

	// Use this for initialization
	void OnEnable () {
		
		targetPos = GameObject.FindGameObjectWithTag ("player").transform.position;

		d = targetPos - (Vector3)transform.position;
		SetDirection (d);
	}

	// Update is called once per frame
	void Update () {

//		Vector3 position = transform.position;
//		position += direction * speed * Time.deltaTime;
		//transform.position = position;
	
		//step = speed * Time.deltaTime;
		//transform.position = Vector2.Lerp (transform.position, targetPos, step);
		//rb.velocity= new Vector2(targetPos.x, -speed);
		
		//transform.position -= transform.forward*speed*Time.deltaTime;


	}
	void FixedUpdate(){
		//rb.AddRelativeForce (targetPos,ForceMode2D.Force);
        transform.Translate(d * speed * Time.deltaTime);
		//rb.AddForce(targetPos,ForceMode2D.Impulse);
	}

	public void SetDirection(Vector3 _direction)
	{
		direction = _direction.normalized;
	}

	void OnTriggerEnter2D(Collider2D other)
	{
		if(other.CompareTag("player"))
		{
            Player.player.getDamage(2f);
            gameObject.SetActive(false);
		}
	}


		
}
