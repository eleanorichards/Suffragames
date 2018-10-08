using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bouncables : MonoBehaviour
{
    //public List<GameObject> BounceableObjects = new List<GameObject> ();
    public GameObject[] BounceablePrefabs;

    private int randint;
    private int seed = 0;
    public int noToSpawn = 20;

    // Use this for initialization
    private void Start()
    {
        //seed = (int)(Time.time*10.0f);
        //Random.InitState(seed);
        SpawnBouncables(50.0f);
    }

    // Update is called once per frame
    private void Update()
    {
        //get rock position
        //if rock position > threshold amount
        //spawn bouncables for threshold until 300m distance

        //for each bouncable
        //move between rand(xMin, xmax)
    }

    public void SpawnBouncables(float xPos)
    {
        seed = (int)System.DateTime.Now.Ticks;
        Random.InitState(seed);

        //evolve this in to object pooling
        for (int i = 0; i < noToSpawn; i++)
        {
            Instantiate(ReturnObject(), new Vector3(xPos + Random.Range(0, 1000), -31.0f, 0), transform.rotation);
        }
    }

    private GameObject ReturnObject()
    {
        randint = Random.Range(0, 100);
        if (randint < 25)
        {
            //normal
            return BounceablePrefabs[0];
        }
        else if (randint < 50)
        {
            //normal
            return BounceablePrefabs[1];
        }
        else if (randint < 75)
        {
            //suffragette
            return BounceablePrefabs[2];
        }
        else if (randint < 100)
        {
            //obstacle
            return BounceablePrefabs[3];
        }
        return BounceablePrefabs[randint];
    }
}