using UnityEditor;

namespace AQUAS
{
	public class AQUAS_RemoveDefine : UnityEditor.AssetModificationProcessor {

		static string symbols;

		public static AssetDeleteResult OnWillDeleteAsset(string assetPath, RemoveAssetOptions rao)
		{

			if (assetPath.Contains ("AQUAS")) 
			{
				symbols = PlayerSettings.GetScriptingDefineSymbolsForGroup(EditorUserBuildSettings.selectedBuildTargetGroup);
			}

			return AssetDeleteResult.DidNotDelete;
		}
	}
}
