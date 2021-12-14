using System.Linq;
using UnityEngine;

namespace Map
{
	public class Mapmanager : MonoBehaviour
	{
		public MapConfig config;
		public Map CurrentMap { get; private set; }

#region  Unity Functions
		private void OnApplicationQuit()
		{
			SaveMap();
		}
#endregion

#region  Public Functions
		public void LoadMap()
		{
			if (PlayerPrefs.HasKey("Map"))
			{
				var mapJson = PlayerPrefs.GetString("Map");
				
				Debug.Log("loading map ... is not complete");
				// load map json   =>  to Map.
				// var map = JsonConvert.DeserializeObject<Map>(mapJson);
				
				// using instead of Contains();
				
				// if (map.path.Any(p => p.Equals(map.GetBossNode().point)))
				// {
				// 	GenerateNewMap();
				// }
				// else
				// {
				// 	// map load complete;
				// 	CurrentMap = map;
				// }
			}
			else
			{
				GenerateNewMap();
			}
		}
		public void SaveMap()
		{
			if (CurrentMap == null)
				return;

			// var json = JsonConvert.SerializeObject(CurrentMap);

		}

		public void GenerateNewMap()
		{
			var map = MapGenerator.GetMap(config);
			CurrentMap = map;
			Debug.Log(CurrentMap.ToJson());
		}
#endregion

#region Private Functions
#endregion
	}
}
