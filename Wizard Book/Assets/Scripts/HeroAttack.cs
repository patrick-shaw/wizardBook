using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;

public class HeroAttack : MonoBehaviour {

    public float strikeRate = 1.0f;
    float nextStrike;

    [SerializeField] Transform attackPos;
    [SerializeField] float attackRange;
    [SerializeField] LayerMask isEnemy;
    [SerializeField] LayerMask isObject;
    [SerializeField] int swordDamage;

    Animator attackAnim;
    HeroHealth hero;

	// Use this for initialization
	void Start () {
        hero = GetComponent<HeroHealth>();
        attackAnim = GetComponent<Animator>();
         nextStrike = Time.time;
	}
	
	// Update is called once per frame
	void Update () {
        if (!hero.isMobile)
        {
            return;
        }
        SwordHit();
        BreakCandle();
	}

    private void SwordHit()
    {
        if (Time.time > nextStrike && CrossPlatformInputManager.GetButtonDown("Fire1"))
        {
            nextStrike = Time.time + strikeRate;
            Debug.Log("Sword Swing");
            attackAnim.SetTrigger("Sword Attack");
            Collider2D[] enemiesToDamage = Physics2D.OverlapCircleAll
                (attackPos.position, attackRange, isEnemy);
            for (int i = 0; i < enemiesToDamage.Length; i++)
            {
                enemiesToDamage[i].GetComponent<Enemy>().TakeDamage(swordDamage);
            }

            Debug.Log("candle Swing");
            Collider2D[] candleBreak = Physics2D.OverlapCircleAll
                (attackPos.position, attackRange, isObject);
            for (int i = 0; i < candleBreak.Length; i++)
            {
                candleBreak[i].GetComponent<Candle>().DamageCandle(swordDamage);
            }
        }
    }

    private void BreakCandle()
    {
        if (Time.time > nextStrike && CrossPlatformInputManager.GetButtonDown("Fire1"))
        {

        }
    }
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(attackPos.position, attackRange);
    }

}
