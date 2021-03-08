using System.Collections;
using System.Collections.Generic;
using UnityEngine;

#if UNITY_EDITOR
using UnityEditor;
#endif

public class StarterMainEffects : StarterEffect
{

    public List<EffectEvent> effectEvents = new List<EffectEvent>();

    public float test;

    public override void Initialise()
    {
        base.Initialise();

        foreach (EffectEvent effectEvent in effectEvents) {
            foreach (EffectEvent.Period period in effectEvent.periods) {
                period.timeBank = period.intervalle;
            }
            foreach (EffectEvent.TimePoint point in effectEvent.timePoints) {
                point.launched = false;
                point.timeBank = 0;
                point.burstRemain = 0;
            }
        }
    }

    public override void Update()
    {
        base.Update();

        foreach (EffectEvent effectEvent in effectEvents) {
            foreach (EffectEvent.Period period in effectEvent.periods) {
                if (period.timeStart <= time && period.timeEnd >= time) {
                    period.timeBank += Time.deltaTime;
                    if (period.timeBank >= period.intervalle) {
                        period.timeBank -= period.intervalle;
                        effectEvent.effect.Play();
                        if (test == 0) test = time;
                    }
                }
            }

            foreach (EffectEvent.TimePoint point in effectEvent.timePoints) {
                if (point.timeStart <= time && !point.launched) {
                    point.launched = true;
                    point.burstRemain = point.burstCount;
                    effectEvent.effect.Play();
                }
                if (point.burstRemain > 0) {
                    point.timeBank += Time.deltaTime;
                    if (point.timeBank >= point.burstDelay) {
                        point.timeBank -= point.burstDelay;
                        point.burstRemain--;
                        effectEvent.effect.Play();
                    }
                }
            }
        }
    }

#if UNITY_EDITOR
    public void Draw()
    {
        if (effectEvents == null) effectEvents = new List<EffectEvent>();
        if (effectEvents.Count == 0) effectEvents.Add(new EffectEvent());

        for (int i = 0; i < effectEvents.Count; i++) {

            EffectEvent effectEvent = effectEvents[i];

            EditorExtensions.DrawInLayout (false, "box", CEditor.window, true, false, () => {

                EditorGUILayout.BeginHorizontal();
                GUILayout.Label("Effect " + (i + 1));
                if (EditorExtensions.DrawColoredButton ("+", CEditor.element, 20)) { effectEvents.Add(new EffectEvent(effectEvent)); }
                if (EditorExtensions.DrawColoredButton ("-", CEditor.element, 20)) { effectEvents.Remove(effectEvent); }
                EditorGUILayout.EndHorizontal();

                EditorExtensions.DrawInLayout(false, "box", CEditor.innerWindow, true, false, () => {
                    effectEvent.effect = (ScreenEffect)EditorGUILayout.ObjectField("Effect", effectEvent.effect, typeof(ScreenEffect), false);
                });

                EditorExtensions.DrawInLayout(false, "box", CEditor.innerWindow, true, false, () => {
                    GUILayout.Label("Periods (Start | End | Intervalle)");
                    if (effectEvent.periods.Count == 0) {
                        if (EditorExtensions.DrawColoredButton("Add", CEditor.element)) { effectEvent.periods.Add(new EffectEvent.Period()); }
                    }
                    else {
                        for (int j = 0; j < effectEvent.periods.Count; j++) {
                            EffectEvent.Period period = effectEvent.periods[j];
                            EditorGUILayout.BeginHorizontal();
                            period.timeStart = EditorGUILayout.FloatField(period.timeStart);
                            period.timeEnd = EditorGUILayout.FloatField(period.timeEnd);
                            period.intervalle = EditorGUILayout.FloatField(period.intervalle);
                            if (EditorExtensions.DrawColoredButton("+", CEditor.element, 20)) { effectEvent.periods.Add(new EffectEvent.Period(period)); }
                            if (EditorExtensions.DrawColoredButton("-", CEditor.element, 20)) { effectEvent.periods.Remove(period); }
                            EditorGUILayout.EndHorizontal();
                        }
                    }
                });

                EditorExtensions.DrawInLayout(false, "box", CEditor.innerWindow, true, false, () => {
                    GUILayout.Label("Time Points (Start | Burst ? | Count | Delay)");
                    if (effectEvent.timePoints.Count == 0) {
                        if (EditorExtensions.DrawColoredButton("Add", CEditor.element)) { effectEvent.timePoints.Add(new EffectEvent.TimePoint()); }
                    } else {
                        for (int j = 0; j < effectEvent.timePoints.Count; j++) {
                            EffectEvent.TimePoint timePoint = effectEvent.timePoints[j];
                            EditorGUILayout.BeginHorizontal();
                            timePoint.timeStart = EditorGUILayout.FloatField(timePoint.timeStart);
                            if (EditorExtensions.DrawColoredButton("Burst", timePoint.burst ? CEditor.elementSelected : CEditor.element)) timePoint.burst = !timePoint.burst;
                            if (timePoint.burst) {
                                timePoint.burstCount = EditorGUILayout.IntField(timePoint.burstCount);
                                timePoint.burstDelay = EditorGUILayout.FloatField(timePoint.burstDelay);
                            }
                            if (EditorExtensions.DrawColoredButton("+", CEditor.element, 20)) { effectEvent.timePoints.Add(new EffectEvent.TimePoint(timePoint)); }
                            if (EditorExtensions.DrawColoredButton("-", CEditor.element, 20)) { effectEvent.timePoints.Remove(timePoint); }
                            EditorGUILayout.EndHorizontal();
                        }
                    }
                });
            });
        }
    }
#endif

    [System.Serializable]
    public class EffectEvent
    {

        public ScreenEffect effect;

        // ******** PERIOD
        public List<Period> periods = new List<Period>();

        [System.Serializable]
        public class Period
        {
            public float timeStart;
            public float timeEnd;
            public float intervalle;

            public float timeBank;

            public Period() { }

            public Period(Period other)
            {
                this.timeStart = other.timeStart;
                this.timeEnd = other.timeEnd;
                this.intervalle = other.intervalle;
            }
        }

        // ******** PONCTUAL
        public List<TimePoint> timePoints = new List<TimePoint>();

        [System.Serializable]
        public class TimePoint
        {
            public float timeStart;
            public bool burst;
            public int burstCount;
            public float burstDelay;

            public bool launched;
            public float timeBank;
            public int burstRemain;

            public TimePoint() { }

            public TimePoint(TimePoint other) {
                this.timeStart = other.timeStart;
                this.burst = other.burst;
                this.burstCount = other.burstCount;
                this.burstDelay = other.burstDelay;
            }
        }

        public EffectEvent () { }

        public EffectEvent (EffectEvent other)
        {
            this.effect = other.effect;

            periods = new List<Period>();
            foreach (Period otherPeriod in other.periods) { periods.Add(new Period(otherPeriod)); }

            timePoints = new List<TimePoint>();
            foreach (TimePoint otherPoint in other.timePoints) { timePoints.Add(new TimePoint(otherPoint)); }
        }
    }
}

#if UNITY_EDITOR
[CanEditMultipleObjects]
[CustomEditor(typeof(StarterMainEffects))]
public class StarterMainPostProcessEditor : Editor
{
    public override void OnInspectorGUI()
    {
        serializedObject.Update();
        StarterMainEffects o = target as StarterMainEffects;
        o.Draw();
        EditorUtility.SetDirty(o);
        serializedObject.ApplyModifiedProperties();
    }
}
#endif