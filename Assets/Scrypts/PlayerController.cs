using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

//Controls Player movement
public class PlayerController : MonoBehaviour
{
    Rigidbody playerRb;

    [SerializeField] GameObject centerOfMass;
    [SerializeField] TextMeshProUGUI speedometerText;
    [SerializeField] TextMeshProUGUI rpmText;
    [SerializeField] List<WheelCollider> allWheels;

    [SerializeField] float speed = 20f;
    [SerializeField] float horsePower = 0;
    [SerializeField] float rpm;
    [SerializeField] int wheelsOnGround;

    float turnSpeed = 30.0f;
    float horizontalInput;
    float forwradInput;
    
   
    private void Start()
    {
        playerRb = GetComponent<Rigidbody>();

        //Set the center of mass to object's position
        playerRb.centerOfMass = centerOfMass.transform.position ;
    }


    // Update is called once per frame
    void FixedUpdate()
    {
        //User input
        horizontalInput = Input.GetAxis("Horizontal");
        forwradInput = Input.GetAxis("Vertical");

        //Preventing moving in the air
        if(IsOnGround())
        playerRb.AddRelativeForce(Vector3.forward * Time.deltaTime * horsePower * forwradInput);
       
        //Rotate the vehicle based on horizontal input
        transform.Rotate(Vector3.up, Time.deltaTime * turnSpeed * horizontalInput);

        //Get the vehicle speed and set it to the Speedometer
        speed = Mathf.RoundToInt(playerRb.velocity.magnitude * 3.6f);
        speedometerText.SetText("Speed: " + speed + " Km/h");

        //Get the RPM and set it to the RPM text field
        rpm = (speed % 30) * 40;
        rpmText.SetText("RPM: " + rpm);
    }

    //Check if the wheels are grounded
    bool IsOnGround()
    {
        wheelsOnGround = 0;

        foreach (WheelCollider wheel in allWheels)
        {
            if (wheel.isGrounded)
                wheelsOnGround++;
        }

        if (wheelsOnGround >= 2)
            return true;
        else
            return false;
    }

}
