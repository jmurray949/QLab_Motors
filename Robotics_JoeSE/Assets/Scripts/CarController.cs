using System.Collections;
using System.Collections.Generic;
using System.Xml.Serialization;
using UnityEngine;
using UnityEngine.UIElements;

/**
 * Car Controller Class - Handles movement, colliders and motor
 * 
 */

public class CarController : MonoBehaviour
{
    //Serailized Fields - Transforms and Colliders needed to indicate movement and collisions
    [SerializeField] private WheelCollider blWheelCollider;
    [SerializeField] private WheelCollider flWheelCollider;
    [SerializeField] private WheelCollider brWheelCollider;
    [SerializeField] private WheelCollider frWheelCollider;

    [SerializeField] private Transform frWheelTransform;
    [SerializeField] private Transform brWheelTransform;
    [SerializeField] private Transform flWheelTransform;
    [SerializeField] private Transform blWheelTransform;

    [SerializeField] private float motorForce;
    [SerializeField] private float breakForce;
    [SerializeField] private float maxSteeringAngle;

    private List<WheelCollider> colliders = new List<WheelCollider>();
    private List<Transform> transforms= new List<Transform>();



    private const string HORIZONTAL = "Horizontal";
    private const string VERTICAL = "Vertical";
    private float horizontalInput;
    private float verticalInput;
    private bool isMoving = true;
    private float currentBreakForce;
    private float currentSteerAngle;
    private bool canMove;

    private bool pressSpace;


    [SerializeField] private Rigidbody vehicle;


    // Update is called once per frame
    void FixedUpdate()
    {
        pressSpace = Input.GetKeyDown(KeyCode.Space);

        GetInput();
        handleMotor();  
        if (pressSpace)
        { 
            if (isMoving)
            {
                ApplyStop();
            }

            else if (canMove)
            {
                ToggleMovement();
            }
        }

        if (isMoving)
        {
            Acceleration();
        }

        else
        {
            Brake();
        }

        updateWheels();
        handleSteering();

    }


    /*
     * Get Input - get WASD and Braking
     * 
     */

    private void GetInput()
    {
        horizontalInput = Input.GetAxis(HORIZONTAL); //Get the horizontal input. The value of the horizontal inout is the 
        verticalInput = Input.GetAxis(VERTICAL);

    }

    private void handleSteering()
    {
        currentSteerAngle = horizontalInput * maxSteeringAngle;
        frWheelCollider.steerAngle = currentSteerAngle;
        flWheelCollider.steerAngle = currentSteerAngle;

    }

    public void handleMotor()
    {
        //For each wheel collider, the motor torque (spinning power applied to wheel by motor) is the max motor force in N * vertical Input
        flWheelCollider.motorTorque = motorForce * verticalInput;
        frWheelCollider.motorTorque = motorForce * verticalInput;
        blWheelCollider.motorTorque = motorForce * verticalInput;
        brWheelCollider.motorTorque = motorForce * verticalInput;
    }

    public void Brake()
    {
        colliders.Add(blWheelCollider);
        colliders.Add(flWheelCollider);
        colliders.Add(brWheelCollider);
        colliders.Add(frWheelCollider);

            foreach (WheelCollider collider in colliders)
            {
                collider.brakeTorque = breakForce;
            }

        vehicle.AddForce(-vehicle.velocity.normalized * breakForce);

    }

    public void UpdateSingleWheel(WheelCollider wheelCollider, Transform wheelTransform)
    {
        Vector3 pos; //Represents the wheel's position
        Quaternion rot; //Represents the wheel's rotation
        wheelCollider.GetWorldPose(out pos, out rot); //Find the wheel's position
        rot = rot * Quaternion.Euler(0, 0, 90); // Rotate the wheel so it turns like a wheel
        
        wheelTransform.position = pos; //assign postion to wheel 
        wheelTransform.rotation = rot; //assign rotation to wheel
    }

    public void updateWheels()
    {

        //Update all wheel positions
        UpdateSingleWheel(frWheelCollider, frWheelTransform);
        UpdateSingleWheel(brWheelCollider, brWheelTransform);
        UpdateSingleWheel(flWheelCollider, flWheelTransform);
        UpdateSingleWheel(blWheelCollider, blWheelTransform);
    }

    private void Acceleration()
    {
        float force = verticalInput * motorForce;

        vehicle.AddForce(transform.forward * force);
    }

    void ApplyForce()  
    {
        vehicle.AddForce(transform.forward * motorForce);
    }

    private void ToggleMovement()
    {

        //So, if isMoving is true/false, it will be turned to the opposite
        isMoving = !isMoving;
    }


    private void ApplyStop()
    {
        vehicle.velocity = Vector3.zero;
        vehicle.angularVelocity =  Vector3.zero;
        canMove= false;
        Invoke(nameof(EnableMovement), 2f);
    }

    private void EnableMovement()
    {

    }
}
