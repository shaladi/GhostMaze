using UnityEngine;
using System.Collections;

public class CameraController : MonoBehaviour {

    public GameObject player;
    public Vector3 offset = new Vector3 (0, 0, -10);
    public float dampTime = 0.25f;
    public float boundary_x = 7.95f;
    public float boundary_y = 14.5f;
    private Vector3 velocity = Vector3.zero;

    // Update is called once per frame
    void Update () {
        Vector3 curPosition = transform.position;
        Vector3 nextPosition = player.transform.position + offset;
        nextPosition.x = Mathf.Max (-boundary_x, Mathf.Min (nextPosition.x, boundary_x));
        nextPosition.y = Mathf.Max (-boundary_y, Mathf.Min (nextPosition.y, boundary_y));
        transform.position = Vector3.SmoothDamp (curPosition, nextPosition, ref velocity, dampTime);
    }
}
