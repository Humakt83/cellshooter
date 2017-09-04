using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EvasiveManeuver : MonoBehaviour {

    public Vector2 startWait;
    public float dodge;
    public Vector2 maneuverTime;
    public Vector2 maneuverWait;
    public float smoothing;
    public Boundary boundary;

    private float currentSpeed;
    private float targetManouver;
    private Rigidbody rBody;
    
	void Start () {
        rBody = GetComponent<Rigidbody>();
        StartCoroutine(Evade());
        currentSpeed = rBody.velocity.z;
	}
	
	void FixedUpdate () {
        float newManeuver = Mathf.MoveTowards(rBody.velocity.x, targetManouver, Time.deltaTime * smoothing);
        rBody.velocity = new Vector3(newManeuver, 0.0f, currentSpeed);
        rBody.position = new Vector3(
            Mathf.Clamp(rBody.position.x, boundary.xMin, boundary.xMax),
            0.0f,
            Mathf.Clamp(rBody.position.z, boundary.zMin, boundary.zMax)
        );
    }

    IEnumerator Evade() {
        yield return new WaitForSeconds(Random.Range(startWait.x, startWait.y));
        while(true) {
            targetManouver = Random.Range(1, dodge) * -Mathf.Sign(transform.position.x);
            yield return new WaitForSeconds(Random.Range(maneuverTime.x, maneuverTime.y));
            targetManouver = 0;
            yield return new WaitForSeconds(Random.Range(maneuverWait.x, maneuverWait.y));
        }
    }
}
