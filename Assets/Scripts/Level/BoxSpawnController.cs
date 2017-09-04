using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoxSpawnController : MonoBehaviour
{
    //Initialize variables
    Transform parent;
    Transform[] array = new Transform[10];
    int counter = 1;
    int counterMax = 200;
    int spawnerCount;
    GameObject box;
    public int lastBox = -1;

    // Use this for initialization
    void Start()
    {
        //Load resources
        box = (GameObject)Resources.Load("Weapon Box");
        //Find children spawnwers
        parent = GetComponent<Transform>();
        int i = 0;
        while (true)
        {
            string num = i.ToString();
            if (parent.Find(num) != null)
            {
                Transform child = parent.Find(num).GetComponent<Transform>();
                array[i] = child.transform;
            }
            else
            {
                spawnerCount = i;
                break;
            }
            i++;
        }
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        //If no box exists, start counter
        if (GameObject.Find("Weapon Box(Clone)") == null)
        {
            counter--;
        }

        //Spawn box
        if (counter == 0)
        {
            counter = counterMax;
            SpawnBox(spawnerCount);
        }
    }

    void SpawnBox(int i)
    {
        //Randomly pick a spawn location and spawn
        int choice = Random.Range(0, i);
        GameObject boxInstance = Instantiate(box);
        boxInstance.transform.position = array[choice].position;
    }
}
