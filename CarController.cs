using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarController : MonoBehaviour
{
    private Vector3 startPosition, startRotation; //This will preserve the start position and rotation of the car in case we need to start the car from thr beginning

    [Range(-1f, 1f)] //acceleration and turning value
    public float a, t;

    public float timeSinceStart = 0f;
    public GameObject lastHit;
    // fitness of the car means that how fast and far it goes, how well it did
    [Header("fitness")]
    public float overallFitness;
    public float distanceMultiplier = 1.4f; // how important distance is to the distance function
    public float avgSpeedMultiplier = 0.2f; // 
    public float sensorMultiplier = 0.1f; // it tells our car how important it is to stay in the middle of the car

    private Vector3 lastPosition;
    private float totalDistanceTravelled;
    private float avgSpeed;

    private float aSensor, bSensor, cSensor; // dstance value and input of our neural net

    private void Awake()
    {
        startPosition = transform.position;
        startRotation = transform.eulerAngles;
    }

    public void Reset()
    {
        //it will reset oue function
        timeSinceStart = 0f;
        totalDistanceTravelled = 0f;
        avgSpeed = 0f;
        lastPosition = startPosition;
        overallFitness = 0f;
        transform.eulerAngles = startRotation;

    }

    private void OnCollisionEnter(Collision collision)
    {
        Reset(); // In this case there is only wall to collide. So we don't need to check
    }

    // most important function as we want to happens uniformly across all frame rate

    private void FixedUpdate()
    {
        InputSensors();
        lastPosition = transform.position; // we are sending the last position then we are moving the car then calculate the fitness

        // Neurqal network code here
        MoveCar(a, t); // control the a ant t values in the inspector

        timeSinceStart += Time.deltaTime;
        CalculateFitness();

        //a = 0; will do it later as we need to play around with them
        //t = 0;
    }

    // calculating the fitness
    private void CalculateFitness()
    {
        // this three value will give how we want to control cars evolve over time
        // do we want to make our car in the middle or how fast it will drive
        totalDistanceTravelled += Vector3.Distance(transform.position, lastPosition); // it will give the current position to last position 
        // by calculating this we can find the distance
        avgSpeed = totalDistanceTravelled / timeSinceStart; //average speed
        overallFitness = (totalDistanceTravelled * distanceMultiplier) + (avgSpeed * avgSpeedMultiplier) + (((aSensor + bSensor + cSensor) / 3) * sensorMultiplier);

        // if our network is dumb or smart
        if (timeSinceStart > 20 && overallFitness < 40) // the value is very small
        {
            Reset();
        }
        if (overallFitness >= 1000)
        {
            // later work Save network to a JSON
            Reset();
        }
    }
    // hit the wall and get the value
    private void InputSensors()
    {
        Vector3 a = (transform.forward + transform.right); // it will get the diagonal value of the right
        Vector3 b = (transform.forward);

        Vector3 c = (transform.forward - transform.right);
        // While driving a car we look forward, diagonally left and right


        Ray r = new Ray(transform.position + new Vector3(0f, .6f, 0f), a);
        RaycastHit hit;

        if (Physics.Raycast(r, out hit))
        {
          aSensor = hit.distance / 20; // to normalize the value as when we pass the value in the neural network if the value is very large then it will be clamped
         //the value will always be 1 , which will not create any diversity in the direction
         //it will ensure the input is not too large
          //print("A :" + aSensor);
        }
        r.direction = b;
        //var forward = new Ray(transform.position + new Vector3(0f,.6f,0f), b);
        //RaycastHit hit; //returns a boolean
        if (Physics.Raycast(r, out hit)) //layer max can be added
        {
            Debug.Log(hit.transform.gameObject);
            bSensor = hit.distance  ;
            
            print("B :" + bSensor);
        }

        r.direction = c;
        if (Physics.Raycast(r, out hit))
        {
            cSensor = hit.distance / 20;
            //print("C :" + cSensor);
        }
    }

    private Vector3 inp;
    //hardcode this function
    public void MoveCar(float v, float h) // vertical and horizontal rotation and acceleration
    {
        //  lerp = linear interpolation
        // lerping between two values with a rate of 0.02f
        // v*(11.4 means to get the correct magnitude of the acceleration)
        // this 11.4 comes with trial and error as the car went too fast sometimes, sometimes a rat-like uniformly 

        if (bSensor < 20f)
        {
            inp = Vector3.Lerp(Vector3.zero, new Vector3(0, 0, 0), 0.02f);
        }
        else
        {

            inp = Vector3.Lerp(Vector3.zero, new Vector3(0, 0, (v * 11.4f)), 0.02f);
            //(0,0,1) means to go forward but it's in the global space, so to convert it into 
            // local space in terms of the car, we use transform.TransformDirection(inp) function
            // this will transform 001 to what is forward in terms of the car

            inp = transform.TransformDirection(inp); // change the transform direction to make sure that the input is relative to the car
            transform.position += inp;
        }

        // how much the car move by 90, if the hedge is 1, it will move 90deg, if h is -1 then -90deg
        // then to smooth the rotation multiply with the 0.02f

        
            transform.eulerAngles += new Vector3(0, (h * 90) * 0.02f, 0);
        

    }
}
