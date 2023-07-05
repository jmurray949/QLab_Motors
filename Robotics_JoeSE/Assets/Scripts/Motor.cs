using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Motor : MonoBehaviour
{

    public float motorTorque; //Rotational force exerted by the motor 
    public float speed  = 20f; //Speed of the rotation
    public RotationDirection direction; //The direction of the reotation clockwise or anti
    public float friction; // How the motor interacts with other objects
    public float mass; // Mass of motor, determines how motor movements afffects [hysics
    public float powerConsumption; // Don't know
    public float efficiency; //gnhidughad

    public float motorForce;
    public float maxForce;
    public float fuelTank;
    public float maxTorque;
    public float maxSpeed;
    private bool start = true;





    [SerializeField] private CapsuleCollider motorCollider;
    [SerializeField] private Transform motorBody;

    [SerializeField] private Rigidbody motor;


    private RotationDirection goldenRectangle = RotationDirection.Counterclockwise;

    public enum RotationDirection
    {
        Clockwise,
        Counterclockwise
    }


    void ChangeDirection()
    {
        if (Input.GetKeyDown(KeyCode.W)) 
        {
            goldenRectangle = RotationDirection.Counterclockwise;
        }

        else if (Input.GetKeyDown(KeyCode.S))
        {
            goldenRectangle = RotationDirection.Clockwise;
        }
    }

    // Start is called before the first frame update

    // Update is called once per frame
    void Update()
    {

       // getStart();
        ChangeDirection();
        if (start == true)
        {
            handleMotor(motorBody, motorCollider);
        }
    }

    void getStart()
    {
        start = Input.GetKeyDown(KeyCode.Space);
    }

    void handleMotor(Transform turning, CapsuleCollider capCollider)
    {
        //Vector3 pos = transform.position;
  
        //Quaternion rot = transform.rotation;
        //rot = rot * Quaternion.Euler(90, 0, 0);
        while (true)
        {
            if (goldenRectangle == RotationDirection.Counterclockwise)
            {
                transform.Rotate(Vector3.up, speed * Time.deltaTime);
            }

        }

    }

    void Stop()
    {
        if (motorTorque > maxTorque)
        {
            motorForce = 0f;
        }

        else if (motorTorque > maxTorque && motorForce > maxForce)
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

    float TorqueCal(float r)
    {
        motorTorque = r * motorForce;
        return motorTorque;
    }

    //private IEnumerator Start()
    //{
    //    while (true)
    //    {
    //        // Rotate the cylinder around its up axis (Y-axis)
    //        transform.Rotate(Vector3.up, speed * Time.deltaTime);

    //        // Wait for the next frame
    //        yield return null;
    //    }
    //}
}
