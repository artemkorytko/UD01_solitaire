using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private const int MinPlaceCompleteValue = 78 /*13 * 4*/;

    [SerializeField] private CardPlace[] cardPlaces;
    
    private CardDeck _cardDeck;
    private PlayerController _playerController;

    private readonly Dictionary<CardType, int> _cardInfo = new Dictionary<CardType, int>();
    
    
    public event System.Action OnWin ;

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

   

    private void OnDestroy()
    {
        _playerController.OnAddToMain -= OnAddToMain;
        _playerController.OnRemoveFromMain -= OnRemoveFromMain;
    }

    public void Reset()
    {
        _cardDeck.Reset();
        _cardDeck.RandomizeDeck();
        FillGamePlaces();
        _cardInfo.Clear();
    }
    
    private void GenerateField()
    {
       _cardDeck.Initialize();
       FillGamePlaces();
    }

    private void FillGamePlaces()
    {
        for (int i = 0; i < cardPlaces.Length; i++)
        {
            int counter = i;
            var cardPlace = cardPlaces[i];
            PlayingCard card = null;

            while (counter > 0)
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
        if (_cardInfo.ContainsKey(type))
        {
            _cardInfo[type] += value;
        }
        else
        {
            _cardInfo.Add(type,value);
        }

        CheckMainPlaces();
        
    }
    private void OnRemoveFromMain(CardType type, int value)
    {
        if (_cardInfo.ContainsKey(type))
        {
            _cardInfo[type] -= value;
        }
        
    }

    private void CheckMainPlaces()
    {
        var keys = _cardInfo.Keys;
        if (keys.Count < 4)
        {
            return;
        }

        bool isWin = true;
        
        foreach (CardType key in _cardInfo.Keys)
        {
            if (_cardInfo[key] != MinPlaceCompleteValue)
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
}
