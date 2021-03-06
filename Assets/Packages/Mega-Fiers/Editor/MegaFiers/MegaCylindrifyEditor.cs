
using UnityEditor;

[CanEditMultipleObjects, CustomEditor(typeof(MegaCylindrify))]
public class MegaCylindrifyEditor : MegaModifierEditor
{
	public override bool Inspector()
	{
		MegaCylindrify mod = (MegaCylindrify)target;

#if !UNITY_5 && !UNITY_2017 && !UNITY_2018 && !UNITY_2019 && !UNITY_2020
		EditorGUIUtility.LookLikeControls();
#endif
		mod.Percent = EditorGUILayout.FloatField("Percent", mod.Percent);
		mod.Decay = EditorGUILayout.FloatField("Decay", mod.Decay);
		mod.axis = (MegaAxis)EditorGUILayout.EnumPopup("Axis", mod.axis);
		return false;
	}
}
