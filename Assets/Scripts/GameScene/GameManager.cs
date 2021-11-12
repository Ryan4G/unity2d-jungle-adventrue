using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : Singleton<GameManager>
{
    public GameObject startingPoint;

    public Rope rope;

    public CameraFollow cameraFollow;

    private StickMan currentStickMan;

    public GameObject stickManPrefab;

    public RectTransform mainMenu;

    public RectTransform gamePlayMenu;

    public RectTransform gameOverMenu;

    public bool stickManInvincible { get; set; }

    public float delayAfterDeath = 1.0f;

    public AudioClip stickManDiedSound;

    public AudioClip gameOverSound;

    private void Start()
    {
        Reset();
    }

    public void Reset()
    {
        if (gameOverMenu)
        {
            gameOverMenu.gameObject.SetActive(false);
        }

        if (mainMenu)
        {
            mainMenu.gameObject.SetActive(false);
        }

        if (gamePlayMenu)
        {
            gamePlayMenu.gameObject.SetActive(true);
        }

        var resetObjects = FindObjectsOfType<Resettable>();

        foreach(Resettable r in resetObjects)
        {
            r.Reset();
        }

        CreateNewStickMan();

        Time.timeScale = 1.0f;
    }

    private void CreateNewStickMan()
    {
        RemoveStickMan();

        GameObject newStickMan = Instantiate(stickManPrefab, startingPoint.transform.position, Quaternion.identity);

        currentStickMan = newStickMan.GetComponent<StickMan>();

        rope.gameObject.SetActive(true);

        rope.connectedObject = currentStickMan.ropeBody;

        rope.ResetLength();

        cameraFollow.target = currentStickMan.cameraFollowTarget;
    }

    private void RemoveStickMan()
    {
        if (stickManInvincible)
        {
            return;
        }

        rope.gameObject.SetActive(false);

        cameraFollow.target = null;

        if (currentStickMan != null)
        {
            currentStickMan.HoldingTreasure = false;

            currentStickMan.gameObject.tag = "Untagged";

            foreach(Transform child in currentStickMan.transform)
            {
                child.gameObject.tag = "Untagged";
            }

            currentStickMan = null;
        }
    }

    private void KillStickMan(StickMan.DamageType type)
    {
        var audio = GetComponent<AudioSource>();

        if (audio)
        {
            audio.PlayOneShot(this.stickManDiedSound);
        }

        currentStickMan.ShowDamageEffect(type);

        if (!stickManInvincible)
        {
            currentStickMan.DestroyStickMan(type);

            RemoveStickMan();

            StartCoroutine(ResetAfterDelay());
        }
    }

    private IEnumerator ResetAfterDelay()
    {
        yield return new WaitForSeconds(delayAfterDeath);

        Reset();
    }

    public void TrapTouched()
    {
        KillStickMan(StickMan.DamageType.Slicing);
    }

    public void FireTrapTouched()
    {
        KillStickMan(StickMan.DamageType.Burning);
    }
    public void TreasureCollected()
    {
        currentStickMan.HoldingTreasure = true;
    }

    public void ExitReached()
    {
        if (currentStickMan != null && currentStickMan.HoldingTreasure)
        {
            var audio = GetComponent<AudioSource>();

            if (audio)
            {
                audio.PlayOneShot(gameOverSound);
            }

            Time.timeScale = 0f;

            if (gameOverMenu)
            {
                gameOverMenu.gameObject.SetActive(true);
            }

            if (gamePlayMenu)
            {
                gamePlayMenu.gameObject.SetActive(false);
            }
        }
    }

    public void SetPaused(bool paused)
    {
        if (paused)
        {
            Time.timeScale = 0f;
            mainMenu.gameObject.SetActive(true);
            gamePlayMenu.gameObject.SetActive(false);
        }
        else
        {
            Time.timeScale = 1f;
            mainMenu.gameObject.SetActive(false);
            gamePlayMenu.gameObject.SetActive(true);
        }
    }

    public void RestartGame()
    {
        Destroy(currentStickMan.gameObject);
        currentStickMan = null;

        Reset();
    }
}
