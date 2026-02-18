using UnityEngine;

public class CollectibleStarScript : MonoBehaviour
{
    [Header("Movement")]
    [SerializeField] private float starMovementSpeed = 2f;

    [Header("Boundaries")]
    [SerializeField] private float lowestStarY = -8.09f;
    [SerializeField] private float highestStarY = 9.51f;

    [Header("Effects")]
    [SerializeField] private ParticleSystem starBurst;

    [Header("Spawn")]
    public float starSpawnRate = 0.3f;

    private float direction = 1f;
    void Update()
    {

        if (!gameObject.activeSelf) return;

        float star_X = gameObject.transform.position.x;
        float star_Y = gameObject.transform.position.y;
        float star_Z = gameObject.transform.position.z;

        if (star_Y < lowestStarY)
        {
            direction = 1;

        }
        else if (star_Y > highestStarY)
        {
            direction = -1;

        }
        star_Y += starMovementSpeed * Time.deltaTime * direction;
        gameObject.transform.position = new Vector3(star_X, star_Y, star_Z);
    }

    public void Collect()
    { 
        starBurst.Play();
    }

    public void SpawnStarCollectible()
    {
        if (gameObject != null)
        {
            if (Random.Range(0.0f, 1.0f) < starSpawnRate)
            {
                gameObject.SetActive(true);
            }
            else
            {
                gameObject.SetActive(false);
            }
        }
    }
}
