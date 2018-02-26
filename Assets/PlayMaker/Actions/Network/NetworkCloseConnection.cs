/*
http://www.cgsoso.com/forum-211-1.html

CG搜搜 Unity3d 每日Unity3d插件免费更新 更有VIP资源！

CGSOSO 主打游戏开发，影视设计等CG资源素材。

插件如若商用，请务必官网购买！

daily assets update for try.

U should buy the asset from home store if u use it in your project!
*/

// (c) Copyright HutongGames, LLC 2010-2012. All rights reserved.

#if PLAYMAKER_LEGACY_NETWORK &&  !(UNITY_FLASH || UNITY_NACL || UNITY_METRO || UNITY_WP8 || UNITY_WIIU || UNITY_PSM || UNITY_WEBGL || UNITY_PS3 || UNITY_PS4 || UNITY_XBOXONE)

using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{
	[ActionCategory(ActionCategory.Network)]
	[Tooltip("Close the connection to another system.\n\nConnection index defines which system to close the connection to (from the Network connections array).\n" +
		"Can define connection to close via Guid if index is unknown. \n" +
		"If we are a client the only possible connection to close is the server connection, if we are a server the target player will be kicked off. \n\n" +
		"Send Disconnection Notification enables or disables notifications being sent to the other end. " +
		"If disabled the connection is dropped, if not a disconnect notification is reliably sent to the remote party and there after the connection is dropped.")]
	public class NetworkCloseConnection : FsmStateAction
	{
		
		[Tooltip("Connection index to close")]
		[UIHint(UIHint.Variable)]
		public FsmInt connectionIndex;
		
		[Tooltip("Connection GUID to close. Used If Index is not set.")]
		[UIHint(UIHint.Variable)]
		public FsmString connectionGUID;
		
		[Tooltip("If True, send Disconnection Notification")]
		public bool sendDisconnectionNotification;

		public override void Reset()
		{
			connectionIndex = 0;
			connectionGUID = null;
			
			sendDisconnectionNotification = true;
		}

		public override void OnEnter()
		{
			
			
			int index = 0;
			
			if (!connectionIndex.IsNone)
			{
				index = connectionIndex.Value;
	
			}else if (!connectionGUID.IsNone){
			
				//find the index of this guid.
				int guidIndex;
				
				if ( getIndexFromGUID(connectionGUID.Value,out guidIndex) )
				{
					index = guidIndex;
				}
				
				
			}
			
			if (index < 0 || index > Network.connections.Length)
			{
				LogError("Connection index out of range: " +index);
			}
			else
			{
				Network.CloseConnection(Network.connections[index], sendDisconnectionNotification);
			}
			
			Finish();
		}
		
		bool getIndexFromGUID(string guid,out int guidIndex)
		{
			
			for(int i=0;i<Network.connections.Length;i++)
			{
				if (guid.Equals(Network.connections[i].guid))
				{
					guidIndex = i;
					return true;
				}
			}
			
			guidIndex = 0;
			return false;
		}

	}
}

#endif
