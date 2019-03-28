using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bat : MonoBehaviour {

    public float speed;
    public float jumpTime;
    public float startjumpTime;
    public float batPush;

    Transform playerTarget;

    HeroHealth hero;

    Rigidbody2D batBod;

	// Use this for initialization
	void Start () {
        playerTarget = GameObject.FindGameObjectWithTag("Player").transform;

        hero = FindObjectOfType<HeroHealth>();

        batBod = GetComponent<Rigidbody2D>();
	}

    // Update is called once per frame
    void Update()
    {
        if (hero.isMobile)
        {
            Fly();
            UpPush();
        }
    
	}

    private void Fly()
    {
        transform.position = Vector2.MoveTowards
            (transform.position, playerTarget.position, speed * Time.deltaTime);



        if (playerTarget.position.x < transform.position.x)
        {
            transform.localScale = new Vector2(-1f, 1f);
        }
        else
        {
            transform.localScale = new Vector2(1f, 1f);
        }
    }

    private void UpPush()
    {
        if (jumpTime <= 0)
        {
            batBod.velocity = new Vector2(0, 0);
            batBod.AddForce(new Vector2(0, batPush), ForceMode2D.Impulse);
            jumpTime = startjumpTime;
            print("BATJUMP");
        }
        else
        {
            jumpTime -= Time.deltaTime;
        }
    }

}
