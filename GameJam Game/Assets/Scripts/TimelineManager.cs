using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.Timeline;

public class TimelineManager : MonoBehaviour
{
    public PlayableDirector TimelineDirector;
    public TimelineAsset[] TimelineAssets;

    public Transform GetRandomTimelineTransform()
    {
        int index = Random.Range(0,TimelineAssets.Length);
        TimelineDirector.Play(TimelineAssets[index]);

        return TimelineAssets[index].GameObject().transform;
    }
}
