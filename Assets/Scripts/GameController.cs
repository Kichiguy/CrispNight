using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{
    #region Private variables
    private bool gameOn;
    private float totalTime;
    #endregion

    #region Game Objects
    public GameObject pauseMenu;
    public GameObject winScreen;
    public GameObject deathScreen;
    public GameObject ambientAudio;
    public Text timeToDieDebug;

    // Torch dying
    public GameObject torch;
    private Vector3 originalTorchScale;
    public Strobe torchlight;

    // Blizzard closing in
    public Camera mainCam;
    private float originalCamSize;
    public ParticleSystem whiteout;
    //public ParticleSystem snowstorm;

    // pan effect on B
    public int interactionCooldown;
    public bool isPanning;
    #endregion

    #region Public Values
    public float timeToDie = 300.0f;
    #endregion
    // Start is called before the first frame update
    void Start()
    {
        interactionCooldown = 0;
        totalTime = timeToDie;
        gameOn = true;
        AudioSource[] AmbientAudios = ambientAudio.GetComponents<AudioSource>();
        foreach (AudioSource audio in AmbientAudios) audio.Play();

        originalTorchScale = torch.transform.localScale;
        originalCamSize = mainCam.orthographicSize;
    }

    // Update is called once per frame
    void Update()
    {
        if (!isPanning)
        {
            interactionCooldown--;
        }

        if (gameOn)
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                PauseGame();
            }
            GameObject actualCam = mainCam.transform.parent.gameObject;
            if (Input.GetAxis("Fire2") > 0 && interactionCooldown <= 0)
            {
                interactionCooldown = 15;
                isPanning = true;
                actualCam.GetComponent<CameraController>().isPanning = true; 
                //var whiteShape = whiteout.shape;
                //whiteShape.radius = 18.0f;
                //whiteShape.radiusThickness = 1.0f;
                //whiteout.
            }

            timeToDie -= Time.deltaTime;
            float normalizedTimeToDie = timeToDie / totalTime;

            if (!isPanning && normalizedTimeToDie > 0)
            {
                float torchRate = Mathf.Log(normalizedTimeToDie + 0.1f, 10) + 1;
                torch.transform.localScale = originalTorchScale * torchRate;
                torchlight._baseIntensity = torchRate;

                float stormRate = 0.4f * Mathf.Log(normalizedTimeToDie + 0.1f, 10) + 0.9f;
                var whiteShape = whiteout.shape;
                whiteShape.radius = 25.0f * stormRate;
                whiteShape.radiusThickness = 1 - stormRate;

                float camRate = 0.7f * stormRate + .35f;
                mainCam.orthographicSize = originalCamSize * camRate;
            }
            else if (isPanning
                && !actualCam.GetComponent<CameraController>().isPanning)
            {
                isPanning = false;
            }

            timeToDieDebug.text = "Time to die: " + Mathf.Ceil(timeToDie).ToString();
            if (timeToDie <= 0) YouHaveDied();
        }
        else
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                UnpauseGame();
            }
        }
    }

    public void PauseGame()
    {
        pauseMenu.SetActive(true);
        gameOn = false;
    }

    public void UnpauseGame()
    {
        pauseMenu.SetActive(false);
        gameOn = true;
    }

    public void YouAreSafe()
    {
        gameOn = false;
        winScreen.SetActive(true);
    }

    public void YouHaveDied()
    {
        Destroy(torch);
        var whiteShape = whiteout.shape;
        whiteShape.radius = 18.0f;
        whiteShape.radiusThickness = 1.0f;
        gameOn = false;
        deathScreen.SetActive(true);
    }

    public bool IsGameOn()
    {
        if (gameOn) return true;
        else return false;
    }
}
