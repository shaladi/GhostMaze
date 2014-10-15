using UnityEngine;
using System.Collections;

public class CameraController : MonoBehaviour {

    public GameObject player;
    public Vector3 offset = new Vector3 (0, 0, -8);
	private Vector3 adjustedOffset;
    public float dampTime = 0.25f;
	public float boundary_x = 12.65f; //tested using 4:3 aspect ratio
	public float boundary_y = 14.5f; //tested using 4:3 aspect ratio
    private Vector3 velocity = Vector3.zero;

    // Update is called once per frame
    void Update () {
		if (Input.GetKey (KeyCode.Z)) {
			adjustedOffset =  offset - new Vector3(0,0,22);
		} else {
			adjustedOffset = offset;
		}

        Vector3 curPosition = transform.position;
        Vector3 nextPosition = player.transform.position + adjustedOffset;
        nextPosition.x = Mathf.Max (-boundary_x, Mathf.Min (nextPosition.x, boundary_x));
        nextPosition.y = Mathf.Max (-boundary_y, Mathf.Min (nextPosition.y, boundary_y));
        transform.position = Vector3.SmoothDamp (curPosition, nextPosition, ref velocity, dampTime);
    }
}
