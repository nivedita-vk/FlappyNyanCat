using System;
using UnityEngine;

public class FlapCatScript : MonoBehaviour
{
    [Header("Components")]
    public Rigidbody2D myRigidBody;
    public AudioClip flapSfx;

    [Header("Settings")]
    public float flapStrength = 5f;

    [Header("Boundaries")]
    [SerializeField] private float deathZoneY = -16.04f;

    public bool birdIsAlive = true;
    
   
    void Update()
    {

        if (Input.GetKeyDown(KeyCode.Space) && birdIsAlive && LogicScript.Instance.IsGamePlaying())
        {
            
            myRigidBody.linearVelocity = Vector2.up * flapStrength;
            if (AudioScript.Instance != null)
            {
                AudioScript.Instance.PlaySFX(flapSfx);
            }      
            
        }
        if(transform.position.y <= deathZoneY) {
            LogicScript.Instance.GameOver();
        }

        if(Input.GetKeyDown(KeyCode.Escape) && birdIsAlive)
        {
            if(LogicScript.Instance.gameIsPaused && LogicScript.Instance.IsGamePaused())
            {
                LogicScript.Instance.ResumeGame();
            } else
            {
                LogicScript.Instance.PauseGame();
            }
            
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        birdIsAlive = false;
        LogicScript.Instance.GameOver();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Star") && birdIsAlive)
        {
            LogicScript.Instance.AddPlayerScore(5);
            collision.gameObject.GetComponent<CollectibleStarScript>().Collect();
            Destroy(collision.gameObject, 0.3f);

        }
    }
}
