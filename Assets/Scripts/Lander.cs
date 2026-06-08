using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class Lander : MonoBehaviour
{
    private Rigidbody2D landerRigidbody;
    private float fuelAmount = 10f;

    public event EventHandler OnLeftForce;
    public event EventHandler OnUpForce;
    public event EventHandler OnRightForce;

    public event EventHandler OnBeforeForce;
    
    public event EventHandler OnPickupCoinEvent;
    public event EventHandler<OnLandedEventArgs> OnLanded;

    public static Lander Instance;

    public class OnLandedEventArgs : EventArgs
    {
        public int score;
    }

    private void Awake()
    {
        Instance = this;
        landerRigidbody = GetComponent<Rigidbody2D>();
    }
    private void FixedUpdate()
    {
        OnBeforeForce?.Invoke(this, EventArgs.Empty);
        if (fuelAmount <= 0)
        {
            return;
        }
        if (Keyboard.current.upArrowKey.isPressed ||
            Keyboard.current.leftArrowKey.isPressed ||
            Keyboard.current.rightArrowKey.isPressed)
        {
            ConsumptFuel();
        }
        if (Keyboard.current.upArrowKey.isPressed)
        {
            float speed = 600f;
            landerRigidbody.AddForce(transform.up * (speed * Time.deltaTime));
            //Debug.Log("Up");
            OnUpForce?.Invoke(this,EventArgs.Empty);
        }

        if (Keyboard.current.rightArrowKey.isPressed)
        {
            float turnSpeed = -100f;
            landerRigidbody.AddTorque(turnSpeed * Time.deltaTime);
            //Debug.Log("Right");
            OnRightForce?.Invoke(this,EventArgs.Empty);
        }

        if (Keyboard.current.leftArrowKey.isPressed)
        {
            float turnSpeed = 100f;
            landerRigidbody.AddTorque(turnSpeed * Time.deltaTime);
            OnLeftForce?.Invoke(this,EventArgs.Empty);
            //Debug.Log("Left");
        }
    }

    private void OnCollisionEnter2D(Collision2D collision2D)
    {
        if (!collision2D.gameObject.TryGetComponent(out LandingPad landingPad))
        {
            Debug.Log("Crash on Terrain!");
            return;
        }
        
        float softLandingMagnitude = 4f;
        float speedRuler = collision2D.relativeVelocity.magnitude;
        if (collision2D.relativeVelocity.magnitude > softLandingMagnitude)
        {
            //Landed too hard
            Debug.Log(collision2D.relativeVelocity.magnitude);
            Debug.Log("Landed too hard");
            return;
        }

        float dotVector = Vector2.Dot(Vector2.up, transform.up);
        float minDotVector = 0.9f;
        if (dotVector < minDotVector)
        {
            //  Landed on a too steep angle!
            Debug.Log(Vector2.Dot(Vector2.up, transform.up));
            Debug.Log("Landed on a too steep angle!");
            return;
        }
        
        Debug.Log("Landed successfully");

        float maxiumScoreAngle = 100f;
        float multiplyScoreAngle = 10f;
        float scoreAngle = (dotVector - minDotVector) * multiplyScoreAngle * maxiumScoreAngle;
        Debug.Log("Score Angle: " + scoreAngle);

        float maxiumScoreSpeed = 100f;
        float scoreSpeed = (softLandingMagnitude - speedRuler) * maxiumScoreSpeed;
        Debug.Log("Score Speed: " + scoreSpeed);
        
        int score = Mathf.RoundToInt(scoreSpeed + scoreAngle) * landingPad.GetMultiScore() ;
        Debug.Log("Score: " + score);
        OnLanded?.Invoke(this,new OnLandedEventArgs
        {
            score = score
        });

    }

    private void ConsumptFuel()
    {
        float fuelConsumpt = 1f;
        fuelAmount -= Time.deltaTime * fuelConsumpt;
    }
    

    private void OnTriggerEnter2D(Collider2D collider2D)
    {
        if (collider2D.gameObject.TryGetComponent(out FuelPickup fuelPickup))
        {
            float fuelPickupAmount = 10f;
            fuelAmount += fuelPickupAmount;
            fuelPickup.DestroySelf();
        }
        if (collider2D.gameObject.TryGetComponent(out CoinPickup coinPickup))
        {
            OnPickupCoinEvent?.Invoke(this, EventArgs.Empty);
            coinPickup.DestroySelf();
        }
    }
}