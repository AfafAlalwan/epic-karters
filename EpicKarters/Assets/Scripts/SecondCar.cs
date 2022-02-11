using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SecondCar : MonoBehaviour
{
    public float speed;
    public float turnSpeed;
    public float coinSpeed;
    public float grassSlowdown;
    private Rigidbody rb;
    public float gravityMultiplier;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void OnCollisionEnter(Collision collision)
    {
        rb.constraints = RigidbodyConstraints.None;
    }

    private void OnCollisionExit(Collision collision)
    {
        rb.constraints = RigidbodyConstraints.FreezeRotationZ;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (Input.GetKey(KeyCode.UpArrow))
        {
            Vector3 forceToAdd = transform.forward;
            Vector3 coinToAdd = transform.forward;
            Vector3 grassToAdd = -transform.forward;
            forceToAdd.y = 0;
            rb.AddForce((forceToAdd * (speed + coinSpeed)));

        }
        else if (Input.GetKey(KeyCode.DownArrow))
        {
            Vector3 forceToAdd = -transform.forward;
            forceToAdd.y = 0;
            rb.AddForce(forceToAdd * (speed + coinSpeed));
        }

        Vector3 localVelo = transform.InverseTransformDirection(rb.velocity);
        localVelo = new Vector3(0, localVelo.y, localVelo.z);
        rb.velocity = new Vector3(transform.TransformDirection(localVelo).x, rb.velocity.y, transform.TransformDirection(localVelo).z);

        if (Input.GetKey(KeyCode.RightArrow))
        {
            rb.AddTorque(Vector3.up * turnSpeed * 10);
        }
        else if (Input.GetKey(KeyCode.LeftArrow))
        {
            rb.AddTorque(-Vector3.up * turnSpeed * 10);
        }

        rb.AddForce(Vector3.down * gravityMultiplier);
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Coins"))
        {

            Destroy(other.gameObject);

            if (coinSpeed < 50)
                coinSpeed = coinSpeed + 5;

        }
    }
}
