using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using CnControls;
using UnityEngine.UI;

public class Player : MonoBehaviour {

    public static Player player;
	private Vector2 movement;
	private Rigidbody2D rb;
	public float xSpeed = 3.5f;
    public float ySpeed;

	public float cd = 1.2f;
	public float autoMoveSpeed = 0.75f;
    public float playerHP = 20f;
    public GameObject damagePrefab;
    public Image damageImage;
    private bool damaged = false;
    public float flashSpeed = 5f;
    public Color flashColour = new Color(249,0,0,47);
    public float damageToMeteor = 33f;
    public enum BulletStyles {single, _double, triple} ; 
    public BulletStyles bulletstyle;
    private float lastTakenDamage;

    public bool isAutoFire = true;


    void Awake ()
    {
        if (player == null)
            player = this;
        else if (player != this)
            Destroy(gameObject);
    }
	
    void Start () 
    {
		rb = GetComponent<Rigidbody2D>();
        if (isAutoFire)
            StartCoroutine(AutoFire());    
	}

	void Update ()
    {
        if (damaged)
            {
            flashColour.a +=  lastTakenDamage/255;
            damageImage.color = flashColour;
            flashColour.a -= lastTakenDamage / 255;  
            }

        else
        {
            damageImage.color = Color.Lerp(damageImage.color, Color.clear, flashSpeed * Time.deltaTime);
        }
       
        damaged = false;

    	transform.Translate (Vector2.up * Time.deltaTime * autoMoveSpeed);

        if (SystemInfo.deviceType == DeviceType.Desktop)
        {
            if (!isAutoFire)
            if (Input.GetButtonDown("Fire1"))
                    Fire();
        }
            
	}

	// Update is called once per frame
	void FixedUpdate () {
		
        if (SystemInfo.deviceType == DeviceType.Desktop)
        {
            float inputX = Input.GetAxis("Horizontal");
            float inputY = Input.GetAxis("Vertical");
            rb.velocity = new Vector2(inputX * xSpeed, inputY * ySpeed);

        }
        else if(SystemInfo.deviceType == DeviceType.Handheld)
        {
            float inputX = CnInputManager.GetAxis("Horizontal");
            float inputY = CnInputManager.GetAxis("Vertical");
            rb.velocity = new Vector2(inputX * xSpeed, inputY * ySpeed);
        }
            
        //left-right teleport
        Vector2 min = Camera.main.ViewportToWorldPoint (new Vector2 (0, 0));
        Vector2 max = Camera.main.ViewportToWorldPoint (new Vector2 (1, 1));

        if (transform.position.x < min.x || transform.position.x > max.x)
        {
            float newX = Mathf.Clamp(transform.position.x, max.x, min.x);
            transform.position = new Vector3(newX, transform.position.y, transform.position.z);
        }
//		movement = new Vector3(inputX,inputY,transform.position.z);
//		rb.AddTorque(torque, ForceMode2D.Impulse);
//		rb.AddForce (movement * speed);
//		transform.Rotate (Vector3.forward * Time.deltaTime,Space.World);

	}

    IEnumerator AutoFire() 
    {
        
        while (true)
        {
            int l = 0;
            if (bulletstyle == BulletStyles.single)
                l = 1;
            else if (bulletstyle == BulletStyles._double)
                l = 3;
            else
                l = 5;
            Transform[] myComps = GetComponentsInChildren<Transform>();

            for (int i = 0; i < l+1; i++)
            {
                if (myComps[i].gameObject.name != this.gameObject.name)
                {
                    GameObject obj = ObjectPool.current.GetPlayerBullet();
                    if (obj == null)
                        return false;

                    //obj.transform.position = myComps[i].position + 1.0f * transform.forward;
                    obj.transform.position = myComps[i].position;
                    obj.transform.rotation = myComps[i].rotation;
                    obj.SetActive(true);
                 
                }
            }
           
            yield return new WaitForSeconds(cd);
        }
    }

    private void Fire ()
    {
        int l = 0;
        if (bulletstyle == BulletStyles.single)
            l = 1;
        else if (bulletstyle == BulletStyles._double)
            l = 3;
        else
            l = 5;
        Transform[] myComps = GetComponentsInChildren<Transform>();

        for (int i = 0; i < l+1; i++)
        {
            if (myComps[i].gameObject.name != this.gameObject.name)
            {
                GameObject obj = ObjectPool.current.GetPlayerBullet();
                if (obj == null)
                    return;

                //obj.transform.position = myComps[i].position + 1.0f * transform.forward;
                obj.transform.position = myComps[i].position;
                obj.transform.rotation = myComps[i].rotation;
                obj.SetActive(true);
            }
        }       
    }

    public void getDamage(float damage)
    {   
        lastTakenDamage = damage;
        playerHP -= damage;
        damaged = true;

        if (playerHP <= 0)
        {
           // Destroy(gameObject);
            Instantiate(damagePrefab, transform.position, transform.rotation);
        }

        else
        {
            Instantiate(damagePrefab, transform.position, transform.rotation);
        }

    }

    void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("meteor"))
        {
            other.gameObject.GetComponent<MeteorFall>().getDamage(damageToMeteor);
            StartCoroutine(ClearForce());
           
        }
    }

    IEnumerator ClearForce()
    {
        yield return new WaitForSecondsRealtime(1.7f);
        rb.velocity = Vector3.zero;
        rb.angularVelocity = 0.0f;
        transform.rotation = Quaternion.Euler(new Vector3(0f,0f,0f));
    }

        
}
