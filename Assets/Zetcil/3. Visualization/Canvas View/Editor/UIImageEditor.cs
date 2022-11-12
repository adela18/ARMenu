using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

namespace Zetcil
{
    [CustomEditor(typeof(UIRawImage)), CanEditMultipleObjects]
    public class UIImageEditor : Editor
    {

        public SerializedProperty
            isEnabled,
            TargetImage,
            ImageLoad,
            ImageStretch,
            ImageDelay,
            ImageTriggerKey,
            BasePath,
            ImageFileName,
            usingStreamingAssets
            ;

        void OnEnable()
        {
            // Setup the SerializedProperties
            isEnabled = serializedObject.FindProperty("isEnabled");
            TargetImage = serializedObject.FindProperty("TargetImage");
            ImageLoad = serializedObject.FindProperty("ImageLoad");
            ImageStretch = serializedObject.FindProperty("ImageStretch");
            ImageDelay = serializedObject.FindProperty("ImageDelay");
            ImageTriggerKey = serializedObject.FindProperty("ImageTriggerKey");
            BasePath = serializedObject.FindProperty("BasePath");
            ImageFileName = serializedObject.FindProperty("ImageFileName");
            usingStreamingAssets = serializedObject.FindProperty("usingStreamingAssets");
        }

        public override void OnInspectorGUI()
        {
            serializedObject.Update();

            EditorGUILayout.PropertyField(isEnabled);
            if (isEnabled.boolValue)
            {
                EditorGUILayout.PropertyField(TargetImage, true);
                if (TargetImage.objectReferenceValue == null)
                {
                    EditorGUILayout.HelpBox("Required Field(s) Null / None", MessageType.Error);
                }
                EditorGUILayout.PropertyField(ImageStretch, true);

                EditorGUILayout.PropertyField(ImageLoad);
                UIRawImage.CImageLoad st = (UIRawImage.CImageLoad)ImageLoad.enumValueIndex;

                switch (st)
                {
                    case UIRawImage.CImageLoad.ByDelay:
                        EditorGUILayout.PropertyField(ImageDelay, true);
                        break;
                    case UIRawImage.CImageLoad.ByInputKey:
                        EditorGUILayout.PropertyField(ImageTriggerKey, true);
                        break;
                }

                EditorGUILayout.PropertyField(BasePath, true);
                if (BasePath.objectReferenceValue == null)
                {
                    EditorGUILayout.HelpBox("Required Field(s) Null / None", MessageType.Error);
                }
                EditorGUILayout.PropertyField(ImageFileName, true);
                if (ImageFileName.objectReferenceValue == null)
                {
                    EditorGUILayout.HelpBox("Required Field(s) Null / None", MessageType.Error);
                }
                EditorGUILayout.PropertyField(usingStreamingAssets, true);
            }
            else
            {
                EditorGUILayout.HelpBox("Prefab Status: Disabled", MessageType.Error);
            }

            serializedObject.ApplyModifiedProperties();
        }
    }
}