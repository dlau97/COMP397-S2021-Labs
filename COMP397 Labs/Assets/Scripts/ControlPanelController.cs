using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ControlPanelController : MonoBehaviour
{
    private RectTransform rectTransform;

    public Vector2 offScreenPos, onScreenPos;
    [Range(0.1f, 10.0f)]
    public float speed = 1.0f;
    public float timer = 0.0f;
    public bool isOnScreen = false;

    public CameraController playerCamera;

    public Pausable pausable;

    // Start is called before the first frame update
    void Start()
    {
        pausable = FindObjectOfType<Pausable>();
        playerCamera = FindObjectOfType<CameraController>();
        rectTransform = GetComponent<RectTransform>();

        rectTransform.anchoredPosition = offScreenPos;
        timer = 0.0f;
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.C)){
            playerCamera.enabled = isOnScreen;
            isOnScreen = !isOnScreen;
            timer = 0.0f;
        }

        if(isOnScreen){
            MoveControlPanelDown();
            Cursor.lockState = CursorLockMode.None;
            
        }
        else{
            MoveControlPanelUp();
            Cursor.lockState = CursorLockMode.Locked;
        }
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
}
