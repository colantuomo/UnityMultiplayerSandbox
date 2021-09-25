using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

using Photon.Pun;
using Photon.Realtime;
public class PlayerAgentMovement : MonoBehaviourPunCallbacks, IPunInstantiateMagicCallback
{
    public GameObject clickPoint;
    public float timeToDestroy = 1f;
    public LayerMask groundMask;
    private NavMeshAgent _agent;
    private GameObject lastClickpoint;
    private PhotonView _photonView;
    private float _timeRemaining;
    void Start()
    {
        _agent = GetComponent<NavMeshAgent>();
        _photonView = GetComponent<PhotonView>();
    }

    void Update()
    {
        if (_photonView.IsMine)
        {
            Movement();
        }
    }

    void Movement()
    {
        if (_timeRemaining > 0)
        {
            _timeRemaining -= Time.deltaTime;
        }
        else
        {
            Destroy(lastClickpoint);
        }

        if (Input.GetButtonDown("Fire1"))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out RaycastHit hitInfo, Mathf.Infinity, groundMask))
            {
                if (lastClickpoint == null)
                {
                    lastClickpoint = Instantiate(clickPoint, hitInfo.point, clickPoint.transform.rotation);
                    _timeRemaining = timeToDestroy;
                }
                else
                {
                    lastClickpoint.transform.position = hitInfo.point;
                    _timeRemaining = timeToDestroy;
                }
                _agent.SetDestination(hitInfo.point);
            }
        }
    }

    // Is called whenever a new Photon instantiation was created
    public void OnPhotonInstantiate(PhotonMessageInfo info)
    {
        object[] instantiationData = info.photonView.InstantiationData;
        Vector3 randomColor = (Vector3)instantiationData.GetValue(0);
        SetRandomColor(randomColor);
    }

    private void SetRandomColor(Vector3 randomColor)
    {
        Color color = new Color(randomColor.x, randomColor.y, randomColor.z);
        gameObject.GetComponent<Renderer>().material.SetColor("_Color", color);
    }
}
