using UnityEngine;
using System.Collections;

public class CameraController : MonoBehaviour {
	
	public GameObject player;
	public GameObject torus;
	public Vector3 offset = new Vector3 (0, 0, -8);
	private Vector3 adjustedOffset;
	public float dampTime = 0.25f;
	public float boundary_x = 12.65f; //tested using 4:3 aspect ratio
	public float boundary_y = 14.5f; //tested using 4:3 aspect ratio
	private Vector3 velocity = Vector3.zero;
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKey (KeyCode.Z)) {
			camera.fieldOfView += (100f - camera.fieldOfView)/20f;
		} else {
			camera.fieldOfView += (45f - camera.fieldOfView)/20f;
		}
		adjustedOffset = offset;
		camera.nearClipPlane = Mathf.Max (torus.transform.position.z - transform.position.z + 0.3f, 0.3f);
		Vector3 curPosition = transform.position;
		Vector3 nextPosition = player.transform.position + adjustedOffset;
		nextPosition.x = Mathf.Max (-boundary_x, Mathf.Min (nextPosition.x, boundary_x));
		nextPosition.y = Mathf.Max (-boundary_y, Mathf.Min (nextPosition.y, boundary_y));
		transform.position = Vector3.SmoothDamp (curPosition, nextPosition, ref velocity, dampTime);
	}
}