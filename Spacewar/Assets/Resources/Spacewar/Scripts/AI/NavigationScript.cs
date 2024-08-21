using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NavigationScript : MonoBehaviour
{
    public Transform _player;
    private UnityEngine.AI.NavMeshAgent agent;

    // public Transform Player{
    //     get => _player;
    //     set => _player = value;
    // }

    // Start is called before the first frame update
    void Start()
    {
        agent = GetComponent<UnityEngine.AI.NavMeshAgent>();
    }

    // Update is called once per frame
    void Update()
    {
        agent.destination = _player.position;
    }
}
