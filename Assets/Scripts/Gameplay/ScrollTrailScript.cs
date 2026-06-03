using UnityEngine;

public class ScrollTrailScript : MonoBehaviour
{
    private TrailRenderer trail;
    void Start()
    {
        trail = GetComponent<TrailRenderer>(); //find trail renderer component thats attached
    }

    void Update()
    {
        float currentSpeed = LogicScript.Instance.pipeMoveSpeed; // get current speed (pipe movement speed) from LogicScript

        Vector3[] positions = new Vector3[trail.positionCount]; // create blank list to hold # of points the trail has
        trail.GetPositions(positions); // copies coordinates of those # of points from trail into the empty list

        for(int i = 0; i < positions.Length; i++)
        {
            positions[i].x -= currentSpeed * Time.deltaTime; // Shift each point left based on the game speed and time passed this frame so it looks pinned to the background

        }

        trail.SetPositions(positions); // take modified list of points and applies it to the actual TrailRenderer component
    }
}
