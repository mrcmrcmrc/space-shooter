using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ObjectPool : MonoBehaviour {

    public static ObjectPool current;

    public GameObject playerBulletPrefab;
    public GameObject enemyBulletPrefab;
    public GameObject[] enemiesPrefab;
   
    public int playerBulletAmount = 20;
    public int enemyBulletAmount = 10;
    public int enemiesAmount = 5; //for each one

    public bool willGrow = true;

    private List<GameObject> playerBulletPools;
    private List<GameObject> enemyBulletPools;
    private List<List<GameObject>> enemiesPools;

    public enum enemyTypes { easy, medium, hard };
   
	// Use this for initialization
	void Awake ()
    {
        if (current == null)
            current = this;

        else if(current != this)
            Destroy(gameObject);

        playerBulletPools = new List<GameObject>();
        enemyBulletPools = new List<GameObject>();
        enemiesPools = new List<List<GameObject>>();

	}

    void Start ()
    {
        for (int i = 0; i < playerBulletAmount; i++)
        {
            playerBulletPools.Add(CreatePlayerBullet());
        }

        for (int i = 0; i< enemyBulletAmount; i++)
        {
            enemyBulletPools.Add(CreateEnemyBullet());
        }

        List<GameObject> e = new List<GameObject>();
        enemyTypes t = enemyTypes.easy;

        for(int i = 0; i < enemiesPrefab.Length; i++)
        {
            for(int j = 0; j < enemiesAmount; j++)
            {
                e.Add(CreateEnemy(t));
            }
            List<GameObject> temp = new List<GameObject>(e);
            enemiesPools.Add(temp);
            t++;
            e.Clear();
        }

            
    }

    public GameObject GetPlayerBullet ()
    {
        for (int i = 0; i < playerBulletPools.Count; i++)
        {
            if (!playerBulletPools[i].activeInHierarchy)
            {
                return playerBulletPools[i];
            }
        }

        if(willGrow)
        {
            GameObject obj = CreatePlayerBullet();
            playerBulletPools.Add(obj);
            return obj;
        }

        return null;
    }
        
    public GameObject GetEnemyBullet ()
    {
        for (int i = 0; i < enemyBulletPools.Count; i++)
        {
            if (!enemyBulletPools[i].activeInHierarchy)
            {
                return enemyBulletPools[i];
            }
        }

        if(willGrow)
        {
            GameObject obj = CreateEnemyBullet();
            enemyBulletPools.Add(obj);
            return obj;
        }

        return null;
    }

    public GameObject GetEnemy (enemyTypes t)
    {
        int n = 0;
        switch (t)
        {
            case enemyTypes.easy:
                n = 0;
                break;
            case enemyTypes.medium:
                n = 1;
                break;
            case enemyTypes.hard:
                n = 2;
                break;
        }

        for(int i = 0; i < enemiesPools[n].Count; i++)
        {
            if (!enemiesPools[n][i].activeInHierarchy)
            {
                return enemiesPools[n][i];
            }
           
        }
          
        if (willGrow)
        {
            GameObject obj = CreateEnemy(t);
            enemiesPools[n].Add(obj);
            return obj;
        }
        return null;
    }

    public GameObject CreatePlayerBullet ()
    {
        GameObject obj = (GameObject) Instantiate(playerBulletPrefab);
        obj.SetActive(false);
        return obj;
    }

    public GameObject CreateEnemyBullet ()
    {
        GameObject obj = (GameObject) Instantiate(enemyBulletPrefab);
        obj.SetActive(false);
        return obj;
    }

    public GameObject CreateEnemy (enemyTypes t)
    {
        int i = 0;
        switch (t)
        {
            case enemyTypes.easy:
                i = 0;
                break;
            case enemyTypes.medium:
                i = 1;
                break;
            case enemyTypes.hard:
                i = 2;
                break;
        }

        GameObject obj = (GameObject) Instantiate(enemiesPrefab[i]);
        obj.SetActive(false);
        return obj;
    }
              
}
