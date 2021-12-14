using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Room : MonoBehaviour
{
    public roomType moduleType;
	// public gunType attackType;
	public bool isSelected;
	public int maxUse;
	[Header("Module status")]
	public int currentUse;
	public int moduleHp;
	public int currentHp;

#region Public Functions
	// 방 설정
	public void SetRoom(ShipModule module)
	{
		isSelected = false;
		moduleType = module.moduleType;
		maxUse = module.maxUse;
		currentUse = module.currentUse;
		moduleHp = module.moduleHp;
		currentHp = module.currentHp;
	}
#endregion
#region Private Functions
	// 방에 NPC 가 들어오는 것 체크 일하게 시키는 것 체크
	private void OnTriggerEnter2D(Collider2D other)
	{
		Debug.Log("trigger in " + moduleType.ToString());
		if (currentUse == maxUse)
			return ;
		if (other.gameObject.CompareTag("NPC"))
		{
			other.gameObject.GetComponent<Animator>().SetBool("Work", true);
			currentUse++;
		}
	}

	private void OnTriggerExit2D(Collider2D other)
	{
		Debug.Log("trigger out " + moduleType.ToString());
		if (other.gameObject.CompareTag("NPC") && currentUse > 0)
		{
			other.gameObject.GetComponent<Animator>().SetBool("Work", false);
			currentUse--;
		}
	}
#endregion
}
