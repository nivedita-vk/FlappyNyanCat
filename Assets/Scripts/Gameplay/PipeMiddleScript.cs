using UnityEngine;

public class PipeMiddleScript : MonoBehaviour
{
    private const int PLAYER_LAYER = 3;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        FlapCatScript flapCat = collision.GetComponent<FlapCatScript>();
        if (collision.gameObject.layer == PLAYER_LAYER && flapCat.birdIsAlive)
        {
            LogicScript.Instance.AddPlayerScore(1);
        }
    }
}
