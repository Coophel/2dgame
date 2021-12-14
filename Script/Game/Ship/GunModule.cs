using UnityEngine;

[CreateAssetMenu]
public class GunModule : ScriptableObject
{
	public gunType attackType;
    public int useammo;
    public int damage;
    public int gunHp;
	public int currentHp;
	public int AttackDelay;

	// need sprite controll

#region Public Functions
	public GunModule check(gunType type)
	{
		if (this.attackType == type)
			return this;
		else
			return null;
	}
#endregion
}