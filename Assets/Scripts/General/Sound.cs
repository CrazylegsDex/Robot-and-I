// Authors:
//	Ordaricc on GitHub
//  Ricky Dev on Youtube
//
// Copyright (c) Novell, Inc. (http://www.novell.com)
//
// Copied from:
//  https://github.com/Ordaricc/Audio-Types
//  All Credit goes to Ordaricc
//

using UnityEngine;

namespace GameMechanics
{
    [System.Serializable]
    public class Sound
    {
        public enum AudioTypes { soundEffect, music, master }
        public AudioTypes audioType;

        [HideInInspector] public AudioSource source;
        public string clipName;
        public AudioClip audioClip;
        public bool isLoop;
        public bool playOnAwake;

        [Range(0, 1)]
        public float volume = 0.5f;
    }
}