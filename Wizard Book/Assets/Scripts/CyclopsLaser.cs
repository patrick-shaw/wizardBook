using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CyclopsLaser : MonoBehaviour {

    public float speed;

    Transform player;
    Vector2 target;

	// Use this for initialization
	void Start () {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        target = new Vector2(player.position.x, player.position.y);
	}
	
	// Update is called once per frame
	void Update () {
        transform.position = Vector2.MoveTowards(transform.position, target, speed * Time.deltaTime);
        transform.localScale += new Vector3(0.1f, 0, 0) * Time.deltaTime;

        if(transform.position.x == target.x && transform.position.y == target.y)
        {
            Destroy(gameObject);
        }

        if (player.position.x < transform.position.x)
        {
            transform.localScale = new Vector2(-1f, 1f);
          //  facingRight = false;
        }
        else
        {
            transform.localScale = new Vector2(1f, 1f);
            //facingRight = true;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            Destroy(gameObject);
        }
    }
}