
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CarDrive : MonoBehaviour
{
    public bool grounded;
    public float speed;
    public float turnSpeed;
    public float coinSpeed;
    public float grassSlowdown;
    public Rigidbody rb;
    public float gravityMultiplier;

    public KeyCode forward, backward, right, left;
    public TMP_Text coinsCountText;
    public int coinsCount;

    private void OnCollisionStay(Collision collision)
    {
        grounded = true;
    }

    private void OnCollisionExit(Collision collision)
    {
        grounded = false;
    }

    void FixedUpdate()
    {
        if (Input.GetKey(forward))
        {
            Vector3 forceToAdd = transform.forward;
            Vector3 coinToAdd = transform.forward;
            Vector3 grassToAdd = -transform.forward;
            forceToAdd.y = 0;
            rb.AddForce((forceToAdd * (speed + coinSpeed)));

        }
        else if (Input.GetKey(backward))
        {
            Vector3 forceToAdd = -transform.forward;
            forceToAdd.y = 0;
            rb.AddForce(forceToAdd * (speed + coinSpeed));
        }

        Vector3 localVelo = transform.InverseTransformDirection(rb.velocity);
        localVelo = new Vector3(0, localVelo.y, localVelo.z);
        rb.velocity = new Vector3(transform.TransformDirection(localVelo).x, rb.velocity.y, transform.TransformDirection(localVelo).z);

        if (Input.GetKey(right) && grounded)
        {
            rb.AddTorque(Vector3.up * turnSpeed * 10);
        }
        else if (Input.GetKey(left) && grounded)
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
            if(coinsCount != 10)
            {
                coinsCount++;
                coinsCountText.text = coinsCount.ToString();
            }
       
            if (coinSpeed < 50)
                coinSpeed = coinSpeed + 5;

        }
    }
}
