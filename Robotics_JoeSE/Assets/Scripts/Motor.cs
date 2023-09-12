using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Motor1 : MonoBehaviour
{
  
    private double motorTorque; //Rotational force exerted by the motor 
    private RotationDirection direction; //The direction of the reotation clockwise or anti - Enumerator
    private float friction; // How the motor interacts with other objects
    private float mass = 500; // Mass of motor, determines how motor movements affects physics. Measured here in grams
    private float powerConsumption; // Don't know
    private float efficiency; //gnhidugha


    private double voltage = 9; //Voltage supplied to the motor in V - is simulated here.
    private double resistance = 3; //resistance measured in ohms - only here for sim purposes
    private double current; // current measured in amps  - sim purposes

    private double torqueConstant = 0.1;





    private Vector3 rot; // The vector representing rotation.

    public float fuelTank;// self explanatory. The amount of fuel. When it reaches 0, nothin happens
    public double maxTorque; //The maximum rotational force
    public float maxRotationSpeed = 100f; //this is the maximum rotation speed
    private bool start; //determines if the motor has started

    //private float acceleration = 5f; //the base acceleration. As


    private float motorRadius = 4; //radius of the motor
    private float inertia; //Ok, this is a property of the motor by which it will continue to move in uniform motion or rest unless a force is applied. I.e. the 

    private double power; //Watts



    [SerializeField] private CapsuleCollider motorCollider; //The motor collider - provides collision detection
    [SerializeField] private Transform motorBody; // the physical motor

    [SerializeField] private Rigidbody motor; // Motor physics simulation

    public float rotationSpeed;  // Speed of rotation in degrees per second
    public float targetRotationSpeed; // the ideal rotation speed
    public float initialRotationSpeed = 0f;

    //private float previousRotation = 0; // the previous rotation of the motor
    //private float currentRotation;
    //public Transform fromObject;

    private void Update()
    {

        try
        {
            //Rotate the motor around its up axis(Y - axis) based on the rotation speed
            ChangeDirection();
            handleMotor(motorBody, motorCollider);
        }
        catch (Exception)
        {
            Console.Error.WriteLine("Something went horribly wrong");
        }

    }

    //Enum for rotation direction
    public enum RotationDirection
    {
        Clockwise,
        Counterclockwise
    }



    RotationDirection ChangeDirection()
    {
        if (Input.GetKeyDown(KeyCode.W)) //If you press down
        {
            direction = RotationDirection.Counterclockwise; //the motor moves anticlockwise (forward)
            rot = Vector3.up;
        }

        else if (Input.GetKeyDown(KeyCode.S))
        {
            direction = RotationDirection.Clockwise;
            rot = Vector3.down;
        }

        return direction;
    }

    //Start is called before the first frame update

    // Update is called once per frame

    void Start()
    {
        rotationSpeed = initialRotationSpeed;
        targetRotationSpeed = initialRotationSpeed;
        start = true;
    }


    /*
     * Handle Motor - implements motion of the motor and direction
     */
    void handleMotor(Transform turning, CapsuleCollider capCollider)
    {
        currentRotation = transform.rotation.eulerAngles.x; // 
        direction = ChangeDirection(); //Gets the input of the direction

        if (start == true)
        {
            rotationSpeed = 1000f; // 
        }

        else if (start == false)
        {
            rotationSpeed = ControlSpeed(); //Calculate rotation speed
        }

        start = false;


        turning.Rotate(rot, rotationSpeed);

    }

    /*
     * Control Speed - so, the angular speed is : Power / Torque
     * Power = voltage * current;
     */
    double ControlSpeed()
    {
        power = voltage * current;

        rotationSpeed = (float)power / (float)motorTorque;

        return rotationSpeed;
    }

    /*
     * Motor Force Calculation Method - For the motor we are using, Motor force = 
     */

    double currentCal()
    {
        current = voltage / resistance;
        return current;
    }

    /*
     * Torque Calculation -  for a brush torque is calcualted as the torque constant * Current
     */
    double TorqueCal()
    {
        motorTorque = current * torqueConstant;
        return motorTorque;
    }






    void Stop()
    {
        if (motorTorque > maxTorque)
        {
            motorTorque = 0;
        }

        else if (motorTorque > maxTorque)
        {
            motor.velocity = Vector3.zero;
            motor.AddForce(-motor.velocity.normalized * 0);
        }

    }


    /**
     * Efficiency: actual energy used / total energy or fuel used.
     */

    float EfficiencyCalculated(float actual, float total)
    {
        efficiency = actual / total;
        return efficiency;
    }


    /*
     * Torque: distance form rotation * force applied
     */




    /*
     * Inertia Method - Calculate inertia using: 1/12 of the mass * 3(r**2)
     */
    float Inertia() //the tendency of a body to resist changes in rotational speed for a given torque. BASICALLY RESISTANCE.
    {
        inertia = (1f / 2f) * mass * (3f * motorRadius * motorRadius);
        return inertia;
    }
}
