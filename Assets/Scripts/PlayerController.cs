using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

    void FixedUpdate() {
        MoveShip();
    }

    void Update() {
        if (Input.GetKeyDown(KeyCode.Mouse0) && Time.time > nextFire) {
            nextFire = Time.time + fireRate;
            Instantiate(shot, shotSpawn.position, shotSpawn.rotation);
            GetComponent<AudioSource>().Play();
        }
    }

    private void MoveShip() {
        Vector3 pz = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        if (Mathf.Abs(pz.x - rBody.position.x) < 0.3f && Mathf.Abs(pz.z - rBody.position.z) < 0.3f) {
            rBody.velocity = new Vector3(0.0f, 0.0f, 0.0f);            
        } else {
            Vector3 movement = new Vector3(Mathf.Sign(pz.x - rBody.position.x) * speed, 0.0f, Mathf.Sign(pz.z - rBody.position.z) * speed);
            rBody.velocity = movement;

            rBody.position = new Vector3(
                Mathf.Clamp(rBody.position.x, boundary.xMin, boundary.xMax),
                0.0f,
                Mathf.Clamp(rBody.position.z, boundary.zMin, boundary.zMax)
            );
        }
        RotateShip();
    }

    private void RotateShip() {
        rBody.rotation = Quaternion.Euler(0.0f, 0.0f, rBody.velocity.x * tilt);
    }
}
