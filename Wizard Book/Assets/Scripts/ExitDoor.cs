using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExitDoor : MonoBehaviour {

    public GameObject doorOpen, doorClosed;

    DoorSwitch doorswitch;


	// Use this for initialization
	void Start () {
        doorswitch = FindObjectOfType<DoorSwitch>();

        gameObject.GetComponent<SpriteRenderer>().sprite =
            doorClosed.GetComponent<SpriteRenderer>().sprite;
	}

    void Update()
    {
        if (doorswitch.isOn)
        {
            gameObject.GetComponent<SpriteRenderer>().sprite =
                doorOpen.GetComponent<SpriteRenderer>().sprite;
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if(doorswitch.isOn && Input.GetButtonDown("Jump"))
        {
            print("exited level");
        }
    }
}
