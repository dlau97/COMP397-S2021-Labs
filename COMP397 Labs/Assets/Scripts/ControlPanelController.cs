using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ControlPanelController : MonoBehaviour
{
    private RectTransform rectTransform;

    public Vector2 offScreenPos, onScreenPos;
    [Range(0.1f, 10.0f)]
    public float speed = 1.0f;
    public float timer = 0.0f;
    public bool isOnScreen = false;

    [Header("Player Settings")]
    public PlayerBehaviour  player;

    public CameraController playerCamera;

    public Pausable pausable;

    [Header("Scene Data")]
    public SceneDataSO sceneData;

    public  GameObject gameStatePanel;

    // Start is called before the first frame update
    void Start()
    {
        pausable = FindObjectOfType<Pausable>();
        player = FindObjectOfType<PlayerBehaviour>();
        playerCamera = FindObjectOfType<CameraController>();
        rectTransform = GetComponent<RectTransform>();

        rectTransform.anchoredPosition = offScreenPos;
        timer = 0.0f;
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.C)){
            ToggleControlPanel();
        }

        if(isOnScreen){
            MoveControlPanelDown();
            //Cursor.lockState = CursorLockMode.None;
            
        }
        else{
            MoveControlPanelUp();
           // Cursor.lockState = CursorLockMode.Locked;
        }

        
        gameStatePanel.SetActive(pausable.isGamePaused);
        
    }

    void ToggleControlPanel(){
        playerCamera.enabled = isOnScreen;
        isOnScreen = !isOnScreen;
        timer = 0.0f;
    }
    private void MoveControlPanelUp(){
        
        rectTransform.anchoredPosition = Vector2.Lerp(onScreenPos, offScreenPos, timer);
        
        if(timer < 1.0f){
            timer += Time.deltaTime * speed;
        }

        if(pausable.isGamePaused){
            pausable.TogglePause();
        }
    }
    private void MoveControlPanelDown(){
        
        rectTransform.anchoredPosition = Vector2.Lerp(offScreenPos, onScreenPos, timer);
        
        if(timer < 1.0f){
            timer += Time.deltaTime * speed;
        }

    }
    public void OnControlPanelButtonPressed(){
        ToggleControlPanel();
    }

    public void OnLoadButtonPressed(){
        player.controller.enabled = false;
        player.transform.position = sceneData.playerPosition;
        player.transform.rotation = sceneData.playerRotation;
        player.controller.enabled = enabled;

        player.health = sceneData.health;
        player.healthBar.SetHealth(sceneData.health);
    }

    public void OnSaveButtonPressed(){
        sceneData.playerPosition = player.transform.position;
        sceneData.playerRotation = player.transform.rotation;
        sceneData.health = player.health;
    }
}
