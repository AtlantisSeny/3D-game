using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using modelcon;
namespace interfacecon
{
	public interface ISceneController
	{
		void LoadResources();
	}
	public interface IUserAction
	{
		void MoveBoat();
		void Restart();
		void MoveRole(RoleModel role);
		int Check();
	}
}