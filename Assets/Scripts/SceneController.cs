using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneController : MonoBehaviour
{
    public const int gridRows = 2;
    public const int gridCols = 4;
    public const float offsetX = 2f;
    public const float offsetY = 2.5f;

    [SerializeField] private MemoryCard originalCard;
    [SerializeField] private Sprite[] images; 



    void Start()
    {
        Vector3 startPos = originalCard.transform.position; // Положение первой карты, и положение остальных карт отсчитывается от этой точки;

        int[] numbers = {0, 0, 1, 1, 2, 2, 3, 3}; // Индентификаторы карт

        numbers = ShuflleArray(numbers); // Перемешивание элементов

        for (int i = 0; i < gridCols; i++) {

            for (int j = 0; j < gridRows; j++) {
                
                MemoryCard card; // Ссылка на контейнер для исходной карты или ее копий
                if (i == 0 && j == 0) {
                    card = originalCard;
                } else {
                    card = Instantiate(originalCard) as MemoryCard;
                }

                int index = j * gridCols + i;
                int id =  numbers[index]; // Получаем индентифакатор
                Debug.Log("index ="+index);
                card.SetCard(id, images[id]);

                float posX = (offsetX * i) + startPos.x;
                float posY = -(offsetY * j) + startPos.y;
                card.transform.position = new Vector3(posX, posY, startPos.z);
            }
        }
    }

    private int[] ShuflleArray(int[] numbers) {
        int[] newArray = numbers.Clone() as int[];
        for (int i = 0; i < newArray.Length; i++) {
            int tmp = newArray[i];
            int r = Random.Range(i, newArray.Length);
            newArray[i] = newArray[r];
            newArray[r] = tmp;
        }
        return newArray;
    }

    private MemoryCard _firstRevealed;
    private MemoryCard _secondRevealed;
    private int _score = 0;

    public bool canReveal {
        get {return _secondRevealed == null;} // Функция чтения, которая возвращает значение false, если вторая карта уже открыта.
    }

    public void CardRevealed(MemoryCard card) {
        if (_firstRevealed == null) { // Сохранениек карт в одной из двух переменных
            _firstRevealed = card;
        } else {
            _secondRevealed = card;
            StartCoroutine(CheckMatch());
        }
    }

    [SerializeField] private TextMesh Score;

    private IEnumerator CheckMatch() {
        if (_firstRevealed.id == _secondRevealed.id) {
            _score++;
            Score.text = "Score: " + _score;
        } else {
            yield return new WaitForSeconds(.5f);

            _firstRevealed.Unreveal();
            _secondRevealed.Unreveal();
        }
        _firstRevealed = null;
        _secondRevealed = null;
    }

    public void Restart() {
        Application.LoadLevel("SampleScene");
    }
}
