using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Scrolling : MonoBehaviour {

	private int tilesOnScreen = 3;
	public GameObject[] tilePrefabs;
	private Transform player;
	private float spawnZ = 0.0f;
	private float tileLength = 16.0f;
	public float safeZone = 10.0f;
    private int rnd;

	private List<GameObject> activeTiles;


	// Use this for initialization
	void Start () {

		activeTiles = new List<GameObject>();
		player = GameObject.FindGameObjectWithTag ("player").transform;

		for (int i = 0; i < tilesOnScreen; i++) {
			SpawnTile ();
		}
	}
	
	// Update is called once per frame
	void Update () {
		if(player.position.y - safeZone > ( spawnZ - tilesOnScreen*tileLength))
        {
            rnd = Random.Range(0, tilePrefabs.Length);
			SpawnTile (rnd);
			DeleteTile ();
		}
	}

    void SpawnTile(int index = 0)
	{
		GameObject go;
		go = Instantiate (tilePrefabs [index]) as GameObject;
		go.transform.SetParent (transform);
        go.transform.position = new Vector3(0 , 1 * spawnZ, 1);
		spawnZ += tileLength;
		activeTiles.Add (go);
	}

	void DeleteTile()
	{
		Destroy (activeTiles [0]);
		activeTiles.RemoveAt (0);
	}
}
