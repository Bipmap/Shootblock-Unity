using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawnController : MonoBehaviour
{

    //Initialize vairables
    BoxCollider2D bc2d;
    float width;
    float height;
    float minX;
    float maxX;
    float minY;
    float maxY;

    public int wave = 0;
    int eCurrent = 0;
    int eMax = 2;

    int timer = 30;

    float aimScalar = StaticSettings.enemyAimScalar;
    public float aimTimer;

    GameObject enemy;

    //Initialize state machine
    enum sStates
    {
        check,
        spawn
    }
    sStates state = sStates.check;

    // Use this for initialization
    void Start()
    {
        //Get component
        bc2d = GetComponent<BoxCollider2D>();
        //Find boundaries
        width = bc2d.bounds.extents.x;
        height = bc2d.bounds.extents.y;
        minX = transform.position.x - width;
        maxX = transform.position.x + width;
        minY = transform.position.y - height;
        maxY = transform.position.y + height;
        //Load resources
        enemy = (GameObject)Resources.Load("Inactive Enemy");
    }

    void FixedUpdate()
    {
        //Check if enemies are alive
        switch (state)
        {
            case sStates.check:
            {
                if (GameObject.Find("Enemy(Clone)") == null && GameObject.Find("Inactive Enemy(Clone)") == null)
                {
                    wave += 1;
                    aimTimer = Mathf.Min(wave * aimScalar, 8 * aimScalar);
                    state = sStates.spawn;
                }

                break;
            }

            case sStates.spawn:
            {
                if (eCurrent < eMax)
                {
                    timer--;
                    if (timer == 0)
                        SpawnEnemy();
                }
                else if (eCurrent == eMax)
                {
                    state = sStates.check;
                    eMax = Mathf.Min(eMax + 1, 8);
                    eCurrent = 0;
                }
                break;
            }
        }
    }

    void SpawnEnemy()
    {
        //Spawn an enemy within range
        GameObject enemyInstance = Instantiate(enemy);
        enemy.transform.position = new Vector2(Random.Range(minX, maxX), Random.Range(minY, maxY));
        eCurrent += 1;
        timer = 30;
    }
}
