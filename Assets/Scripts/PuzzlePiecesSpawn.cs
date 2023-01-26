using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class PuzzlePiecesSpawn : MonoBehaviour
{
    private Vector3 PuzzlePosition;
    
    public GameObject[] child;
    public Transform content;
    public GameObject wood;
    public bool Initialized = false;
    public int Counter;
    public ConectingPuzzles SelectedPuzzle;
    public Vector3 puzzleContentSize;

    private void Start()
    {
        for (int i = 0; i < child.Length; i++)
        {
            child[i].transform.SetParent(content);
            child[i].AddComponent<HorizontalLayoutGroup>();
            child[i].transform.localScale = puzzleContentSize;
            child[i].GetComponent<SpriteRenderer>().enabled = false;
            child[i].GetComponent<SpriteMask>().enabled = false;
            ConectingPuzzles conectingPuzzles = child[i].GetComponent<ConectingPuzzles>();
            conectingPuzzles.SetRandomPuzzleRotation(conectingPuzzles);
        }

        Initialized = true;
    }
}