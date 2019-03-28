using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skeleton : MonoBehaviour {

    float swingCounter;
    public Transform attackPos;
    Transform playerTarget;
    public float stoppingDistance;
    public float retreatDistance;
    public float speed;
    public int swordDamage;

    public LayerMask isPlayer;
    public float attackRange;
    public float minTimeBetweenSwings;
    public float maxTimeBetweenSwings;
    public float animationWait = 1f;

    bool facingRight = true;

    Animator skelAnim;

	void Start () {
        skelAnim = GetComponent<Animator>();

        playerTarget = GameObject.FindGameObjectWithTag("Player").transform;

        swingCounter = Random.Range(minTimeBetweenSwings, maxTimeBetweenSwings);
	}
	
	void Update () {
        Movement();
        FacePlayer();
        CountDownandSwing();

	}

    private void Movement()
    {
        if (Vector2.Distance(playerTarget.position, transform.position) > stoppingDistance)
        {
            transform.position = Vector2.MoveTowards
                (transform.position, playerTarget.position, speed * Time.deltaTime);
            //skelAnim.SetBool("Run", true);
        }
        else if (Vector2.Distance(playerTarget.position, transform.position) < stoppingDistance &&
            Vector2.Distance(playerTarget.position, transform.position) > retreatDistance)
        {
            transform.position = this.transform.position;
           // skelAnim.SetBool("Run", false);
        }
        else if (Vector2.Distance(playerTarget.position, transform.position) < retreatDistance)
        {
            transform.position = Vector2.MoveTowards
               (transform.position, playerTarget.position, -speed * Time.deltaTime);
           // skelAnim.SetBool("Run", true);
        }
    }

    private void FacePlayer()
    {
        if(playerTarget.position.x < transform.position.x)
        {
            transform.localScale = new Vector2(1f, 1f);
            facingRight = false;
        }
        else
        {
            transform.localScale = new Vector2(-1f, 1f);
            facingRight = true;
        }
    }

    private void CountDownandSwing()
    {
        swingCounter -= Time.deltaTime;
        if(swingCounter <= 0)
        {
            
            skelAnim.SetTrigger("Attack");
            print("skeleswing");
            StartCoroutine(SwordSwing());
            
            swingCounter = Random.Range(minTimeBetweenSwings, maxTimeBetweenSwings);
        }
    }

    IEnumerator SwordSwing()
    {
        yield return new WaitForSeconds(animationWait);
        Enemy sword = GetComponent<Enemy>();
        Collider2D[] playerDamage = Physics2D.OverlapCircleAll
            (attackPos.position, attackRange, isPlayer);
        for (int i = 0; i < playerDamage.Length; i++)
        {
            playerDamage[i].GetComponent<HeroHealth>().HurtPlayer(swordDamage);
        }
    }

    
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(attackPos.position, attackRange);
    }
}
