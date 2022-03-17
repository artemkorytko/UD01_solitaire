using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private const int MinPlaysCompleteValue = 78;

    [SerializeField] private CardPlace[] gameCardPlaces;

    public event Action OnWin;

    private CardDeck _cardDeck;
    private PlayerController _playerController;

    private Dictionary<CardType, int> _cardInfo = new Dictionary<CardType, int>();

    private void Awake()
    {
        _cardDeck = FindObjectOfType<CardDeck>();
        _playerController = FindObjectOfType<PlayerController>();
    }

    private void Start()
    {
        GenerateField();

        _playerController.OnAddToMain += OnAddToMain;
        _playerController.OnRemoveFromMain += OnRemoveFromMain;
    }
    private void GenerateField()
    {
        _cardDeck.Initialize();
        FillGamePlaces();
    }

    private void FillGamePlaces()
    {
        for(int i = 0; i < gameCardPlaces.Length; i++)
        {
            int counter = i;
            var cardPlace = gameCardPlaces[i];
            Card card = null;

            while(counter > 0)
            {
                card = _cardDeck.GetFirstCard();
                card.SetParent(cardPlace);
                cardPlace = card;
                counter--;
            }

            card = _cardDeck.GetFirstCard();
            card.SetParent(cardPlace);
            card.Open();
        }
    }

    private void OnAddToMain(CardType type, int value)
    {
        if(_cardInfo.ContainsKey(type))
        {
            _cardInfo[type] = value;
        }
        else
        {
            _cardInfo.Add(type, value);
        }

        CheckMainPlaces();
    }


    private void OnRemoveFromMain(CardType type, int value)
    {
        if(_cardInfo.ContainsKey(type))
        {
            _cardInfo[type] -= value;
        }
    }
    private void CheckMainPlaces()
    {
        var keys = _cardInfo.Keys;
        if (keys.Count < 4) return;

        bool isWin = true;

        foreach (var key in _cardInfo.Keys)
        {
            if(_cardInfo[key] != MinPlaysCompleteValue)
            {
                isWin = false;
                break;
            }
        }

        if (isWin)
        {
            OnWin?.Invoke();
        }
    }

    public void Reset()
    {
        _cardDeck.Reset();
        _cardDeck.Initialize();
        FillGamePlaces();
        _cardInfo.Clear();
    }


    private void OnDestroy()
    {
        _playerController.OnAddToMain -= OnAddToMain;
        _playerController.OnRemoveFromMain -= OnRemoveFromMain;
    }

}
