using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainResource : MonoBehaviour
{
    [Header("Out Game Resource")]
	public int Diamond;
	[Header("In Game Resource")]
	// public List<AItem> ancientItem;
	public float food;
	public int energy;
	public int ammo;

#region Public Functions
	// 새로운 게임 시작시 자원 설정.
	public void initializeNewGame()
	{
		food = 10f;
		energy = 10;
		ammo = 100;
	}
	// 참으로 리턴시 발사하는 함수와 같이 쓸 것 (조건확인 및 소모)
	public bool useGun(/*t_gun mygun*/ int ammo)
	{
		return (true);
	}
	// 선택지로 갈 곳을 정하고 물자 계산을 하는 함수
	public void passday(int people)
	{
		// 맵 선택이 되어서 이동 하는지
		// if (we move)
		//     energy += -1 * distains;
		food -= people;
	}
#endregion
}
