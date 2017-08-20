using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Boundary {
    public float xMin, xMax, zMin, zMax;
}

public class PlayerController : MonoBehaviour {

    public float speed;
    public float tilt;
    public Boundary boundary;
    public GameObject shot;
    public Transform shotSpawn;
    public float fireRate;

    private Rigidbody rBody;
    private float nextFire = 0.0f;

    private void Start() {
        rBody = GetComponent<Rigidbody>();
    }

    void FixedUpdate () {
        MoveShip();
	}

    void Update() {
        if (Input.GetButton("Fire1") && Time.time > nextFire) {
            nextFire = Time.time + fireRate;
            Instantiate(shot, shotSpawn.position, shotSpawn.rotation);
            GetComponent<AudioSource>().Play();
            
        }
    }

    private void MoveShip() {
        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");

        Vector3 movement = new Vector3(moveHorizontal * speed, 0.0f, moveVertical * speed);
        rBody.velocity = movement;

        rBody.position = new Vector3(
            Mathf.Clamp(rBody.position.x, boundary.xMin, boundary.xMax),
            0.0f,
            Mathf.Clamp(rBody.position.z, boundary.zMin, boundary.zMax)
        );

        rBody.rotation = Quaternion.Euler(0.0f, 0.0f, rBody.velocity.x * tilt);
    }
}
