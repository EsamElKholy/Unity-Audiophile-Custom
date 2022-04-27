﻿using System;
using UnityEditor;
using UnityEngine;

namespace PixelDust.Audiophile
{
    [CustomPropertyDrawer(typeof(SoundEventCollection))]
    public class SoundEventCollectionPropertyDrawer : PropertyDrawer
    {
        public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
        {
            float height = 0;

            //Determine where we are receiving the data from
            //Get the preset asset
            var preset = property.FindPropertyRelative("preset");

            //Get the data since we need to delete its managed reference if using a preset
            //And create its managed reference if not using a preset
            SerializedProperty data = property.FindPropertyRelative("data");
            height += ExtraEditorGUIUtility.SingleLineHeight();

            if (property.isExpanded)
            {
                //Draw Preset Field
                if (preset.objectReferenceValue == null)
                {
                    height += EditorGUI.GetPropertyHeight(data);
                    height += ExtraEditorGUIUtility.SingleLineHeight();
                }
                else
                {
                    height += ExtraEditorGUIUtility.SingleLineHeight();
                }
            }

            return height;
        }

        public override void OnGUI(Rect pos, SerializedProperty property, GUIContent label)
        {
            Rect position = pos;
            EditorGUI.BeginProperty(pos, label, property);

            position.height = EditorGUIUtility.singleLineHeight;

            Rect headerRect = position;
            headerRect.width -= SoundEventPropertyDrawer.PLAY_BUTTON_WIDTH * 2 + SoundEventPropertyDrawer.PLAY_BUTTON_GAP;

            GUIContent newLabel = label;
            Texture2D tex = EditorGUIUtility.Load("se-col.png") as Texture2D;
            newLabel.image = tex;
            property.isExpanded = EditorGUI.BeginFoldoutHeaderGroup(headerRect, property.isExpanded, newLabel);
            EditorGUI.EndFoldoutHeaderGroup();
            position.y += ExtraEditorGUIUtility.SingleLineHeight();

            //Get the data since we need to delete its managed reference if using a preset
            //And create its managed reference if not using a preset
            SerializedProperty dataProp = property.FindPropertyRelative("data");
            SerializedProperty selDataProp = dataProp;
            //Determine where we are receiving the data from
            var preset = property.FindPropertyRelative("preset");


            if (property.isExpanded)
            {
                EditorGUI.indentLevel++;
                {
                    EditorGUI.PropertyField(position, preset, true);
                    position.y += ExtraEditorGUIUtility.SingleLineHeight();

                    if (preset.objectReferenceValue == null)
                    {
                        EditorGUI.PropertyField(position, dataProp, true);
                    }
                }
                EditorGUI.indentLevel--;
            }

            if (preset.objectReferenceValue != null)
            {
                var presetSO = new SerializedObject(preset.objectReferenceValue);
                selDataProp = presetSO.FindProperty("data");
            }

            Rect playPos = pos;
            playPos.height = EditorGUIUtility.singleLineHeight;

            SoundEventPropertyDrawer.DrawPlayStop(playPos, selDataProp, OnPlay, OnStop);

            EditorGUI.EndProperty();
        }

        public static void OnPlay(SerializedProperty dataProp)
        {
            SoundEventCollectionData soundEventCollectionData = null;
            PropertyDrawerUtility.GetTargetObjectOfProperty<SoundEventCollectionData>(dataProp, out soundEventCollectionData);
            soundEventCollectionData?.PlayAt(Vector3.zero);
        }

        public static void OnStop(SerializedProperty dataProp)
        {
            SoundEventCollectionData soundEventCollectionData = null;
            PropertyDrawerUtility.GetTargetObjectOfProperty<SoundEventCollectionData>(dataProp, out soundEventCollectionData);
            SoundManager.StopSound(soundEventCollectionData.SoundId);
        }
    }
}