using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UiController : MonoBehaviour
{
    [SerializeField] private GameObject mainPanel;
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

    private void OnDestroy()
    {
        _gameManager.OnWin -= OnWin;
        reloadButton.onClick.RemoveListener(Reload);
        resetButton.onClick.RemoveListener(Reset);
    }

    private void Reset()
    {
        _gameManager.DoReset();
    }

    private void Reload()
    {
        mainPanel.SetActive(true);
        winPanel.SetActive(false);
    }

    private void OnWin()
    {
        mainPanel.SetActive(false);
        winPanel.SetActive(true);
    }
}