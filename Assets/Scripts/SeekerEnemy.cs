using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
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
    public float damage = 1f;
    public float searchArea = 1f;
    public float rayDistance = 2f;
    public LayerMask wallLayer;
    public float countdownSeconds = 2f;
    private float localRayDistance;
    private NavigationAgent _agent;
    private float _timeRemaining;
    private bool canDamage;
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
        if (!canDamage)
        {
            if (_timeRemaining > 0)
            {
                _timeRemaining -= Time.deltaTime;
                canDamage = false;
            }
            else
            {
                canDamage = true;
                _timeRemaining = countdownSeconds;
            }
        }
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
                        if (canDamage)
                        {
                            canDamage = false;
                            hitInfo.transform.GetComponent<PlayerHealth>().Hit(damage);
                        }
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
