using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AgentScript : MonoBehaviour
{
	public Ship myship;

	public NPCStatus status;

	// for test code
	int indextarget = 0;

	private NavMeshAgent agent;
	private Animator npcAni;
	private SpriteRenderer npcSprite;

#region Unity Functions
	private void OnEnable()
	{
		npcSprite = GetComponent<SpriteRenderer>();
		agent = GetComponent<NavMeshAgent>();
		npcAni = GetComponent<Animator>();
		myship = GameObject.FindGameObjectWithTag("Ship").GetComponent<Ship>();

		// To not Rotation our NPC.
		agent.updateRotation = false;
		agent.updateUpAxis = false;
	}

	private void Start()
	{
		status.infection = 0;
	}

	// ani controll here
	private void Update()
	{
		// for test code...  1-> right click   ||  next time change Input
		if (Input.GetMouseButtonDown(1))
		{
			// Debug.Log("Now targetindex : " + indextarget);
			indextarget++;
			if (indextarget == myship.moduletransform.Length)
				indextarget = 0;

			agent.SetDestination(myship.moduletransform[indextarget].transform.position);
		}
		SetnpcAni();
	}
#endregion

#region Public Functions
	public void SetnpcAni()
	{
		// 좌우 반전을 위한 실수
		float dis = agent.desiredVelocity.x;

		if (dis < 0)
			npcSprite.flipX = true;
		else
			npcSprite.flipX =false;

		npcAni.SetFloat("Run", agent.remainingDistance);
	}
#endregion

#region Private Functions

#endregion
}
