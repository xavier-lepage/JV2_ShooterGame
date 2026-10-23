using System.Collections;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    private const float GAME_MUSIC_FADE_OUT_DURATION = 3.0f;

    [SerializeField] private AudioClip gameMusicClip;
    [SerializeField] private AudioClip victoryMusicClip;

    private bool isVictoryMusicStarted = false;

    private AudioSource music;

    public void OnGameWon()
    {
        Debug.Log("You win!");
        StartCoroutine(FadeMusicOut(0.0f));
    }

	private void Start()
    {
		music = GetComponent<AudioSource>();
        music.loop = true;
        music.clip = gameMusicClip;
        music.Play();
        Debug.Log(music.loop);
	}


    private IEnumerator FadeMusicOut(float targetVolume)
    {
        float timer = 0.0f;
        float initialVolume = music.volume;

        while (timer < GAME_MUSIC_FADE_OUT_DURATION)
        {
            timer += Time.deltaTime;
            music.volume = Mathf.Lerp(initialVolume, targetVolume, timer / GAME_MUSIC_FADE_OUT_DURATION);
            yield return null;
        }

        music.loop = false;
        music.volume = 1.0f;
        if (!isVictoryMusicStarted)
        {
			music.Stop();
            music.PlayOneShot(victoryMusicClip);
		}
        isVictoryMusicStarted = true;
    }
}
