using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mover : MonoBehaviour {

    public float speed;

	void Start () {
        Rigidbody rBody = GetComponent<Rigidbody>();
        rBody.velocity = transform.forward * speed;
	}
	
}
