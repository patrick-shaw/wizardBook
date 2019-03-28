using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cyclops : MonoBehaviour {

    [Header("MOVEMENT")]
    public float speed;
    public float stoppingDistance;
    public float retreatDistance;

    [Header("LASER FIRE")]
    public float timeBetweenShots;
    public float startTimeBetweenShots;

    [Header("STONE THROW")]
    public float minTimeBetweenThrows = 0.2f;
    public float maxTimeBetweenThrows = 3f;
    public float throwRockTime = 2f;

    float throwCounter;
    bool facingRight;
    bool canMove = true;

    public GameObject laser;
    public GameObject stone;
    public Transform eyePos;
    Transform playerTarget;
    Vector2 headPosLeft;
    Vector2 headPosRight;

    Animator cyclopsAnim;

	// Use this for initialization
	void Start () {
        playerTarget = GameObject.FindGameObjectWithTag("Player").transform;
        
        timeBetweenShots = startTimeBetweenShots;

        throwCounter = Random.Range(minTimeBetweenThrows, maxTimeBetweenThrows);

        cyclopsAnim = GetComponent<Animator>();
	}
	
	// Update is called once per frame
	void Update ()
    {
        LaserHeadPos();
        if (!canMove)
        {
            return;
        }
        else
        {
            Movement();
        }

        LaserShot();

        //CountDownAndThrow();
    }

    private void CountDownAndThrow()
    {
        throwCounter -= Time.deltaTime;
        if(throwCounter <= 0)
        {
            canMove = false;
            cyclopsAnim.SetTrigger("Rock Throw");
            StartCoroutine(RockThrow());
            throwCounter = Random.Range(minTimeBetweenThrows, maxTimeBetweenThrows);
        }
    }

    IEnumerator RockThrow()
    {
        yield return new WaitForSeconds(throwRockTime);
        print("rick thrown");
        Instantiate(stone, transform.position, Quaternion.identity);
        canMove = true;

    }

    private void LaserHeadPos()
    {
        headPosLeft = new Vector2(transform.position.x, transform.position.y + 0.25f);
        headPosRight = new Vector2(transform.position.x + 1f, transform.position.y + 0.25f);
    }

    private void LaserShot()
    {
        if (timeBetweenShots <= 0)
        {
            Instantiate(laser, eyePos.position, Quaternion.identity);
            timeBetweenShots = startTimeBetweenShots;
       /*     if (facingRight == false)
            {
                Instantiate(laser, headPosLeft, Quaternion.identity);
                timeBetweenShots = startTimeBetweenShots;
            }
            else
            {
                Instantiate(laser, headPosRight, Quaternion.identity);
                timeBetweenShots = startTimeBetweenShots;
                print("right");
            }*/
        }
        else
        {
            timeBetweenShots -= Time.deltaTime;
        }
    }

    private void Movement()
    {
        if (Vector2.Distance(playerTarget.position, transform.position) > stoppingDistance)
        {
            transform.position = Vector2.MoveTowards
                (transform.position, playerTarget.position, speed * Time.deltaTime);
            cyclopsAnim.SetBool("Walk Forwards", true);
        }
        else if (Vector2.Distance(playerTarget.position, transform.position) < stoppingDistance &&
            Vector2.Distance(playerTarget.position, transform.position) > retreatDistance)
        {
            transform.position = this.transform.position;
            cyclopsAnim.SetBool("Walk Forwards", false);
            cyclopsAnim.SetBool("Walk Backwards", false);
        }
        else if (Vector2.Distance(playerTarget.position, transform.position) < retreatDistance)
        {
            transform.position = Vector2.MoveTowards
               (transform.position, playerTarget.position, -speed * Time.deltaTime);
            cyclopsAnim.SetBool("Walk Backwards", true);
        }

        if (playerTarget.position.x < transform.position.x)
        {
            transform.localScale = new Vector2(-1f, 1f);
            facingRight = false;
        }
        else
        {
            transform.localScale = new Vector2(1f, 1f);
            facingRight = true;
        }

    }
}
