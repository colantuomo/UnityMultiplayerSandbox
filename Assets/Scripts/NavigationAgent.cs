using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

using Photon.Pun;
using Photon.Realtime;
public class NavigationAgent : MonoBehaviourPunCallbacks
{
    public float countdownTimer;
    public bool IsChasingPlayer;
    // public Transform plane;
    [SerializeField] private float baseSpeed = 5f;
    [SerializeField] private float chasingSpeed = 6.5f;
    private NavMeshAgent _agent;
    private float _timeRemaining;
    private Vector3 _planeBounds;
    private Vector3 _target;
    private bool _reachedLastPosition;
    private bool _isSearchingPlayer;
    private void Awake()
    {
        _target = transform.position;
        _timeRemaining = countdownTimer;
        _agent = GetComponent<NavMeshAgent>();
        _planeBounds = GameObject.FindGameObjectWithTag("Ground").GetComponent<MeshCollider>().bounds.extents;

    }

    private void Update()
    {
        _agent.speed = baseSpeed;
        float MIN_DISTANCE = 2f;
        float distance = Vector3.Distance(transform.position, _target);
        // Debug.Log(distance);
        _reachedLastPosition = false;
        if (distance <= MIN_DISTANCE)
        {
            _reachedLastPosition = true;
        }
        if (_target == null || _reachedLastPosition)
        {
            MoveToNextDestination();
            // TimerCount();
        }
        if (IsChasingPlayer)
        {
            _agent.speed = chasingSpeed;
        }
    }

    void TimerCount()
    {
        if (_timeRemaining > 0)
        {
            _timeRemaining -= Time.deltaTime;
        }
        else
        {
            _timeRemaining = countdownTimer;
            MoveToNextDestination();
        }
    }

    void MoveToNextDestination()
    {
        float xBound = Random.Range(-_planeBounds.x, _planeBounds.x);
        float zBound = Random.Range(-_planeBounds.z, _planeBounds.z);
        Vector3 nextDestination = new Vector3(xBound, 0f, zBound);
        _target = nextDestination;
        SetDestination(_target);
    }

    public void SetDestination(Vector3 destination)
    {
        _target = destination;
        _agent.SetDestination(destination);
    }
}
