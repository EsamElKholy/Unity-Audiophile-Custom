﻿using System;
using UnityEngine;
using WeightedObjects;

namespace PixelDust.Audiophile
{
    [System.Serializable]
    public class SoundEventData
    {
        [SerializeField]
        private WeightedObjectCollection<AudioClip> audioClips = new WeightedObjectCollection<AudioClip>();
        public WeightedObjectCollection<AudioClip> AudioClips => audioClips;

        [SerializeField]
        private bool loop = false;
        public bool Loop => loop;

        [SerializeField]
        private string soundId;
        public string SoundId { get { return soundId; } set { soundId = value; } }

        [SerializeField]
        private StandardSettings standardSettings = new StandardSettings();
        public StandardSettings StandardSettings => standardSettings;

        [SerializeField]
        private AdvancedSettings advancedSettings = new AdvancedSettings();
        public AdvancedSettings AdvancedSettings => advancedSettings;

        [SerializeField]
        private SpatialSettings spatialSettings = new SpatialSettings();
        public SpatialSettings SpatialSettings => spatialSettings;

        public void Reset()
        {
            this.standardSettings.Reset();
            this.advancedSettings.Reset();
            this.spatialSettings.Reset();
        }

        public void Stop()
        {
            SoundManager.StopSound(this.SoundId);
        }

        public AudiophilePlayResult PlayAt(Vector3 position, float delay = 0, string overrideId = null, Transform followTransform = null)
        {
            return SoundManager.ProcessSound(this, position, delay, overrideId, followTransform);
        }

        public AudiophilePlayResult PlayAt(Transform transform, float delay = 0, string overrideId = null)
        {
            Vector3 position = transform != null ? transform.position : Vector3.zero;
            return PlayAt(position, delay, overrideId);
        }

        public AudiophilePlayResult Play(float delay = 0, string overrideId = null)
        {
            return PlayAt(Vector3.zero, delay, overrideId);
        }

        public SoundEventData()
        {
            Reset();
        }
    }

    [System.Serializable]
    public class StandardSettings
    {
        [Range(0, 2)]
        [SerializeField]
        private float minPitch = 1;
        public float MinPitch => minPitch;

        [Range(0, 2)]
        [SerializeField]
        private float maxPitch = 1;
        public float MaxPitch => maxPitch;

        [Range(0, 1)]
        [SerializeField]
        private float minVolume = 1;
        public float MinVolume => minVolume;

        [Range(0, 1)]
        [SerializeField]
        private float maxVolume = 1;
        public float MaxVolume => maxVolume;

        [Range(0, 256)]
        [SerializeField]
        private int priority = 128;
        public int Priority => priority;

        [SerializeField]
        private UnityEngine.Audio.AudioMixerGroup group;
        public UnityEngine.Audio.AudioMixerGroup Group => group;

        public void Reset()
        {
            this.minVolume = 1;
            this.maxVolume = 1;
            this.minPitch = 1;
            this.maxPitch = 1;
            this.priority = 128;
        }
    }

    [System.Serializable]
    public class SpatialSettings
    {
        #region 2DSettings
        [SerializeField]
        [Range(-1, 1)]
        private float stereoPanMin = 0;
        public float StereoPanMin => stereoPanMin;

        [SerializeField]
        [Range(-1, 1)]
        private float stereoPanMax = 0;
        public float StereoPanMax => stereoPanMax;
        #endregion

        #region 3DSettings
        [SerializeField]
        private bool is3D = false;
        public bool Is3D => is3D;

        [SerializeField]
        [Range(0, 5)]
        private float dopplerLevel = 1;
        public float DopplerLevel => dopplerLevel;

        [SerializeField]
        [Range(0, 360)]
        private float spread = 0;
        public float Spread => spread;

        [SerializeField]
        private AudioRolloffMode volumeRolloff;
        public AudioRolloffMode VolumeRolloff => volumeRolloff;

        [SerializeField]
        private float minDistance = 1;
        public float MinDistance => minDistance;

        [SerializeField]
        private float maxDistance = 500;
        public float MaxDistance => maxDistance;

        public void Reset()
        {
            this.maxDistance = 500;
            this.minDistance = 1;
            this.spread = 0;
            this.dopplerLevel = 1;
            this.is3D = false;
        }
        #endregion
    }

    [System.Serializable]
    public class AdvancedSettings
    {
        [SerializeField]
        private bool bypassEffects = false;
        public bool BypassEffects => bypassEffects;

        [SerializeField]
        private bool bypassListenerEffects = false;
        public bool BypassListenerEffects => bypassListenerEffects;

        [SerializeField]
        private bool bypassReverbZones = false;
        public bool BypassReverbZones => bypassReverbZones;

        [SerializeField]
        [Range(0, 1.1f)]
        private float reverbZoneMix = 1;
        public float ReverbZoneMix => reverbZoneMix;

        public void Reset()
        {
            this.reverbZoneMix = 1;
        }
    }
}