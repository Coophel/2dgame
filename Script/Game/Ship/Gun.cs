using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : MonoBehaviour
{
    public gunType attackType;
    public int useammo;
    public int damage;
    public int gunHp;
	public int currentHp;
	public int AttackDelay;

	Gun(gunType attackType, int useammo, int damage, int gunHp, int currentHp, int AttackDelay)
	{
		this.attackType = attackType;
		this.useammo = useammo;
		this.damage = damage;
		this.gunHp = gunHp;
		this.currentHp = currentHp;
		this.AttackDelay = AttackDelay;
	}

	Gun(GunModule temp)
	{
		this.attackType = temp.attackType;
		this.useammo = temp.useammo;
		this.damage = temp.damage;
		this.gunHp = temp.gunHp;
		this.currentHp = temp.currentHp;
		this.AttackDelay = temp.AttackDelay;
	}
#region Public Functions
	public void SetGun(GunModule temp)
	{
		this.attackType = temp.attackType;
		this.useammo = temp.useammo;
		this.damage = temp.damage;
		this.gunHp = temp.gunHp;
		this.currentHp = temp.currentHp;
		this.AttackDelay = temp.AttackDelay;
	}
#endregion
}
