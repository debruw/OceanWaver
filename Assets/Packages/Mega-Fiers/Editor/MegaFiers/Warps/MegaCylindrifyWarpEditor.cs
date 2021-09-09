
using UnityEditor;

[CanEditMultipleObjects, CustomEditor(typeof(MegaCylindrifyWarp))]
public class MegaCylindrifyWarpEditor : MegaWarpEditor
{
	[MenuItem("GameObject/Create Other/MegaFiers/Warps/Cylindrify")]
	static void CreateStarShape() { CreateWarp("Cylindrify", typeof(MegaCylindrifyWarp)); }

	public override bool Inspector()
	{
		MegaCylindrifyWarp mod = (MegaCylindrifyWarp)target;

#if !UNITY_5 && !UNITY_2017 && !UNITY_2018 && !UNITY_2019 && !UNITY_2020
		EditorGUIUtility.LookLikeControls();
#endif
		mod.Percent = EditorGUILayout.FloatField("Percent", mod.Percent);
		mod.Decay = EditorGUILayout.FloatField("Decay", mod.Decay);
		mod.axis = (MegaAxis)EditorGUILayout.EnumPopup("Axis", mod.axis);
		return false;
	}
}