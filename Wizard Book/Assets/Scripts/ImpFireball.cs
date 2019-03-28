using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ImpFireball : MonoBehaviour {

    public float speed;
    public int fireDamage;

    Transform player;
    Vector2 playerPosition;

    Rigidbody2D fireballBod;
    Vector2 moveDirection;

    HeroHealth damage;


	void Start () {
        player = GameObject.FindGameObjectWithTag("Player").transform;

        fireballBod = GetComponent<Rigidbody2D>();

        moveDirection = (player.transform.position - transform.position).normalized * speed;

        fireballBod.velocity = new Vector2(moveDirection.x, moveDirection.y + 0.5f);

        damage = FindObjectOfType<HeroHealth>();
	}
	
	void Update () {
       // Movement();
        playerPosition = new Vector2(player.position.x, player.position.y);

        //  transform.Translate(-playerPosition * speed * Time.deltaTime);
    }

    private void Movement()
    {
        transform.position = Vector2.MoveTowards
            (transform.position, playerPosition, speed * Time.deltaTime);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            Destroy(gameObject);
            damage.HurtPlayer(fireDamage);
        }
    }
}
