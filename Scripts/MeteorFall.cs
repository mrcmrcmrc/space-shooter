using UnityEngine;
using System.Collections;

public class MeteorFall : MonoBehaviour {

    public GameObject meteorExplotion;
    private Rigidbody2D rb;
    public float meteorHP = 5f;
    public float meteorDamage = 10f;
   
    private AudioSource sfx;
    bool canPlay = true;

    public float fallingSpeed = 0.6f;



	// Use this for initialization
	void Start () {
        rb = GetComponent<Rigidbody2D>();
        sfx = GetComponent<AudioSource>();

	}
	
	// Update is called once per frame
	void Update () {
        

        if ((transform.position.y - Player.player.transform.position.y) < 16)
        {
            rb.gravityScale = fallingSpeed;
            if (canPlay)
            {
                sfx.Play();
                canPlay = false;
            }
        }


        Vector2 min = Camera.main.ViewportToWorldPoint(new Vector2(0, 0));
       
        if (transform.position.y < min.y)
        {
                Destroy(gameObject);
        }
	
	}

    public void getDamage(float damage)
    {
        meteorHP -= damage;
        if (meteorHP <= 0)
        {
            Instantiate(meteorExplotion, transform.position, transform.rotation);
            Die();
        }

    }

    public void Die ()
    {
        
        Destroy(gameObject);
        
    }


    void OnCollisionEnter2D(Collision2D other)
    {
        if(other.gameObject.CompareTag("player"))
            {
                Player.player.getDamage(meteorDamage);
            }
    }
        
}
