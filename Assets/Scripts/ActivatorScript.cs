﻿using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.Events;

public class ActivatorScript : MonoBehaviour
{
    public KeyCode Key;
    private SpriteRenderer sr;
    private bool active = false;
    private GameObject note;
    private Color old;
    private GameManager gm;
    private bool inputAllowed = true;
    private ParticleSystem BloodForTheBloodGod;
    void Awake()
    {
        sr = GetComponentInChildren<SpriteRenderer>();
    }

    // Use this for initialization
    void Start()
    {
        gm = FindObjectOfType<GameManager>();
        BloodForTheBloodGod = GameObject.FindGameObjectWithTag("HasiParticles").GetComponent<ParticleSystem>();
        old = sr.color;

    }

    // Update is called once per frame
    void Update()
    {
        if (!inputAllowed) return;

        if (Input.GetKeyDown(Key) && active)
        {
            StartCoroutine(Pressed());
            if (note != null)
                ThrowBack(note);
            active = false;
        }
        else if (Input.GetKeyDown(Key) && !active)
        {
            StartCoroutine(Disable());
        }
    }

    private IEnumerator Disable()
    {
        inputAllowed = false;
        sr.color = Color.red;
        yield return new WaitForSeconds(0.5f);
        sr.color = old;
        inputAllowed = true;
    }

    public UnityAction HasiHit = delegate { };
    public float HurlTime;
    private void ThrowBack(GameObject obj)
    {
        obj.GetComponentInChildren<EggNote>().Speed = 0;
        obj.layer = 10;
        obj.transform.DOMove(new Vector3(0, 1.5f, 0), HurlTime);
        obj.transform.DOScale(new Vector3(0.05f, 0.05f, 0.05f), HurlTime);
        obj.transform.Rotate(Vector3.right,180f);
        StartCoroutine(TriggerHasiHit(obj));
       
    }

    private IEnumerator TriggerHasiHit(GameObject obj)
    {
        yield return new WaitForSeconds(HurlTime);
        BloodForTheBloodGod.Play();
        gm.RequestHasiHit();
        Destroy(obj);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        active = true;
        if (other.gameObject.CompareTag("Note"))
        {
            note = other.gameObject;

        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        active = false;
    }

    private IEnumerator Pressed()
    {
        sr.color = new Color(0, 0, 0);
        yield return new WaitForSeconds(0.05f);
        sr.color = old;
    }
}