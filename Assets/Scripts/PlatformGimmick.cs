using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//
// Summary:
//      Pe platforma din taramul de gheata se jucatorul aluneca (dupca ce a ridicat degetul de pe tasta continua se se deplaseze usor
//      in directia respectiva si se opreste greu) (matrial cu frecare mica pe platform)
//      Platformele din tramaul de foc odata atinse devin din ce in ce mai transparente pana dispar de tot (necesita animatie)
//      Din platformele din taramul de padure dupa 2 secunde dupa ce sunt atinse incep sa creasca spini (animatie care dureaza 2 secunde)
//      (necesita animatie noua) dupa care la fiecare secunda da damage jucatorului daca inca mai e pe platforma, nu se dejactiveaza niciodata
//      NECESITA ca platforma sa aiba materialul Ice Physics Material in BoxCollider2D !
public class PlatformGimmick : MonoBehaviour
{
    public float timeAutoDestruction = 5f;
    public float timeUntilSpikes = 2f;
    private bool triggered;
    private float timeNextSpikeDamage;
    public bool spikesActivated;
    public GameObject spike;  // trebuie setat din inspector
    private Collider2D groundTriggerCollider;
    private PlayerController player;
    public enum Realm
    {
        Fire,
        Ice, 
        Forest,
    }

    public Realm realm;  // trebuie setat din inspector

    private void Start()
    {
        player = FindObjectOfType<PlayerController>();
        groundTriggerCollider = player.transform.GetComponentsInChildren<Collider2D>()[1];
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (realm == Realm.Ice)
        {
            player.onIce = true;
            return;
        }

        if (triggered) return;
        if (col.transform.GetComponent<GroundCheck>() == null) return;  // nu e player ul

        Debug.Log($"{transform.name} collided with Player");
        triggered = true;
        switch (realm)
        {
            case Realm.Fire:
                StartCoroutine("FirePlatform");
                break;
            case Realm.Forest:
                StartCoroutine("ForestPlatform");
                break;
            case Realm.Ice:
                break;
            default:
                throw new ArgumentOutOfRangeException();
        }
    }

    private void OnTriggerExit2D(Collider2D col)
    {
        if (realm == Realm.Ice)
            player.onIce = false;
    }

    private void Update()
    {
        if (realm != Realm.Forest) return;

        if (!spikesActivated || timeNextSpikeDamage > Time.time) return;

        var layerMask = LayerMask.GetMask("Ground");
        var hit = Physics2D.Raycast(groundTriggerCollider.bounds.center, Vector2.down, groundTriggerCollider.bounds.extents.y, layerMask);
        if (hit.transform?.name != "Platform Forest") return;
        // TODO: 
        Debug.Log("Player Take Damage");
        timeNextSpikeDamage = Time.time + 1f;
    }

    private IEnumerator FirePlatform()
    {
        var renderer = GetComponent<SpriteRenderer>();
        var albeto = 1f;
        while (albeto >= 0)
        {
            albeto -= 0.25f / timeAutoDestruction;
            renderer.color = new Color(renderer.color.a, renderer.color.g, renderer.color.b, albeto);
            yield return new WaitForSeconds(0.25f);
        }
        Destroy(gameObject);
    }

    private IEnumerator ForestPlatform()
    {
        yield return new WaitForSeconds(timeUntilSpikes);
        var initialLocalScale = new Vector3(0.001f, 0.04f); // de 10 or mai mica decat vreau sa ajunga
        var spikes = new List<GameObject>(9);
        for (var x = -4.5f; x <= 4.5f; x += 1f)
        { 
            spikes.Add(Instantiate(spike, transform.position + new Vector3(x, 0.5f, 0), Quaternion.identity, transform));
            spikes[spikes.Count - 1].transform.localScale = initialLocalScale;
        }

        const float timeToGrow = 2f;
        var newLocalScale = 2 * initialLocalScale;
        var finalLocalScale = 10 * initialLocalScale;
        foreach (var s in spikes)
        {
            s.transform.localScale = newLocalScale;
        }
        while (newLocalScale != finalLocalScale)
        {
            yield return new WaitForSeconds(timeToGrow / 9);
            newLocalScale += initialLocalScale;
            foreach (var s in spikes)
            {
                s.transform.localScale = newLocalScale;
            }
        }

        spikesActivated = true;
    }
}
