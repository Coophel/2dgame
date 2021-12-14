using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCManager : MonoBehaviour
{
	public static NPCManager Instance;
	public GameObject[] npcs;


#region Unity Functions
	private void Awake()
	{
		Instance = this;
		
	}
#endregion
}
