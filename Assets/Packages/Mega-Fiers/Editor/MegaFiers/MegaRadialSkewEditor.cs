
using UnityEngine;
using UnityEditor;

[CanEditMultipleObjects, CustomEditor(typeof(MegaRadialSkew))]
public class MegaRadialSkewEditor : MegaModifierEditor
{
	public override string GetHelpString() { return "Radial Skew Modifier by Chris West"; }
	public override Texture LoadImage() { return (Texture)EditorGUIUtility.LoadRequired("MegaFiers\\skew_help.png"); }

	public override bool Inspector()
	{
		MegaRadialSkew mod = (MegaRadialSkew)target;

#if !UNITY_5 && !UNITY_2017 && !UNITY_2018 && !UNITY_2019 && !UNITY_2020
		EditorGUIUtility.LookLikeControls();
#endif
		mod.angle = EditorGUILayout.FloatField("Angle", mod.angle);
		mod.axis = (MegaAxis)EditorGUILayout.EnumPopup("Axis", mod.axis);
		mod.eaxis = (MegaAxis)EditorGUILayout.EnumPopup("Effective Axis", mod.eaxis);
		mod.biaxial = EditorGUILayout.Toggle("Bi Axial", mod.biaxial);
		return false;
	}
}