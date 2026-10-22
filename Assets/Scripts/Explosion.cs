using UnityEngine;

public class Explosion : MonoBehaviour
{
	private ParticleSystem explosionParticles;

	public void Explode()
	{
		explosionParticles.Play();
	}

	public bool isExploding()
	{
		/*	Documentation consultée pour vérifier si les particules d'explosion sont encore actives:
			https://docs.unity.com/en-us/engine/6000.5/script-reference/unityengine/particlesystem/isalive#isalive() */
		return explosionParticles.IsAlive();
	}

	private void Start()
	{
		explosionParticles = GetComponent<ParticleSystem>();
	}
}
