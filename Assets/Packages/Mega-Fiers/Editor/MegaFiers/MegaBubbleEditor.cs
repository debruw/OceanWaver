// This asset was uploaded by http://unityassetcollection.com/
using UnityEngine;
using UnityEditor;

[CanEditMultipleObjects, CustomEditor(typeof(MegaBubble))]
public class MegaBubbleEditor : MegaModifierEditor
{
	public override string GetHelpString() { return "Bubble Modifier by Chris West"; }
	public override Texture LoadImage() { return (Texture)EditorGUIUtility.LoadRequired("MegaFiers\\bubble_help.png"); }

	public override bool Inspector()
	{
		MegaBubble mod = (MegaBubble)target;

#if !UNITY_5 && !UNITY_2017 && !UNITY_2018 && !UNITY_2019 && !UNITY_2020
		EditorGUIUtility.LookLikeControls();
#endif
		mod.radius = EditorGUILayout.FloatField("Radius", mod.radius);
		mod.falloff = EditorGUILayout.FloatField("Falloff", mod.falloff);
		return false;
	}
}