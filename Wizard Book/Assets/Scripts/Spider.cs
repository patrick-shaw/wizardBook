using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spider : MonoBehaviour {

    public float speed;
    Rigidbody2D spiderBod;

    Vector3 spiderDirection = new Vector3(0, 0, 0);
    Vector3 angleChange = new Vector3(0, 0, -90);

	// Use this for initialization
	void Start () {
        spiderBod = GetComponent<Rigidbody2D>();
	}

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector2.right * speed * Time.deltaTime);
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        transform.eulerAngles = spiderDirection += angleChange;
    }
}
