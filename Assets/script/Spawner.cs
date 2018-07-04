using UnityEngine;
using System.Collections;

public class Spawner : MonoBehaviour
{
    public float spawnTime = 5f;        // The amount of time between each spawn.
    public float spawnDelay = 3f;       // The amount of time before spawning starts.
    public GameObject[] enemies;        // Array of enemy prefabs.
    public int[] emptysite;             // Array of sites with 0 and 1 indication: 1 means site has no enemies
    public Transform[] spawnsites;      // Array of potential sites to create enemies


    void Start()
    {
        // Start calling the Spawn function repeatedly after a delay .
        //InvokeRepeating("Spawn", spawnDelay, spawnTime);
		//InvokeRepeating ("Spawn", spawnTime, spawnTime);
        
    }

    public int getlen(int[] arr) {//return number of empty sites
        int count = 0;
        for (int i = 0; i < arr.Length; i++)
        {
            if (arr[i] == 1) count ++;

        }
        return count;
    }
    public int getsite(int[] arr, int count) {//return the countTH empty sites in arr
        for (int i = 0; i < arr.Length; i++) {
            if (count == 0 && arr[i] == 1) return i;
            if (arr[i] == 1) count--;
         
        }
        return 0;//return 0 for code paths requirement
    }

    public void Spawn(int destroysite)
    {
        // Instantiate a random enemy.
        if (getlen(emptysite) == 0) return;
        int enemyIndex = Random.Range(0, enemies.Length);
        int site = Random.Range(0, getlen(emptysite));
        Debug.Log("site: " + site.ToString());
        int spawnnumber = getsite(emptysite, site);
        Debug.Log("spawnnumber:"+spawnnumber.ToString());

        GameObject clone = Instantiate(enemies[enemyIndex], spawnsites[spawnnumber].position, spawnsites[spawnnumber].rotation);
        clone.GetComponent<enermy>().create_site(spawnnumber); //record the enemy created position

        // Play the spawning effect from all of the particle systems.
        foreach (ParticleSystem p in GetComponentsInChildren<ParticleSystem>())
        {
            p.Play();
        }
        emptysite[spawnnumber] = 0;
        emptysite[destroysite] = 1;
        for (int i = 0; i < emptysite.Length; i++)
        {
            Debug.Log("Number:" + i.ToString() +"   "+ emptysite[i].ToString());
        }

    }
}
