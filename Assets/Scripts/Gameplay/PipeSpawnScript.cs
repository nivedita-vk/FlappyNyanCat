using UnityEngine;

public class PipeSpawnScript : MonoBehaviour
{
    public GameObject pipe;
    public CollectibleStarScript star;
    
    public float spawnRate = 2;
    private float timer = 0;
    public float heightOffset = 10;

    void Start()
    {
        star = GetComponent<CollectibleStarScript>();
        SpawnObjects();
    }
    void Update()
    {
        if(timer < spawnRate)
        {
            timer += Time.deltaTime;
        } else
        {
            SpawnObjects();
            timer = 0;
        }
            
    }

    void SpawnPipe()
    {
        float lowestPoint = transform.position.y - heightOffset;
        float highestPoint = transform.position.y + heightOffset;

        Instantiate(pipe, new Vector3(transform.position.x, Random.Range(lowestPoint, highestPoint), 0), transform.rotation);
        
    }

    void SpawnObjects()
    {
        SpawnPipe();
        if(star!=null) { star.SpawnStarCollectible();  }
    }
}
