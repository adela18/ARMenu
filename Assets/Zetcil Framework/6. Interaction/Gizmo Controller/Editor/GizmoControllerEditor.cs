using UnityEditor;
using UnityEngine;

namespace Zetcil
{
    [CustomEditor(typeof(GizmoController)), CanEditMultipleObjects]
    public class GizmoControllerEditor : Editor
    {
        public SerializedProperty
           isEnabled,
           TargetCamera,
           IndexMouseButton,
           usingMouseStatus,
           MouseType,
           space,
           type,
           TagObjects,
           showReadOnly,
           debugMouseX,
           debugMouseY,
           debugCoordX,
           debugCoordY,
           debugCoordZ,
           IndexSelection
        ;

        void OnEnable()

        {
            isEnabled = serializedObject.FindProperty("isEnabled");
            TargetCamera = serializedObject.FindProperty("TargetCamera");
            IndexMouseButton = serializedObject.FindProperty("IndexMouseButton");
            usingMouseStatus = serializedObject.FindProperty("usingMouseStatus");
            MouseType = serializedObject.FindProperty("MouseType");
            space = serializedObject.FindProperty("space");
            type = serializedObject.FindProperty("type");
            TagObjects = serializedObject.FindProperty("TagObjects");
            showReadOnly = serializedObject.FindProperty("showReadOnly");
            debugMouseX = serializedObject.FindProperty("debugMouseX");
            debugMouseY = serializedObject.FindProperty("debugMouseY");
            debugCoordX = serializedObject.FindProperty("debugCoordX");
            debugCoordY = serializedObject.FindProperty("debugCoordY");
            debugCoordZ = serializedObject.FindProperty("debugCoordZ");
            IndexSelection = serializedObject.FindProperty("IndexSelection");
        }

        void GUILine(int i_height, string aText)
        {
            GUIStyle style = new GUIStyle();
            style.normal.textColor = Color.white;
            style.richText = true;
            Rect rect = EditorGUILayout.GetControlRect(false, i_height);
            rect.height = i_height;
            EditorGUI.DrawRect(rect, new Color(0.4f, 0.4f, 0.4f, 1));
            EditorGUI.LabelField(rect, " <b>" + aText + "</b>", style);
        }
        public override void OnInspectorGUI()
        {
            serializedObject.Update();
            EditorGUILayout.PropertyField(isEnabled);
            if (isEnabled.boolValue)
            {
                EditorGUILayout.HelpBox("Gizmo Controller Mechanic", MessageType.Info);

                EditorGUILayout.Space(10);
                GUILine(20, "1. Camera Settings");
                EditorGUILayout.PropertyField(TargetCamera, true);
                if (TargetCamera.objectReferenceValue == null)
                {
                    EditorGUILayout.HelpBox("Required Field(s) Null / None", MessageType.Error);
                }

                EditorGUILayout.Space(10);
                GUILine(20, "2. Mouse Settings");
                EditorGUILayout.PropertyField(IndexMouseButton, true);
                EditorGUILayout.PropertyField(usingMouseStatus, true);
                EditorGUILayout.PropertyField(MouseType, true);
                EditorGUILayout.PropertyField(space, true);
                EditorGUILayout.PropertyField(type, true);

                EditorGUILayout.Space(10);
                GUILine(20, "3. GameObject Settings");
                EditorGUILayout.PropertyField(TagObjects, true);
                EditorGUILayout.PropertyField(showReadOnly, true);
                EditorGUILayout.PropertyField(debugMouseX, true);
                EditorGUILayout.PropertyField(debugMouseY, true);
                EditorGUILayout.PropertyField(debugCoordX, true);
                EditorGUILayout.PropertyField(debugCoordY, true);
                EditorGUILayout.PropertyField(debugCoordZ, true);
                EditorGUILayout.PropertyField(IndexSelection, true);
            }
            else
            {
                EditorGUILayout.HelpBox("Prefab Status: Disabled", MessageType.Error);
            }
            serializedObject.ApplyModifiedProperties();
        }

    }
}