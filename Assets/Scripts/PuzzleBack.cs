using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PuzzleBack : MonoBehaviour
{
    [SerializeField] private PuzzlePiecesSpawn _puzzlePiecesSpawn;

    private bool isEnter;
    private bool isExit;

    public Vector3 puzzleSize;


    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!_puzzlePiecesSpawn.SelectedPuzzle)
        {
            return;
        }

        GameObject puzzle = _puzzlePiecesSpawn.SelectedPuzzle.gameObject;
        if (isEnter)
        {
            return;
        }

        if (other.gameObject == puzzle)
        {
            _puzzlePiecesSpawn.SelectedPuzzle = null;
            _puzzlePiecesSpawn.SelectedPuzzle = null;
            puzzle.transform.SetParent(_puzzlePiecesSpawn.content);
            puzzle.transform.localScale = _puzzlePiecesSpawn.puzzleContentSize;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (_puzzlePiecesSpawn.SelectedPuzzle != null)
        {
            _puzzlePiecesSpawn.SelectedPuzzle.GetComponent<SpriteRenderer>().enabled = true;
            _puzzlePiecesSpawn.SelectedPuzzle.GetComponent<SpriteMask>().enabled = true;
            StartCoroutine(ExampleCoroutine());
        }
    }

    private IEnumerator ExampleCoroutine()
    {
        isEnter = true;
        GameObject puzzle = _puzzlePiecesSpawn.SelectedPuzzle.gameObject;
        _puzzlePiecesSpawn.SelectedPuzzle.transform.SetParent(null);

        _puzzlePiecesSpawn.wood.gameObject.GetComponent<ScrollRect>().enabled = false;
        _puzzlePiecesSpawn.wood.gameObject.GetComponent<ScrollRect>().enabled = true;
        puzzle.transform.localScale = puzzleSize;
        yield break;
    }

    private void Update()
    {
        if (!Input.GetMouseButton(0))
        {
            isEnter = false;
        }
    }
}