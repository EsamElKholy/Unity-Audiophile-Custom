using PixelDust.Audiophile;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(SoundIdGenerator))]
public class SoundIdGeneratorEditor : Editor
{
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();
        SoundIdGenerator soundIdGenerator = target as SoundIdGenerator;

        if (GUILayout.Button("Generate New Sound Ids For All Sound Events"))
        {
            soundIdGenerator.GenerateUniqueSoundIdForAllSoundEvents();
        }

        if (GUILayout.Button("Generate New Sound Ids For All Sound Event Collections"))
        {
            soundIdGenerator.GenerateUniqueSoundIdForAllSoundEventCollections();
        }

        if (GUILayout.Button("Check Duplicate Sound Ids In All Sound Events"))
        {
            soundIdGenerator.LookForDuplicates();
        }

        if (GUILayout.Button("Check Duplicate Sound Ids In All Sound Event Collections"))
        {
            soundIdGenerator.LookForDuplicateCollections();
        }
    }
}
