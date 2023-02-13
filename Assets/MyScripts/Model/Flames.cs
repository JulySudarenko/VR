using System;
using System.Collections;
using UnityEngine;

namespace GeekBrains
{
	public class Flames : MonoBehaviour
	{
		[HideInInspector] public float TimeLife;
		public Light Light;
		public ParticleSystem[] ParticleSystems;
		public GameObject MainObject;

		public event EventHandler<DisableFlamesEventArgs> Message;

		private void Start()
		{
			foreach (var particleSystem in ParticleSystems)
			{
				SetParticleSystem(particleSystem);
			}
		}

		private void SetParticleSystem(ParticleSystem particleSystem)
		{
			if (particleSystem == null) return;
			particleSystem.Stop();
			var main = particleSystem.main;
			main.duration = TimeLife;
		}

		private IEnumerator Attenuation()
		{
			while (true)
			{
				Light.intensity -= 0.3f;
				yield return new WaitForSeconds(0.3f);
			}
		}

		private void Active()
		{
			if (Message != null) Message.Invoke(this, new DisableFlamesEventArgs());
		}

		public void SetActive(bool value)
		{
			if (value)
			{
				MainObject.SetActive(true);
				foreach (var particleSystem in ParticleSystems)
				{
					particleSystem.Play();
				}
				StartCoroutine(Attenuation());
				Light.intensity = TimeLife;
				Invoke("Active", TimeLife);
			}
			else
			{
				foreach (var particleSystem in ParticleSystems)
				{
					particleSystem.Stop();
				}
				StopCoroutine(Attenuation());
				MainObject.SetActive(false);
			}
		}
	}
}