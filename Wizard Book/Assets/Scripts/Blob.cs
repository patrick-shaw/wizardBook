    using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Blob : MonoBehaviour {

    public float speed;


    Vector3 blobDirection = new Vector3(0, 0, 0);
    Vector3 angleChange = new Vector3(0, 0, -90);


    // Use this for initialization
    void Start () {
    }

    // Update is called once per frame
    void Update ()
    {
        transform.Translate(Vector2.right * speed * Time.deltaTime);
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        transform.eulerAngles = blobDirection += angleChange;
    }

}
