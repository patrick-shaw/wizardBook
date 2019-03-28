using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Minotaur : MonoBehaviour {

    public Transform sweepPos;
    public float sweepRange;
    public LayerMask isPlayer;

    public int axeDamage;

    public float timeBetweenSweep;
    public float startTimeSweep;

    public int random;

	// Use this for initialization
	void Start () {
        random = Random.Range(1, 10);
	}
	
	// Update is called once per frame
	void Update () {
        AxeSweep();
        
	}

    private void AxeSweep()
    {
        timeBetweenSweep -= Time.deltaTime;
        if(timeBetweenSweep <= 0)
        {
            print(random);
            print("sweep");
            Collider2D[] playerDamage = Physics2D.OverlapCircleAll
               (sweepPos.position, sweepRange, isPlayer);
            for (int i = 0; i < playerDamage.Length; i++)
            {
                playerDamage[i].GetComponent<HeroHealth>().HurtPlayer(axeDamage);
            }
            timeBetweenSweep = startTimeSweep;
        }

    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(sweepPos.position, sweepRange);
    }
}
