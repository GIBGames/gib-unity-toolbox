using UnityEditor;
using System;

namespace GIB.EditorUtilities
{
	/// <summary>
	/// Launches IMGUI Debugger.
	/// </summary>
	public static class IMGUIDebugger
	{
		static Type type = Type.GetType("UnityEditor.GUIViewDebuggerWindow,UnityEditor");

		[MenuItem("GIB//Fix Box/IMGUI Debugger",false,1)]
		public static void Open() => EditorWindow.GetWindow(type).Show();

	}
}