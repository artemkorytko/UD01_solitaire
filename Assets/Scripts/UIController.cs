using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIController : MonoBehaviour
{
    [SerializeField] private GameObject gamePanel;
    [SerializeField] private GameObject winPanel;
    [SerializeField] private Button reloadButton;
    [SerializeField] private Button resetButton;

    private GameManager _gameManager;

    private void Awake()
    {
        _gameManager = FindObjectOfType<GameManager>();
    }

    private void Start()
    {
        _gameManager.OnWin += OnWin;
        reloadButton.onClick.AddListener(Reload);
        resetButton.onClick.AddListener(Reset);
    }

    private void Reset()
    {
        _gameManager.Reset();
    }

    private void Reload()
    {
        gameObject.SetActive(true);
        winPanel.SetActive(false);
    }

    

    private void OnWin()
    {
        gameObject.SetActive(false);
        winPanel.SetActive(true);
    }

    private void OnDestroy()
    {
        _gameManager.OnWin -= OnWin;
        reloadButton.onClick.RemoveAllListeners();
        resetButton.onClick.RemoveAllListeners();
    }
}
