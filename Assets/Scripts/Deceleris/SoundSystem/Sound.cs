using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

#if UNITY_EDITOR
using UnityEditor;
#endif

[System.Serializable]
[CreateAssetMenu(menuName = "Content/Sound", fileName = "Sound")]
public class Sound : ScriptableObject
{

    // ========================================================= VARIABLES

    // The sound depend of the sound type, wich allow hierachy volumes
    public AudioMixerGroup audioMixer;

    // When the volume change, we recalculate the final volume, same for pitch
    public float volume = 1;
    public float pitch = 1;

    public List<SoundComponent> soundClips = new List<SoundComponent>();
    public List<SoundSource> sources = new List<SoundSource>();

    // ========================================================= LOCAL

    public void Play()
    {
        foreach (SoundComponent soundClip in soundClips) {

            if (soundClip.clip == null) {
                Debug.Log(this.name + " is missing one or mutliples AudioClips");
                continue;
            }

            SoundSource clipInstance = SoundManager.current.SortInstance(this);
            clipInstance.Play(soundClip);
        }
    }

    // ========================================================= EDITOR

    // This void allow to customize how the variables are displayed in the inspector
#if UNITY_EDITOR
    public void Draw()
    {
        EditorGUILayout.BeginVertical(EditorStyles.helpBox);
        audioMixer = (AudioMixerGroup)EditorGUILayout.ObjectField("Audio Mixer", audioMixer, typeof(AudioMixerGroup), false);
        volume = EditorGUILayout.Slider("Volume", volume, 0, 1);
        pitch = EditorGUILayout.Slider("Pitch", pitch, 0, 2);
        EditorGUILayout.EndVertical();


        EditorGUILayout.BeginVertical(EditorStyles.helpBox);
        for (int i = 0; i < soundClips.Count; i++) {
            SoundComponent clip = soundClips[i];
            EditorGUILayout.BeginVertical(EditorStyles.helpBox);
            EditorGUILayout.BeginHorizontal();
            clip.clip = (AudioClip)EditorGUILayout.ObjectField(clip.clip, typeof(AudioClip), false);
            if (i != 0 && GUILayout.Button("▲", EditorStyles.helpBox, GUILayout.Width(20), GUILayout.Height(17))) { soundClips.RemoveAt(i); soundClips.Insert(i - 1, clip); return; }
            if (GUILayout.Button("x", EditorStyles.helpBox, GUILayout.Width(20))) { soundClips.RemoveAt(i); return; }
            EditorGUILayout.EndHorizontal();
            clip.volume.DrawField("Volume");
            clip.pitch.DrawField("Pitch");
            EditorGUILayout.EndVertical();
        }
        if (soundClips.Count == 0)
            soundClips.Add(new SoundComponent());
        else if (GUILayout.Button("+", EditorStyles.helpBox))
            soundClips.Add(new SoundComponent(soundClips[soundClips.Count - 1]));
        EditorGUILayout.EndVertical();

        EditorGUILayout.BeginHorizontal(EditorStyles.helpBox);
        // Allow to play the sound inside the editor
        GUI.backgroundColor = CEditor.elementSelected;
        if (GUILayout.Button("►", EditorStyles.helpBox, GUILayout.Height(60))) Play();
        GUI.backgroundColor = Color.white;
        if (GUILayout.Button("■", EditorStyles.helpBox, GUILayout.Height(60)))SoundManager.current.StopAll();
        if (GUILayout.Button("X", EditorStyles.helpBox, GUILayout.Height(60)))SoundManager.current.DestroyAll();
        EditorGUILayout.EndHorizontal();

    }
#endif

}

// This block simply call the custom void Draw (), in the target object
#if UNITY_EDITOR
[CustomEditor(typeof(Sound))]
public class SoundEditor : Editor
{
    public override void OnInspectorGUI()
    {
        serializedObject.Update();
        Sound o = target as Sound;
        o.Draw();
        EditorUtility.SetDirty(o);
        serializedObject.ApplyModifiedProperties();
    }
}
#endif