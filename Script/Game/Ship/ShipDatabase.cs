using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class ShipDatabase : ScriptableObject
{
    public List<GunModule> gunModules;
	public List<ShipModule> shipModules;


#region Public Functions
	public GunModule findGun(gunType type)
	{
		foreach(var gunmodule in gunModules)
			return gunmodule.check(type);

		// 초기 값 None
		return gunModules[0];
	}

	public ShipModule findroom(roomType type)
	{
		foreach (var shipmodule in shipModules)
			if (shipmodule.moduleType == type)
				return shipmodule;

		return shipModules[0];
	}
#endregion
}
