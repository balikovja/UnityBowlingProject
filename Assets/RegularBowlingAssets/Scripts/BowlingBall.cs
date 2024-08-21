using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BowlingBall : MonoBehaviour
{
    public float leftBorder;
    public float rightBorder;
    public float maxRotation;
    public float positionIncrement;
    public float rotationIncrement;
    public Rigidbody body;
    private GameObject guideLine;
    private bool isBowling = false;
    public PinBallSpawner pinBallSpawner;
    public AudioSource pinSoundHandler;
    private bool collided = false;

    private void Start()
    {
        pinBallSpawner = FindObjectOfType<PinBallSpawner>();
        pinSoundHandler = GameObject.Find("PinSoundHandler").GetComponent<AudioSource>();
    }

    public IEnumerator BowlCoroutine(float forwardForce)
    {
        if (!isBowling)
        {
            guideLine.SetActive(false);
            body.AddForce(-transform.forward * forwardForce, ForceMode.Impulse);
            // how to curve: body.AddTorque(new Vector3(0, 0, 1) * 100, ForceMode.Impulse);
            yield return new WaitForSeconds(6.0f);

            // Check if pins have fallen and update the score
            pinBallSpawner.CheckPinsAndUpdateScore();
        }
        isBowling = true;
    }

    /*
    public void Bowl(Vector3 position, Quaternion rotation)
    {
        transform.position = position;
        transform.rotation = rotation;
        guideLine.SetActive(false);
        body.AddForce(-transform.forward * forwardForce, ForceMode.Impulse);
        isBowling = true;
    }
    */

    public void MoveLeft()
    {
        if (transform.position.x < leftBorder && !isBowling)
        {
            transform.position += new Vector3(positionIncrement, 0, 0);
            guideLine.transform.position += new Vector3(positionIncrement, 0, 0);
        }
    }

    public void MoveRight()
    {
        if (transform.position.x > rightBorder && !isBowling)
        { 
            transform.position += new Vector3(-positionIncrement, 0, 0);
            guideLine.transform.position += new Vector3(-positionIncrement, 0, 0);
        }
    }

    public void RotateLeft()
    {
        if (transform.rotation.eulerAngles.y < maxRotation + 1 || transform.rotation.eulerAngles.y > 360 - maxRotation - 1)
        {
            transform.Rotate(new Vector3(0, -rotationIncrement, 0));
            guideLine.transform.RotateAround(transform.position, new Vector3(0, 1, 0), -rotationIncrement);
        }
        else
        {
            transform.Rotate(new Vector3(0, rotationIncrement, 0));
            guideLine.transform.RotateAround(transform.position, new Vector3(0, 1, 0), rotationIncrement);
        }
    }

    public void RotateRight()
    {
        if (transform.rotation.eulerAngles.y < maxRotation + 1 || transform.rotation.eulerAngles.y > 360 - maxRotation - 1)
        {
            transform.Rotate(new Vector3(0, rotationIncrement, 0));
            guideLine.transform.RotateAround(transform.position, new Vector3(0, 1, 0), rotationIncrement);
        }
        else
        {
            transform.Rotate(new Vector3(0, -rotationIncrement, 0));
            guideLine.transform.RotateAround(transform.position, new Vector3(0, 1, 0), -rotationIncrement);
        }
    }

    public GameObject GetGuideLine()
    {
        return guideLine;
    }

    
    public void SetGuideLine(GameObject guideLine)
    {
        this.guideLine = guideLine;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Pin") && !collided)
        {
            // Play the collision sound
            collided = true;
            pinSoundHandler.Play();
        }
    }

    public void ResetCollided()
    {
        collided = false;
    }
}
