using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Imp : MonoBehaviour {

    public float speed;
    public float stoppingDistance;
    public float retreatDistance;

    public float timeBetweenShots;
    public float startTimeBetweenShots;

    public float animationDelay;

    public Vector2 jumpBack = new Vector2(10f, 10f);

    public GameObject fireball;

    Transform player;

    Animator impAnim;
    
	void Start () {
        player = GameObject.FindGameObjectWithTag("Player").transform;

        timeBetweenShots = startTimeBetweenShots;

        impAnim = GetComponent<Animator>();
	}
	
	void Update () {
        Movement();
        Fireball();
	}

    private void Fireball()
    {
        if(timeBetweenShots <= 0)
        {
            impAnim.SetTrigger("Fireball");
            StartCoroutine(Fire());
            timeBetweenShots = startTimeBetweenShots;
        }
        else
        {
            timeBetweenShots -= Time.deltaTime;
        }
    }

    IEnumerator Fire()
    {
        yield return new WaitForSeconds(animationDelay);
        Instantiate(fireball, transform.position, Quaternion.identity);
    }

    private void Movement()
    {
        if(Vector2.Distance(player.position, transform.position) > stoppingDistance)
        {
            transform.position = Vector2.MoveTowards
                (transform.position, player.position, speed * Time.deltaTime);
        }
        else if (Vector2.Distance(player.position, transform.position) < stoppingDistance &&
            Vector2.Distance(player.position, transform.position) > retreatDistance)
        {
            transform.position = this.transform.position;
        }
        else if (Vector2.Distance(player.position, transform.position) < retreatDistance)
        {
           // GetComponent<Rigidbody2D>().velocity = jumpBack;
           transform.position = Vector2.MoveTowards
                (transform.position, player.position, -speed * Time.deltaTime);
        }

        if (player.position.x < transform.position.x)
        {
            transform.localScale = new Vector2(1f, 1f);
        }
        else
        {
            transform.localScale = new Vector2(-1f, 1f);
        }
    }
}
