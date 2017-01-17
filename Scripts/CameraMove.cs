using UnityEngine;
using System.Collections;

public class CameraMove : MonoBehaviour {

	private Transform player;
	private Vector3 v;
	private Vector3 offset;
	// Use this for initialization
	void Start () {

		player = GameObject.FindGameObjectWithTag("player").transform;
		offset = transform.position - player.position;
	}
	
	// Update is called once per frame
	void Update () {
		v.x = 0;
		v.y = player.position.y + offset.y;
		v.z = -10;
		transform.position = v;
	}
}
