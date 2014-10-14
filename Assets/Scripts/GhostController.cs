using UnityEngine;
using System.Collections;

public class GhostController : MonoBehaviour {
    
    // Ghost location
    public float speed =  100f;
    public float refreshRate = 4f;
    private float nextCommand = 0f;
    
    // Update is called once per frame
    void Update () {

        /* 
         * Ghost movement Logic
         */
        if (Time.time > nextCommand) {
            nextCommand = Time.time + refreshRate;
            //Horizontal Movement Vector
            Vector2 hor = (-(Random.Range (-1, 2))) * Vector2.right;
            Vector2 ver = (-(Random.Range (-1, 2))) * Vector2.up;
            rigidbody2D.velocity = (hor + ver) * speed * Time.deltaTime;
        }
    }
    
    /* Collisions */
    void OnTriggerStay2D (Collider2D other) {
        if (other.gameObject.tag == "Beacon") {
            if (Input.GetKey (KeyCode.P)) {
                //Destroy (other.gameObject.transform.parent.gameObject);
                //--;
            }
        }
    }
    
}
