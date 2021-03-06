﻿using System.Collections;
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

        SpawnBouncables(0.0f);
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
        if (randint < 20)
        {
            //normal
            return BounceablePrefabs[0];
        }
        else if (randint < 40)
        {
            //normal
            return BounceablePrefabs[1];
        }
        else if (randint < 60)
        {
            //suffragette
            return BounceablePrefabs[2];
        }
        else if (randint < 80)
        {
            //obstacle
            return BounceablePrefabs[3];
        }
        else if (randint < 100)
        {
            //obstacle
            return BounceablePrefabs[4];
        }
        return BounceablePrefabs[randint];
    }
}