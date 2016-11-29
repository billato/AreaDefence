using UnityEngine;
using System.Collections;

public class AgentPath : MonoBehaviour {

	public Transform targetExit;
	NavMeshAgent agent;

	// Use this for initialization
	void Start () {
		agent = GetComponent<NavMeshAgent> ();
		targetExit = GameObject.Find("NavMeshAgentTarget").transform;

	}
	
	// Update is called once per frame
	void Update () {
		agent.SetDestination (targetExit.position);



		if (!agent.pathPending){
			if (agent.remainingDistance <= agent.stoppingDistance){
				if (!agent.hasPath || agent.velocity.sqrMagnitude == 0f){
					// Done
					//currentPoint = 0;
					Destroy(this.gameObject);
		        }
		    }
		}
	}



}
