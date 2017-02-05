using UnityEngine;
public class SpinObject : MonoBehaviour {
    public float speed=100f;

	void FixedUpdate () {
        transform.Rotate(Vector3.up, speed*Time.fixedDeltaTime);
        transform.localPosition = new Vector3(transform.localPosition.x, Mathf.PingPong(Time.time*3f, 1.5f)+0.5f, transform.localPosition.z);
    }
}
