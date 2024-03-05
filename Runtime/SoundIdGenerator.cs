using PixelDust.Audiophile;
using System;
using System.Collections;
using System.Collections.Generic;
#if UNITY_EDITOR
using UnityEditor;
#endif
using UnityEngine;

public class SoundIdGenerator : MonoBehaviour
{
#if UNITY_EDITOR
    public void GenerateUniqueSoundIdForAllSoundEvents() 
    {
        var allSounds = GetAllSoundEventsInProject();
        for (int i = 0; i < allSounds.Count; i++)
        {
            var sound = allSounds[i];
            sound.Data.SoundId = Guid.NewGuid().ToString() + Guid.NewGuid().ToString();

            bool duplicatesNotFound = false;

            while (!duplicatesNotFound)
            {
                bool foundDuplicate = false;
                for (int j = 0; j < allSounds.Count; j++)
                {
                    if (i == j)
                    {
                        continue;
                    }

                    if (allSounds[j].Data.SoundId.Equals(sound.Data.SoundId))
                    {
                        sound.Data.SoundId = Guid.NewGuid().ToString() + Guid.NewGuid().ToString();
                        foundDuplicate = true;
                        break;
                    }
                }

                if (!foundDuplicate)
                {
                    duplicatesNotFound = true;
                }
            }

            EditorUtility.SetDirty(sound);
        }        
    }

    public void GenerateUniqueSoundIdForAllSoundEventCollections()
    {
        var allSounds = GetAllSoundEventsCollectionsInProject();

        for (int i = 0; i < allSounds.Count; i++)
        {
            var sound = allSounds[i];
            sound.Data.SoundId = Guid.NewGuid().ToString() + Guid.NewGuid().ToString();

            bool duplicatesNotFound = false;

            while (!duplicatesNotFound)
            {
                bool foundDuplicate = false;
                for (int j = 0; j < allSounds.Count; j++)
                {
                    if (i == j)
                    {
                        continue;
                    }

                    if (allSounds[j].Data.SoundId.Equals(sound.Data.SoundId))
                    {
                        sound.Data.SoundId = Guid.NewGuid().ToString() + Guid.NewGuid().ToString();
                        foundDuplicate = true;
                        break;
                    }
                }

                if (!foundDuplicate)
                {
                    duplicatesNotFound = true;
                }
            }

            EditorUtility.SetDirty(sound);
        }
    }

    private List<SoundEventPreset> GetAllSoundEventsInProject() 
    {
        string[] guids = AssetDatabase.FindAssets("t:SoundEventPreset", null);
        Debug.LogError($"Found sound asset count: {guids.Length}");
        List<SoundEventPreset> allSounds = new();
        foreach (string guid in guids)
        {
            allSounds.Add(AssetDatabase.LoadAssetAtPath<SoundEventPreset>(AssetDatabase.GUIDToAssetPath(guid)));
        }

        return allSounds;
    }

    private List<SoundEventCollectionPreset> GetAllSoundEventsCollectionsInProject()
    {
        string[] guids = AssetDatabase.FindAssets("t:SoundEventCollectionPreset", null);
        Debug.LogError($"Found sound asset collection count: {guids.Length}");
        List<SoundEventCollectionPreset> allSounds = new();
        foreach (string guid in guids)
        {
            allSounds.Add(AssetDatabase.LoadAssetAtPath<SoundEventCollectionPreset>(AssetDatabase.GUIDToAssetPath(guid)));
        }

        return allSounds;
    }

    public void LookForDuplicates()
    {
        Debug.LogError($"Starting sound search");

        List<SoundEventPreset> allSounds = GetAllSoundEventsInProject();

        Dictionary<string, List<SoundEventPreset>> duplicates = new();
        Debug.LogError($"Found sound asset count: {allSounds.Count}");

        Debug.LogError($"Starting duplicate search");
        for (int i = 0; i < allSounds.Count; i++)
        {
            var sound = allSounds[i];
            string id = sound.Data.SoundId;

            for (int j = 0; j < allSounds.Count; j++)
            {
                if (i == j)
                {
                    continue;
                }

                var item = allSounds[j];
                var itemId = item.Data.SoundId;
                if (itemId.Equals(id))
                {
                    if (!duplicates.ContainsKey(id))
                    {
                        duplicates.Add(id, new List<SoundEventPreset>());
                    }

                    if (!duplicates[id].Contains(sound))
                    {
                        duplicates[id].Add(sound);
                    }

                    if (!duplicates[id].Contains(item))
                    {
                        duplicates[id].Add(item);
                    }
                }
            }
        }

        foreach (var duplicate in duplicates)
        {
            Debug.LogError($"Found duplicate id: {duplicate.Key}: Sounds are");

            foreach (var s in duplicate.Value)
            {
                Debug.LogError($"{s.name}");
            }

            Debug.LogError($"____________________________________________________________");
        }

        Debug.LogError($"Finish duplicate search");
    }

    public void LookForDuplicateCollections()
    {
        Debug.LogError($"Starting sound search");

        List<SoundEventCollectionPreset> allSounds = GetAllSoundEventsCollectionsInProject();

        Dictionary<string, List<SoundEventCollectionPreset>> duplicates = new();
        Debug.LogError($"Found sound asset count: {allSounds.Count}");

        Debug.LogError($"Starting duplicate search");
        for (int i = 0; i < allSounds.Count; i++)
        {
            var sound = allSounds[i];
            string id = sound.Data.SoundId;

            for (int j = 0; j < allSounds.Count; j++)
            {
                if (i == j)
                {
                    continue;
                }

                var item = allSounds[j];
                var itemId = item.Data.SoundId;
                if (itemId.Equals(id))
                {
                    if (!duplicates.ContainsKey(id))
                    {
                        duplicates.Add(id, new List<SoundEventCollectionPreset>());
                    }

                    if (!duplicates[id].Contains(sound))
                    {
                        duplicates[id].Add(sound);
                    }

                    if (!duplicates[id].Contains(item))
                    {
                        duplicates[id].Add(item);
                    }
                }
            }
        }

        foreach (var duplicate in duplicates)
        {
            Debug.LogError($"Found duplicate id: {duplicate.Key}: Sounds are");

            foreach (var s in duplicate.Value)
            {
                Debug.LogError($"{s.name}");
            }

            Debug.LogError($"____________________________________________________________");
        }

        Debug.LogError($"Finish duplicate search");
    }
#endif
}
