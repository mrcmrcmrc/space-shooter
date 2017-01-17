using UnityEngine;
using System.Collections;

public class laserMove : MonoBehaviour {
	public float speed = 0.025f;
    public float laserDamage;

	// Use this for initialization
	void Start () {
		
	}

	void Update(){

	}
	
	// Update is called once per frame
	void FixedUpdate () {
        transform.Translate (Vector2.up * speed * Time.deltaTime );

	}

	void OnTriggerEnter2D(Collider2D other)
	{
		if(other.CompareTag("enemy"))
		{
            other.gameObject.GetComponent<Enemy>().hp -= laserDamage;
            gameObject.SetActive(false);
		}

        else if(other.CompareTag("meteor"))
            {
            other.gameObject.GetComponent<MeteorFall>().getDamage(2f);
            gameObject.SetActive(false);
            }
            
	}
       

}
