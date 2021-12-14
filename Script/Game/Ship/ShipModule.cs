using UnityEngine;

[CreateAssetMenu]
public class ShipModule : ScriptableObject
{
	public roomType moduleType;
	// public gunType attackType;
	public bool isSelected;
	public int maxUse;
	[Header("Module status")]
	public int currentUse;
	public int moduleHp;
	public int currentHp;

	// need sprite controll.


#region Public Functions
	public void SetModule()
	{

	}
#endregion
}
