using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class FlyingSkull : MonoBehaviour
{

    public float speed;

    Transform playerTarget;

    HeroHealth hero;

    // Use this for initialization
    void Start()
    {
        playerTarget = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        hero = FindObjectOfType<HeroHealth>();
    }

    // Update is called once per frame
    void Update()
    {
        if (hero.isMobile)
        {
            transform.position = Vector2.MoveTowards
                (transform.position, playerTarget.position, speed * Time.deltaTime);
        }



        if (playerTarget.position.x < transform.position.x)
        {
            transform.localScale = new Vector2(-1f, 1f);
        }
        else
        {
            transform.localScale = new Vector2(1f, 1f);
        }

    }
}