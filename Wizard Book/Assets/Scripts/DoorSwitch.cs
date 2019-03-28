using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DoorSwitch : MonoBehaviour {

    public GameObject switchOn, switchOff;

    public bool isOn;

	// Use this for initialization
	void Start () {
        gameObject.GetComponent<SpriteRenderer>().sprite = 
            switchOff.GetComponent<SpriteRenderer>().sprite;
	}

    private void OnTriggerEnter2D(Collider2D collision)
    {   
        gameObject.GetComponent<SpriteRenderer>().sprite =
            switchOn.GetComponent<SpriteRenderer>().sprite;

        isOn = true;
    }

    // Update is called once per frame
    void Update () {
		
	}

}
