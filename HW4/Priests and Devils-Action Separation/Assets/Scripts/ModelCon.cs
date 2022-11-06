using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace modelcon
{
	public class BoatModel
	{
		GameObject boat;
		Vector3[] start_empty_pos;
		Vector3[] end_empty_pos;
		Click click;
		int boat_sign = 1;
		//站在船上的两个角色
		RoleModel[] roles = new RoleModel[2];
		public float move_speed = 250;
		public GameObject getGameObject() { return boat; }
		public BoatModel()
		{
			//加载船的实体
			boat = Object.Instantiate(Resources.Load("Prefabs/Boat", typeof(GameObject)), new Vector3(-0.2F, 0.05F, -0.35F), Quaternion.identity) as GameObject;
			//为游戏对象起名
			boat.name = "boat";
			//增加构件
			click = boat.AddComponent(typeof(Click)) as Click;
			//设置click对应的boat
			click.SetBoat(this);
			//设置船在起点的上船位置
			start_empty_pos = new Vector3[] { new Vector3(-0.2F, 0.3F, -0.5F), new Vector3(-0.2F, 0.3F, -0.2F) };
			//设置船在终点的上船位置
			end_empty_pos = new Vector3[] { new Vector3(-0.2F, 0.3F, -2.4F), new Vector3(-0.2F, 0.3F, -2.2F) };
		}

		public bool IsEmpty()
		{
			for (int i = 0; i < roles.Length; i++)
			{
				if (roles[i] != null)
					return false;
			}
			return true;
		}

		//改变船的位置
		public Vector3 BoatMove()
		{
			if (boat_sign == -1)
			{
				boat_sign = 1;
				return new Vector3(-0.2F, 0.05F, -0.35F);
				
			}
			else
			{
				boat_sign = -1;
				return new Vector3(-0.2F, 0.05F, -2.2F);
			}
		}

		public int GetBoatSign() { return boat_sign; }

		public RoleModel DeleteRoleByName(string role_name)
		{
			for (int i = 0; i < roles.Length; i++)
			{
				if (roles[i] != null && roles[i].GetName() == role_name)
				{
					RoleModel role = roles[i];
					roles[i] = null;
					return role;
				}
			}
			return null;
		}

		public int GetEmptyNumber()
		{
			for (int i = 0; i < roles.Length; i++)
			{
				if (roles[i] == null)
				{
					return i;
				}
			}
			return -1;
		}

		public Vector3 GetEmptyPosition()
		{
			Vector3 pos;
			if (boat_sign == -1)
				pos = end_empty_pos[GetEmptyNumber()];
			else
				pos = start_empty_pos[GetEmptyNumber()];
			return pos;
		}

		public void AddRole(RoleModel role)
		{
			roles[GetEmptyNumber()] = role;
		}

		public GameObject GetBoat() { return boat; }

		public int[] GetRoleNumber()
		{
			int[] count = { 0, 0 };
			for (int i = 0; i < roles.Length; i++)
			{
				if (roles[i] == null)
					continue;
				if (roles[i].GetSign() == 0)
					count[0]++;
				else
					count[1]++;
			}
			return count;
		}
	}
	public class LandModel
	{
		GameObject land;
		Vector3[] positions;
		int land_sign;
		RoleModel[] roles = new RoleModel[6];
		public LandModel(string land_mark)
		{
			

			//修改地面标记以及对应位置
			if (land_mark == "start")
			{
				//设置牧师和魔鬼站立的位置
				positions = new Vector3[] {new Vector3(0,0.46F,0.49F), new Vector3(0,0.46F,0.735F), new Vector3(0,0.46F,0.988F),
				new Vector3(0,0.46F,1.235F), new Vector3(0,0.46F,1.489F), new Vector3(0,0.46F,1.747F)};
				land = Object.Instantiate(Resources.Load("Prefabs/Ground", typeof(GameObject)), new Vector3(0, 0, (float)1.090), Quaternion.identity) as GameObject;
				land_sign = 1;
			}
			else if (land_mark == "end")
			{
				//设置牧师和魔鬼站立的位置
				positions = new Vector3[] {new Vector3(0,0.46F,-4.25F), new Vector3(0,0.46F,-4.005F), new Vector3(0,0.46F,-3.752F),
				new Vector3(0,0.46F,-3.505F), new Vector3(0,0.46F,-3.251F), new Vector3(0,0.46F,-2.993F)};
				land = Object.Instantiate(Resources.Load("Prefabs/Ground", typeof(GameObject)), new Vector3(0, 0, (float)-3.65), Quaternion.identity) as GameObject;
				land_sign = -1;
			}
		}

		public int GetEmptyNumber()
		{
			for (int i = 0; i < roles.Length; i++)
			{
				if (roles[i] == null)
					return i;
			}
			return -1;
		}

		public int GetLandSign() { return land_sign; }

		public Vector3 GetEmptyPosition()
		{
			Vector3 pos = positions[GetEmptyNumber()];
			pos.x = land_sign * pos.x;
			return pos;
		}

		public void AddRole(RoleModel role)
		{
			roles[GetEmptyNumber()] = role;
		}

		public RoleModel DeleteRoleByName(string role_name)
		{
			for (int i = 0; i < roles.Length; i++)
			{
				if (roles[i] != null && roles[i].GetName() == role_name)
				{
					RoleModel role = roles[i];
					roles[i] = null;
					return role;
				}
			}
			return null;
		}

		public int[] GetRoleNum()
		{
			int[] count = { 0, 0 };
			for (int i = 0; i < roles.Length; i++)
			{
				if (roles[i] != null)
				{
					if (roles[i].GetSign() == 0)
						count[0]++;
					else
						count[1]++;
				}
			}
			return count;
		}
	}
	public class RoleModel
	{
		GameObject role;
		int role_sign;
		Click click;
		bool on_boat;
		public float move_speed = 250;
		LandModel land_model = (SSDirector.GetInstance().CurrentScenceController as Controller).start_land;
		public GameObject getGameObject() { return role; }
		public RoleModel(string role_name)
		{
			if (role_name == "priest")
			{
				role = Object.Instantiate(Resources.Load("Prefabs/Priests", typeof(GameObject)), new Vector3(0, 0, (float)2.090), Quaternion.Euler(0, -90, 0)) as GameObject;
				role_sign = 0;
			}
			else
			{
				role = Object.Instantiate(Resources.Load("Prefabs/Devils", typeof(GameObject)), new Vector3(0, 0, (float)2.090), Quaternion.Euler(0, -90, 0)) as GameObject;
				role_sign = 1;
			}
			click = role.AddComponent(typeof(Click)) as Click;
			click.SetRole(this);
		}
		public int GetSign() { return role_sign; }
		public LandModel GetLandModel() { return land_model; }
		public string GetName() { return role.name; }
		public bool IsOnBoat() { return on_boat; }
		public void SetName(string name) { role.name = name; }
		public void SetPosition(Vector3 pos) { role.transform.position = pos; }
		public void GoLand(LandModel land)
		{
			role.transform.parent = null;
			land_model = land;
			on_boat = false;
		}
		public void GoBoat(BoatModel boat)
		{
			role.transform.parent = boat.GetBoat().transform;
			land_model = null;
			on_boat = true;
		}

	}
}