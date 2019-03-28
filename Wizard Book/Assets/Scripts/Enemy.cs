using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour {

    [SerializeField] int health;

    Rigidbody2D enemyBod;
    Animator enemyAnim;

    public float hitPush = 5;

    Transform player;

    void Start () {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        enemyBod = GetComponent<Rigidbody2D>();
        enemyAnim = GetComponent<Animator>();
	}
	
	void Update ()
    {
        Die();
    }

    public void TakeDamage(int damage)
    {
        health -= damage;
        Debug.Log("taking damage");
        Vector2 difference = transform.position - player.position;
        difference = difference.normalized * hitPush;
        enemyBod.AddForce(difference, ForceMode2D.Impulse);
        //enemyAnim.SetTrigger("Hurt");

    }

    private void Die()
    {
        if(health <= 0)
        {
            Destroy(gameObject);
        }
    }

}
