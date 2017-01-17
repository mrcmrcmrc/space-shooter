using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Enemy : MonoBehaviour {

    public enum enemyType
    {
        easy,
        medium,
        hard,
        boss
    };

    public enemyType eType;
	public float cooldown = 0.6f;
    private float timer = 0.0f;	
    public GameObject bullet;
    public float hp = 5f;
    public GameObject explosion;
    private bool enemyDead;
    public float enemyMoveSpeed = 1.2f;
    private float fullHP;

    void Awake ()
    {
        fullHP = hp;
    
    }

    void OnEnable()
    {
        timer = cooldown;
        enemyDead = false;
        hp = fullHP;

    }

	void Update () {

        if (!enemyDead)
        {
            timer += Time.deltaTime;

            if (timer >= cooldown && isEnemyOnScreen() )
            {
                Fire();
                timer = 0.0f;
            }

            if (hp <= 0 )
            {
                Die();
            }

            if (transform.position.y < Player.player.transform.position.y - 2)
                Die(false);
        }

      

	}

    void FixedUpdate()
    {
       // player = GameObject.FindWithTag("player").transform;
        if(isEnemyOnScreen())
            transform.position = Vector3.MoveTowards(transform.position, Player.player.transform.position, enemyMoveSpeed * Time.deltaTime);
       
       // Debug.Log(player.transform.position);
      //  transform.Translate(Vector2.down * 0.125f);
       // Debug.Log(transform.position + "p=" + player.transform.position);
    }


//    IEnumerator Start()
//	{
//        
//        timer = cooldown;
//		var pointA = transform.position;
//
//		Vector3 pointB;
//
//	 	pointB = new Vector3 (pointA.x + 0.12f, pointA.y,-1);
//
//		while(true) {
//			yield return StartCoroutine(MoveObject(transform, pointA, pointB, 0.6f));
//			yield return StartCoroutine(MoveObject(transform, pointB, pointA, 0.6f));
//		}
//	}
//
//	//source: http://answers.unity3d.com/questions/14279/make-an-object-move-from-point-a-to-point-b-then-b.html
//	IEnumerator MoveObject(Transform thisTransform, Vector3 startPos, Vector3 endPos, float time)
//	{
//		var i= 0.0f;
//		var rate= 1.0f/time;
//		while (i < 1.0f) {
//			i += Time.deltaTime * rate;
//			thisTransform.position = Vector3.Lerp(startPos, endPos, i);
//			yield return null;
//		}
//	}

	private bool isEnemyOnScreen() // fire if enemy in field of view
	{
		Vector2 min = Camera.main.ViewportToWorldPoint (new Vector2 (0, 0));
		Vector2 max = Camera.main.ViewportToWorldPoint (new Vector2 (1, 1));

		if ((transform.position.x > min.x) && (transform.position.x < max.x) &&
		   (transform.position.y > min.y) && (transform.position.y < max.y)) {
			return true;
		}
		return false;
	}

	private void Fire()
	{

		Transform[] t = GetComponentsInChildren<Transform> ();

		//Vector3 theposition = t.TransformPoint (t.position);
        GameObject obj = ObjectPool.current.GetEnemyBullet();
        if (obj == null)
            return;
       
        obj.transform.position = t[1].transform.position;
        obj.transform.rotation = t[1].transform.rotation;
        obj.SetActive(true);

	}

    private void Die(bool anim = true)
    {     
        enemyDead = true;
        if (anim)
        {
            Instantiate(explosion, transform.position, transform.rotation);
            //sfx.Play();
        }
        gameObject.SetActive(false);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.CompareTag("player"))
            {
            Player.player.getDamage(5);
                //player.GetComponent<Player>().getDamage(5);
                Die();
            }
    }

   
        

}
