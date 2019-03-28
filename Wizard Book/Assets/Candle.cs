using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Candle : MonoBehaviour {

    public int health;

    public GameObject heart;

    int heartDrop;

	// Use this for initialization
	void Start () {
        heartDrop = Random.Range(0, 10);
	}
	
	// Update is called once per frame
	void Update () {
		if(health <= 0 && heartDrop >= 5)
        {
            Instantiate(heart, transform.position, Quaternion.identity);
            Destroy(gameObject);
        }
        else if(health <= 0)
        {
            Destroy(gameObject);
        }
	}

    public void DamageCandle(int damage)
    {
        health -= damage;
    }



}
