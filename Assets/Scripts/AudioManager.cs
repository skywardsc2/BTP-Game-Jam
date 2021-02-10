using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public Sound[] sounds;

	[SerializeField] private bool isEnabled = true;

	private void Awake()
	{
		foreach(Sound s in sounds)
		{
			s.source = gameObject.AddComponent<AudioSource>();

			s.source.clip = s.clip;
			s.source.volume = s.volume;
			s.source.pitch = s.pitch;

			s.source.playOnAwake = s.playOnAwake;
			s.source.loop = s.loop;
		}
	}

	public void Play(string name)
	{
		if (isEnabled)
		{
			Sound s = Array.Find(sounds, sound => sound.name == name);
			s.source.Play();
		}
	}

	public void PlayRandom()
	{
		if (isEnabled)
		{
			int index = UnityEngine.Random.Range(0, sounds.Length);
			Debug.Log("index " + index);

			float randomPitch = ((float)UnityEngine.Random.Range(50, 150)) /100;
			Sound s = sounds[index];

			s.source.pitch = randomPitch;
			s.source.PlayOneShot(s.clip);
			//s.source.Play();
			//sounds[index].source.PlayOneShot(sounds[index].clip);
		}
	}

	public void toggleEnabled()
	{
		if (isEnabled)
		{
			foreach(Sound s in sounds)
			{
				s.source.enabled = false;
			}
			isEnabled = false;
		} else
		{
			foreach (Sound s in sounds)
			{
				s.source.enabled = true;
			}
			isEnabled = true;
		}
	}
}
