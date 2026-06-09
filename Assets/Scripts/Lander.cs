using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class Lander : MonoBehaviour
{

    private const float GRAVITY_DEFAULT = 0.7f;
    
    private Rigidbody2D landerRigidbody;
    private float fuelAmount = 10f;
    private float fuelMax = 10f;

    public event EventHandler OnLeftForce;
    public event EventHandler OnUpForce;
    public event EventHandler OnRightForce;

    public event EventHandler OnBeforeForce;
    
    public event EventHandler OnPickupCoinEvent;
    public event EventHandler<OnLandedEventArgs> OnLanded;
    public event EventHandler<StateChangedEventArgs> StateChanged;

    public State state;
    public enum State
    {
        WaitingToStart,
        Normal,
        GameOver
    }

    public class StateChangedEventArgs : EventArgs
    {
        public State state;
    }

    public static Lander Instance { get; private set; }

    public enum LandingType
    {
        Success,
        WrongLandingArea,
        TooSteepAngle,
        TooFastLanding
    }
    public class OnLandedEventArgs : EventArgs
    {
        public LandingType landingType;
        public int score;
        public float landingSpeed;
        public float landingAngle;
        public int multiplier;
    }

    private void Awake()
    {
        Instance = this;
        landerRigidbody = GetComponent<Rigidbody2D>();
        landerRigidbody.gravityScale = 0f;
        state = State.WaitingToStart;
    }

    private void FixedUpdate()
    {
        switch (state)
        {
            case(State.WaitingToStart) :
                if (Keyboard.current.upArrowKey.isPressed ||
                    Keyboard.current.leftArrowKey.isPressed ||
                    Keyboard.current.rightArrowKey.isPressed)
                {
                    SetState(State.Normal);
                }
                break;
            case(State.Normal) :
                landerRigidbody.gravityScale = GRAVITY_DEFAULT;
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
                break;
            case(State.GameOver) :
                break;
        }
       
    }

    private void SetState(State state)
    {
        this.state = state;
        StateChanged?.Invoke(this, new StateChangedEventArgs
        {
            state = state
        });
    }

    private void OnCollisionEnter2D(Collision2D collision2D)
    {
        if (!collision2D.gameObject.TryGetComponent(out LandingPad landingPad))
        {
            Debug.Log("Crash on Terrain!");
            OnLanded?.Invoke(this,new OnLandedEventArgs
            {
                landingType =  LandingType.WrongLandingArea,
                score = 0,
                landingAngle = 0,
                landingSpeed = 0,
                multiplier =  0
            });
            SetState(State.GameOver);
            return;
        }
        
        float softLandingMagnitude = 4f;
        float speedRuler = collision2D.relativeVelocity.magnitude;
        if (collision2D.relativeVelocity.magnitude > softLandingMagnitude)
        {
            //Landed too hard
            Debug.Log(collision2D.relativeVelocity.magnitude);
            Debug.Log("Landed too hard");
            OnLanded?.Invoke(this,new OnLandedEventArgs
            {
                landingType = LandingType.TooFastLanding,
                score = 0,
                landingAngle = 0,
                landingSpeed = 0,
                multiplier =  0
            });
            SetState(State.GameOver);
            return;
        }

        float dotVector = Vector2.Dot(Vector2.up, transform.up);
        float minDotVector = 0.9f;
        if (dotVector < minDotVector)
        {
            //  Landed on a too steep angle!
            Debug.Log(Vector2.Dot(Vector2.up, transform.up));
            Debug.Log("Landed on a too steep angle!");
            OnLanded?.Invoke(this,new OnLandedEventArgs
            {
                landingType = LandingType.TooSteepAngle,
                score = 0,
                landingAngle = 0,
                landingSpeed = 0,
                multiplier =  0
            });
            SetState(State.GameOver);
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
            landingType = LandingType.Success,
            score = score,
            landingAngle = dotVector,
            landingSpeed = speedRuler,
            multiplier =  landingPad.GetMultiScore()
        });
        SetState(State.GameOver);

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
            if (fuelAmount > fuelMax)
            {
                fuelAmount = fuelMax;
            }
        }
        if (collider2D.gameObject.TryGetComponent(out CoinPickup coinPickup))
        {
            OnPickupCoinEvent?.Invoke(this, EventArgs.Empty);
            coinPickup.DestroySelf();
        }
    }

    public float getSpeedX()
    {
        return landerRigidbody.linearVelocityX;
    }

    public float getSpeedY()
    {
        return landerRigidbody.linearVelocityY;
    }

    public float getFuelAmount()
    {
        return fuelAmount;
    }

    public float getFuelMax()
    {
        return fuelMax;
    }

    public float GetFuelAmountNormalized()
    {
        return fuelAmount / fuelMax;
    }
}