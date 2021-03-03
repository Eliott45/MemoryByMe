using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MemoryCard : MonoBehaviour
{
    [SerializeField] private GameObject cardBack;
    public void OnMouseDown() { // Эта функция вызывается после щелчка на объекте
        if (cardBack.activeSelf) { // Код деактивации
            cardBack.SetActive(false); // Делаем объект неактивным/невидимым
            controller.CardRevealed(this); // Уведомление контроллера при открытии этой карты
        }
    }

    public void Unreveal() { // Метод который позволяет сновы скрыть карту
        cardBack.SetActive(true);
    }

    [SerializeField] private SceneController controller;

    private int _id;
    public int id {
        get {return _id;} // Добавление функции чтения 
    }

    public void SetCard(int id, Sprite image) {
        _id = id;
        GetComponent<SpriteRenderer>().sprite = image;
    }

    void Start()
    {
      
    }

    void Update()
    {
        
    }
}
