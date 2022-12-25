using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class CarScript : MonoBehaviour
{

    WheelJoint2D[] wheelJoints;
    JointMotor2D frontWheel;
    JointMotor2D backWheel;

    public float maxSpeed = -1000f;
    private float maxBackSpeed = 1500f;
    public float acceleration = 300f;
    private float deacceleration = -100f;
    public float brakeForce = 3000f;
    private float gravity = 9.8f;
    private float angleCar = 0;
    public float wheelSize = 0.078f;
    public float fuelSize;
    public float fuelUsage;
    public float fualAdd = 20;
    public bool grounded = false;
    public LayerMask map;
    public Transform bwheel;
    public int coinsInt = 0;
    public Text coinsText;
    public GameObject fp;
    public GameObject centerMass;
    public GameObject fuelProgressBar;
    private AudioSource carsSound;
    public AudioSource coinsSound;
    private float currentFuel;

    public ClickScript[] ControlCar;

    void Start()
    {
        GetComponent<Rigidbody2D>().centerOfMass = centerMass.transform.localPosition;
        wheelJoints = gameObject.GetComponents<WheelJoint2D>();
        backWheel = wheelJoints[1].motor;
        frontWheel = wheelJoints[0].motor;

        carsSound = GetComponent<AudioSource>();

        currentFuel = fuelSize;

    }
    void Update()
    {
        coinsText.text = coinsInt.ToString();
        grounded = Physics2D.OverlapCircle(bwheel.transform.position, wheelSize, map);

        carsSound.pitch = Mathf.Clamp(-backWheel.motorSpeed /1000,0.3f,3);

    }
    void FixedUpdate()
    {
        if (currentFuel <= 0)
        {
            print("Закончилось топливо!");
            if (grounded == true)
            {
                backWheel.motorSpeed = Mathf.Clamp(backWheel.motorSpeed - brakeForce * Time.deltaTime, 0, maxBackSpeed);
            }
            return;
        }
        
        frontWheel.motorSpeed = backWheel.motorSpeed;

        angleCar = transform.localEulerAngles.z;

        if (angleCar >= 180)
        {
            angleCar = angleCar - 360;
        }

        if (grounded == true)
        {//Педаль газа
            if (ControlCar[0].clickedIs == true)
            {
                backWheel.motorSpeed = Mathf.Clamp(backWheel.motorSpeed - (acceleration - gravity * Mathf.PI * (angleCar / 2)) * Time.deltaTime, maxSpeed, maxBackSpeed);
                currentFuel -= fuelUsage * Time.deltaTime;
                print(currentFuel);
            }
            //Тяготение машины гравитацией.
            else if ((backWheel.motorSpeed < 0) || (ControlCar[0].clickedIs == false && backWheel.motorSpeed == 0 && angleCar < 0))
            {
                backWheel.motorSpeed = Mathf.Clamp(backWheel.motorSpeed - (deacceleration - gravity * Mathf.PI * (angleCar / 2)) * Time.deltaTime, maxSpeed, 0);
                currentFuel -= (fuelUsage / 2.5f) * Time.deltaTime;
            }
            if ((ControlCar[0].clickedIs == false && backWheel.motorSpeed > 0) || (ControlCar[0].clickedIs == false && backWheel.motorSpeed == 0 && angleCar > 0))
            {
                backWheel.motorSpeed = Mathf.Clamp(backWheel.motorSpeed - (-deacceleration - gravity * Mathf.PI * (angleCar / 2)) * Time.deltaTime, 0, maxBackSpeed);
            }
        }
        else
        {
            //Если машина не касается земли, то колёса замедляются.
            if (ControlCar[0].clickedIs == false && backWheel.motorSpeed < 0)
            {
                backWheel.motorSpeed = Mathf.Clamp(backWheel.motorSpeed - deacceleration * Time.deltaTime, maxSpeed, 0);
            }
            else if (ControlCar[0].clickedIs == false && backWheel.motorSpeed > 0)
            {
                backWheel.motorSpeed = Mathf.Clamp(backWheel.motorSpeed + deacceleration * Time.deltaTime, 0, maxBackSpeed);
            }
            //Если машина не касается земли и нажата кнопка газа, то колёса начинают крутиться.
            if (ControlCar[0].clickedIs == true)
            {
                backWheel.motorSpeed = Mathf.Clamp(backWheel.motorSpeed - (acceleration - gravity * Mathf.PI * (angleCar / 180) * 100) * Time.deltaTime, maxSpeed, maxBackSpeed);
                currentFuel -= (fuelUsage/1.25f) * Time.deltaTime;
            }
        }
        //Тормозная система
        if (ControlCar[1].clickedIs == true && backWheel.motorSpeed > 0)
        {
            backWheel.motorSpeed = Mathf.Clamp(backWheel.motorSpeed - brakeForce * Time.deltaTime, 0, maxBackSpeed);
        }
        else if (ControlCar[1].clickedIs == true && backWheel.motorSpeed < 0)
        {
            backWheel.motorSpeed = Mathf.Clamp(backWheel.motorSpeed + brakeForce * Time.deltaTime, maxSpeed, 0);
        }
        //Приравниваем скорость WheelJoints к JointMotor2D.
        wheelJoints[1].motor = backWheel;
        wheelJoints[0].motor = frontWheel;

        fuelProgressBar.transform.localScale = new Vector2(currentFuel/fuelSize,1);
    }
    void OnTriggerEnter2D(Collider2D trigger)
    {
        if (trigger.gameObject.tag == "coins")
        {
            coinsInt++;
            coinsSound.pitch = coinsSound.pitch + 0.1f;
            coinsSound.Play();
            Destroy(trigger.gameObject);
        }
        else if (trigger.gameObject.tag == "Finish")
        {
            //SceneManager.LoadScene(1);
            fp.SetActive(true);
        }

        else if (trigger.gameObject.tag == "fuel")
        {
            currentFuel += fualAdd;
            Destroy(trigger.gameObject);
                }
    }
    void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(bwheel.transform.position, wheelSize);
    }
}