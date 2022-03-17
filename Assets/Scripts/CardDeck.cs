using System;
using System.Collections;
using System.Collections.Generic;
using DefaultNamespace;
using UnityEngine;
using Random = UnityEngine.Random;

public class CardDeck : MonoBehaviour
{
   [SerializeField] private PlayingCard cardPrefab;
   [SerializeField] private CardConfig cardConfig;
   [SerializeField] private Transform showContainer;

   private List<PlayingCard> _cards = new List<PlayingCard>();
   private List<PlayingCard> _allCards = new List<PlayingCard>();
   private int _currentShown = -1;

   private MeshRenderer _meshRenderer;

   private void Awake()
   {
      _meshRenderer = GetComponent<MeshRenderer>();
   }

   private void OnMouseUpAsButton()
   {
      ShowNext();
   }

   public void Initialize()
   {
      GenerateCards();
      _allCards.AddRange(_cards);
      RandomizeDeck();
      
   }

   public void Reset()
   {
      _cards.Clear();
      for (int i = 0; i < _allCards.Count; i++)
      {
         _allCards[i].transform.SetParent(showContainer);
         _allCards[i].Reset();
         _allCards[i].IsInDeck = true;
         _allCards[i].gameObject.SetActive(false);
      }
      _cards.AddRange(_allCards);
   }

   public PlayingCard GetFirstCard()
   {
      if (_cards.Count == 0)
      {
         return null;
      }

      var card = _cards[0];
      _cards.RemoveAt(0);
      card.gameObject.SetActive(true);
      card.IsInDeck = false;
      
      return card;
   }

   public void ExcludeCurrentCard()
   {
      _cards[_currentShown].IsInDeck = false;
      _cards.RemoveAt(_currentShown);

      if (_currentShown + 1 < _cards.Count)
      {
         _currentShown++;
      }
      else
      {
         _meshRenderer.enabled = true;
         _currentShown = -1;
      }
   }
   private void GenerateCards()
   {
      GenerateType(cardConfig.Diamond,CardType.Diamond,CardColor.Red);
      GenerateType(cardConfig.Heart,CardType.Heart,CardColor.Red);
      GenerateType(cardConfig.Spade,CardType.Spade,CardColor.Black);
      GenerateType(cardConfig.Club,CardType.Club,CardColor.Black);
   }

   private void GenerateType(Material[] materials, CardType type, CardColor color)
   {
      for (int i = 0; i < materials.Length; i++)
      {
        PlayingCard newCard =  Instantiate(cardPrefab, showContainer);
        newCard.Initialize(i,color,type,materials[i]);
        newCard.gameObject.SetActive(false);
        newCard.Close();
        _cards.Add(newCard);
      }
   }
   public void RandomizeDeck()
   {
      List<PlayingCard> randomList = new List<PlayingCard>();
      while (_cards.Count > 0)
      {
         int randomIndex = Random.Range(0, _cards.Count);
         randomList.Add(_cards[randomIndex]);
         _cards.RemoveAt(randomIndex);
      }
      _cards.AddRange(randomList);
   }

   private void ShowNext()
   {
      if (_currentShown >= 0)
      {
         _cards[_currentShown].gameObject.SetActive(false);
         _cards[_currentShown].Close();
      }

      _currentShown++;
      
      if (_currentShown == _cards.Count - 1 && _meshRenderer.enabled)
      {
         _cards[_currentShown].gameObject.SetActive(true);
         _cards[_currentShown].Open();
         _meshRenderer.enabled = false;
         return;
      }

      if (_currentShown >= _cards.Count)
      {
         _currentShown = -1;
         _meshRenderer.enabled = true;
      }
      else
      {
         _cards[_currentShown].Open();
      }
   }
}
