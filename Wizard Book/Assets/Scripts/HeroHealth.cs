using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeroHealth : MonoBehaviour
{

    public int health;
    public int collisionDamage;

    public float timeAfterHurt;
    public float hitPush;

    Rigidbody2D heroRigidBody;
    Animator damageAnim;
    Collider2D[] allColliders;
    HeartManager hearts;

    public bool isMobile = true;

    void Start()
    {
        heroRigidBody = GetComponent<Rigidbody2D>();
        damageAnim = GetComponent<Animator>();
        allColliders = GetComponents<Collider2D>();
        hearts = GetComponent<HeartManager>();

    }

    void Update()
    {

    }

    public void HurtPlayer(int damage)
    {
        print("hurt");
        health -= damage;
        hearts.DamageDude();
        if (health <= 0)
        {
            Die();
        }
        else
        {
            isMobile = false;
            StartCoroutine(HurtBlinker());

        }
    }

    IEnumerator HurtBlinker()
    {
        int enemyLayer = LayerMask.NameToLayer("Enemy");
        int playerLayer = LayerMask.NameToLayer("Player");
        Physics2D.IgnoreLayerCollision(enemyLayer, playerLayer);
        foreach (Collider2D collider in allColliders)
        {
            collider.enabled = false;
            collider.enabled = true;
        }

        damageAnim.SetTrigger("Hurt");
        Debug.Log("hurt anim");

        yield return new WaitForSeconds(timeAfterHurt);

        Physics2D.IgnoreLayerCollision(enemyLayer, playerLayer, false);
        isMobile = true;
    }

   /* private void OnTriggerEnter2D(Collider2D collision)
    {
        Enemy enemy = collision.gameObject.GetComponent<Enemy>();
        if (enemy != null)
        {
            HurtPlayer(collisionDamage);
            Vector2 difference = transform.position - enemy.transform.position;
            difference = difference.normalized * hitPush;
            heroRigidBody.AddForce(difference, ForceMode2D.Impulse);
        }        
    }*/


    private void OnCollisionEnter2D(Collision2D collision)
    {
        Enemy enemy = collision.collider.GetComponent<Enemy>();
        if (enemy != null)
        {
            HurtPlayer(collisionDamage);
            Vector2 difference = transform.position - enemy.transform.position;
            difference = difference.normalized * hitPush;
            heroRigidBody.AddForce(difference, ForceMode2D.Impulse);
        }
    }
   

    private void Die()
    {
        isMobile = false;
        damageAnim.SetTrigger("Death");
        int enemyLayer = LayerMask.NameToLayer("Enemy");
        int playerLayer = LayerMask.NameToLayer("Player");
        Physics2D.IgnoreLayerCollision(enemyLayer, playerLayer);
    }
}
