using UnityEngine;
using System.Collections;

public class EnemySpawner : MonoBehaviour
{
    public int hazardCount;
    public float spawnWait;
    public float startWait;
    public float waveWait;

    void Start ()
    {      
        StartCoroutine (SpawnWaves ());
    }

    IEnumerator SpawnWaves ()
    {
        yield return new WaitForSeconds (startWait);
        while (true)
        {
            for (int i = 0; i < hazardCount; i++)
            {
                
                Vector3 spawnPosition = new Vector3 (Random.Range (10, Screen.width-10), Random.Range(Screen.height*2 ,Screen.height*3) + Player.player.transform.position.y+5, -1);
                Quaternion spawnRotation = Quaternion.identity;

                spawnPosition = Camera.main.ScreenToWorldPoint(spawnPosition);
                spawnPosition.z = -1;
               
                GameObject obj = ObjectPool.current.GetEnemy(ObjectPool.enemyTypes.medium);

                if (obj == null)
                    return false;

                obj.transform.position = spawnPosition;
                obj.transform.rotation = spawnRotation;
                obj.SetActive(true);
                //Instantiate (enemy, spawnPosition, spawnRotation);
                yield return new WaitForSeconds (spawnWait);
            }
            yield return new WaitForSeconds (waveWait);
        }
    }
}