using UnityEngine;

public class SpaceshipMover : MonoBehaviour
{
    public Transform startPoint;
    public Transform endPoint;
    public Transform player;
    public Light spotlight;

    public float floatSpeed = 0.5f;
    public float rotationSpeed = 30f;
    public float followSpeed = 3f;

    private float elapsedTime = 0f;
    private float speed;
    private bool isMoving = false;
    private bool hasArrived = false;
    private bool isFollowing = false;

    void Start()
    {
        if (startPoint != null)
        {
            transform.position = startPoint.position;
        }
        else
        {
            Debug.LogError("Start Point not assigned!");
        }

        if (spotlight != null)
        {
            spotlight.enabled = false;
        }
    }

    void Update()
    {
        if (isMoving && !hasArrived)
        {
            elapsedTime += Time.deltaTime;
            float step = speed * Time.deltaTime; // ✅ Moves at the correct speed
            transform.position = Vector3.MoveTowards(transform.position, endPoint.position, step);

            if (Vector3.Distance(transform.position, endPoint.position) < 0.01f)
            {
                hasArrived = true;
                isMoving = false;
            }
        }

        if (hasArrived && !isFollowing)
        {
            transform.position += new Vector3(0, Mathf.Sin(Time.time * floatSpeed) * Time.deltaTime, 0);
            transform.Rotate(Vector3.up * rotationSpeed * Time.deltaTime);
        }

        if (isFollowing && player != null)
        {
            transform.position = Vector3.Lerp(transform.position, player.position + new Vector3(0, 25, 0), Time.deltaTime * followSpeed);
        }
    }

    public void StartMoving()
    {
        if (!isMoving && !hasArrived)
        {
            float distance = Vector3.Distance(startPoint.position, endPoint.position);
            speed = distance / 10f; 

            elapsedTime = 0f;
            isMoving = true;
        }
    }

    public void FollowPlayer()
    {
        if (player == null)
        {
            Debug.LogError("No player assigned to follow!");
            return;
        }


        isFollowing = true;
        hasArrived = false;

        if (spotlight != null)
        {
            spotlight.enabled = true;
        }
    }
}
