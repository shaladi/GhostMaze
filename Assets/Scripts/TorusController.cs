using UnityEngine;
using System.Collections;

public class TorusController : MonoBehaviour {

    public GameObject player;
    public Vector3 offset = new Vector3 (-13, 1, -10);
    public float dampTime = 0.25f;
    public Vector3 speed = Vector3.zero;

    // Update is called once per frame
    void Update () {
        Vector3 curPosition = transform.position;
        Vector3 nextPosition = player.transform.position + offset;
        transform.position = Vector3.SmoothDamp (curPosition, nextPosition, ref speed, dampTime);
    }
}
