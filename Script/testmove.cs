using UnityEngine;
using UnityEngine.AI;

public class testmove : MonoBehaviour
{
	[SerializeField]
	Transform target;
    // Start is called before the first frame update
    
	NavMeshAgent agent;
	void Start()
    {
        agent = GetComponent<NavMeshAgent>();
		agent.updateRotation = false;
		agent.updateUpAxis = false;
    }

    // Update is called once per frame
    void Update()
    {
        agent.SetDestination(target.position);
    }
}
