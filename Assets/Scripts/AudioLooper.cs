﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioLooper : MonoBehaviour {

	public bool playOnStart = false;
	public float maxVolume;
	public float audioTotalTrackTime;
	bool firstPlay = true;
	public float loopCrossOver;
	public AudioClip toLoop;
	AudioSource[] audioSources = new AudioSource[2];
	float[] curTime = new float[2];
	bool[] playingAudio = new bool[2];


//	bool playingAudio = false;

	public void Start()
	{
		GameObject child0 = new GameObject ();
		child0.transform.SetParent (transform);
		audioSources[0] =  child0.AddComponent<AudioSource> ();
		audioSources[0].clip = toLoop;
		audioSources [0].playOnAwake = false;

		GameObject child1 = new GameObject ();
		child1.transform.SetParent (transform);
		audioSources[1] =  child1.AddComponent<AudioSource> ();
		audioSources[1].clip = toLoop;
		audioSources [1].playOnAwake = false;

		if (playOnStart)
			PlayAudio (0);
	}

	public void Update()
	{
		for (int i = 0; i < 2; i++)
		{
			if (playingAudio [i])
				curTime [i] += Time.deltaTime;

			if (curTime[i] < loopCrossOver)
			{
				audioSources [i].volume = Mathf.Min(curTime [i] / loopCrossOver,maxVolume);
			}
			else if (curTime[i] + loopCrossOver > audioTotalTrackTime)
			{
				audioSources [i].volume = 1 - (curTime [i] - (audioTotalTrackTime-loopCrossOver)) / loopCrossOver - maxVolume;

				if (!playingAudio [((i + 1) % 2)]) {
					PlayAudio ((i + 1) % 2);
				}

				if (curTime [i] > audioTotalTrackTime)
					StopAudio (i);
			}
		}
	}

	private void StopAudio(int track)
	{
		audioSources [track].Stop ();
		curTime [track] = 0;
		playingAudio [track] = false;
	}

	private void PlayAudio(int track)
	{
		//Play there API thing here
		audioSources[track].Play();
		playingAudio [track] = true;
		curTime [track] = 0;
	}
}