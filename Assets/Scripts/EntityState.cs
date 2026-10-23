using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class EntityState : MonoBehaviour
{
    [SerializeField] private AudioClip deathSound;
    [SerializeField] private GameObject head;
    [SerializeField] private GameObject body;
    private AudioSource audioSource;
    private Collider collider;
    private Rigidbody rigidbody;
    private bool isDead = false;

    void Start()
    {
        Setup();
    }

    protected virtual void Setup()
    {
        rigidbody = GetComponent<Rigidbody>();
        audioSource = gameObject.GetComponent<AudioSource>();
        collider = gameObject.GetComponent<Collider>();
    }

    public void StartDeathCoroutine()
    {
        if (isDead) return;
        isDead = true;

        StartCoroutine(DeathCourotine());
    }

    protected virtual IEnumerator DeathCourotine()
    {
        SetDeathState(true);
        if (rigidbody != null) rigidbody.linearVelocity = Vector3.zero;
        if (audioSource != null) audioSource.PlayOneShot(deathSound);
        gameObject.transform.DetachChildren();
        ActivateBodyPart(head);
        ActivateBodyPart(body);

        yield return new WaitForSeconds(deathSound.length);

        SetDeathState(false);
        gameObject.SetActive(false);
        isDead = false;
    }

    protected virtual void SetDeathState(bool state)
    {
        if (rigidbody != null) rigidbody.useGravity = !state;
        if (collider != null) collider.enabled = !state;
        /*foreach (Renderer renderer in renderers)
        {
            renderer.enabled = !state;
        }*/
    }

    private void ActivateBodyPart(GameObject part)
    {
        part.AddComponent<Rigidbody>();
        Rigidbody rb = part.GetComponent<Rigidbody>();
        Collider col = part.GetComponent<Collider>();

        if (part == head)
        {
            rb.linearVelocity = part.transform.up * 15f;
        } 
        else
        {
            rb.linearVelocity = part.transform.forward * 20f;
        }
        rb.useGravity = true;

        col.enabled = true;
    }
}