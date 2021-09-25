using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class RayDirection
{
    public Vector3 direction { get; set; }
    public float lastDistance { get; set; }
    public RayDirection(Vector3 direction, float lastDistance)
    {
        this.direction = direction;
        this.lastDistance = lastDistance;
    }
}
public class SeekerEnemy : MonoBehaviour
{
    public float searchArea = 1f;
    public float rayDistance = 2f;
    public LayerMask wallLayer;
    public float timeRemaining = 2f;
    private float localRayDistance;
    private NavigationAgent _agent;
    List<RayDirection> rays = new List<RayDirection>();
    void Start()
    {
        _agent = GetComponent<NavigationAgent>();
        localRayDistance = rayDistance;
    }

    void Update()
    {
        // Vector3 rightRay = transform.forward - transform.right;
        // Vector3 leftRay = transform.forward - (-transform.right);
        // RayDirection rayDirectionL = new RayDirection(leftRay, rayDistance);
        // RayDirection rayDirectionR = new RayDirection(rightRay, rayDistance);
        // RayDirection rayDirectionF = new RayDirection(transform.forward, rayDistance);
        // SearchForEnemies(rayDirectionL);
        // SearchForEnemies(rayDirectionR);
        // SearchForEnemies(rayDirectionF);
        SearchForPlayerSphere();
    }

    void SearchForEnemies(RayDirection rayDirection)
    {
        if (Physics.Raycast(transform.position, rayDirection.direction, out RaycastHit hitInfo, rayDirection.lastDistance))
        {
            rayDirection.lastDistance = hitInfo.distance;
            if (hitInfo.transform.CompareTag("Player"))
            {
                _agent.SetDestination(hitInfo.transform.position);
            }
            Debug.Log(hitInfo.transform.name);
        }
        else
        {
            rayDirection.lastDistance = rayDistance;
        }
        Debug.DrawRay(transform.position, rayDirection.direction * rayDirection.lastDistance, Color.red);
    }

    void SearchForPlayerSphere()
    {
        Collider[] items = Physics.OverlapSphere(transform.position, searchArea, LayerMask.GetMask("Walls", "Player"));
        foreach (Collider item in items)
        {
            if (item.CompareTag("Player"))
            {
                var dir = item.transform.position - transform.position;
                Debug.DrawRay(transform.position, dir * 5f, Color.red);
                if (Physics.Raycast(transform.position, dir, out RaycastHit hitInfo))
                {
                    if (hitInfo.transform.CompareTag("Player"))
                    {
                        _agent.IsChasingPlayer = true;
                        _agent.SetDestination(hitInfo.transform.position);
                    }
                    else
                    {
                        _agent.IsChasingPlayer = false;
                    }
                }
            }
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, searchArea);
    }
}
