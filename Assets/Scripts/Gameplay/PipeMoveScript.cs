using UnityEngine;

public class PipeMoveScript : MonoBehaviour
{
    [SerializeField] private float despawnX = -181f;
    void Update()
    {
        transform.position = transform.position + (Vector3.left * LogicScript.Instance.pipeMoveSpeed) * Time.deltaTime;
        
        if (transform.position.x < despawnX)
        {
            Destroy(gameObject);
        }
    }
}
