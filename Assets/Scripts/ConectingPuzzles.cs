using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UIElements;

public class ConectingPuzzles : MonoBehaviour
{
    [SerializeField] private Color trColor;
    [SerializeField] private Color _color;
    [SerializeField] private PuzzlePiecesSpawn _puzzlePiecesSpawn;

    private Vector3 RightPosition;
    private bool isConected;
    private bool isBusy;
    private BoxCollider2D _boxCollider2D;

    public Shape Shape;
    public ERotation CurrentRotation;
    public ERotation TargetRotation;


    private void Start()
    {
        _boxCollider2D = GetComponent<BoxCollider2D>();
        RightPosition = transform.position;
        isConected = false;
        isBusy = false;
    }

    private ERotation GetNextRotation()
    {
        for (int i = 0; i < _puzzlePiecesSpawn.child.Length; i++)
        {
            if (_puzzlePiecesSpawn.child[i] == _puzzlePiecesSpawn.SelectedPuzzle.gameObject)
            {
                var valuesAsList = ERotation.GetValues(typeof(ERotation)).Cast<ERotation>().ToList();

                for (int j = 0; j < valuesAsList.Count; j++)
                {
                    if (valuesAsList[j] == _puzzlePiecesSpawn.SelectedPuzzle.CurrentRotation)
                    {
                        int nextValue = j++;
                        if (j >= valuesAsList.Count)
                        {
                            return valuesAsList[0];
                        }

                      //  Debug.Log(valuesAsList[j] + "" + _puzzlePiecesSpawn.SelectedPuzzle.CurrentRotation + j);
                        return valuesAsList[j];
                    }
                }
            }
        }

        Debug.LogError("rotation is not founded");
        return ERotation.ERotation0;
    }

    private void Update()
    {
        
        if (Input.GetMouseButtonDown(0))

        {
            RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero);
            if (hit.collider == null)
            {
                return;
            }

            if (hit.transform.CompareTag("Puzzle"))
            {
                _puzzlePiecesSpawn.SelectedPuzzle = hit.transform.gameObject.GetComponent<ConectingPuzzles>();
            }

            if (_puzzlePiecesSpawn.SelectedPuzzle != null &&
                _puzzlePiecesSpawn.SelectedPuzzle == this)
            {
                SetRotation(_puzzlePiecesSpawn.SelectedPuzzle, GetNextRotation());
            }
        }

        if (Input.GetMouseButtonUp(0))
        {
            _puzzlePiecesSpawn.SelectedPuzzle = null;
        }

        if (_puzzlePiecesSpawn.SelectedPuzzle != null)
        {
            Debug.Log(_puzzlePiecesSpawn.SelectedPuzzle.gameObject.name);
            Vector3 MousePoint = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            _puzzlePiecesSpawn.SelectedPuzzle.transform.position = new Vector3(MousePoint.x, MousePoint.y, 0);
        }

        for (int i = 0; i < _puzzlePiecesSpawn.child.Length; i++)
        {
            if (Vector3.Distance(transform.position,
                    _puzzlePiecesSpawn.child[i].GetComponent<ConectingPuzzles>().RightPosition) < 2f &&
                _puzzlePiecesSpawn.child[i].GetComponent<ConectingPuzzles>().Shape == Shape && !isConected &&
                _puzzlePiecesSpawn.Initialized && transform.root.tag != "Canvas" &&
                !_puzzlePiecesSpawn.child[i].GetComponent<ConectingPuzzles>().isBusy &&
                _puzzlePiecesSpawn.child[i].GetComponent<ConectingPuzzles>().TargetRotation ==
                _puzzlePiecesSpawn.child[i].GetComponent<ConectingPuzzles>().CurrentRotation)
            {
                _puzzlePiecesSpawn.SelectedPuzzle = null;
                _puzzlePiecesSpawn.SelectedPuzzle = null;
                transform.position = _puzzlePiecesSpawn.child[i].GetComponent<ConectingPuzzles>().RightPosition;
                _boxCollider2D.enabled = false;
                isConected = true;
                _puzzlePiecesSpawn.child[i].GetComponent<ConectingPuzzles>().isBusy = true;
                _puzzlePiecesSpawn.Counter++;
                StartCoroutine(Coroutine1());
            }
        }
    }

    private void SetRotation(ConectingPuzzles conectingPuzzles, ERotation rotation)
    {
        switch (rotation)
        {
            case ERotation.ERotation0:
                conectingPuzzles.transform.eulerAngles = new Vector3(0, 0, 0);
                conectingPuzzles.CurrentRotation = ERotation.ERotation0;
                break;
            case ERotation.ERotation90:
                conectingPuzzles.transform.eulerAngles = new Vector3(0, 0, 90);
                conectingPuzzles.CurrentRotation = ERotation.ERotation90;
                break;
            case ERotation.ERotation180:
                conectingPuzzles.transform.eulerAngles = new Vector3(0, 0, 180);
                conectingPuzzles.CurrentRotation = ERotation.ERotation180;
                break;
            case ERotation.ERotation270:
                conectingPuzzles.transform.eulerAngles = new Vector3(0, 0, 270);
                conectingPuzzles.CurrentRotation = ERotation.ERotation270;
                break;
        }
    }


    private IEnumerator Coroutine1()
    {
        transform.GetComponent<SpriteRenderer>().color = trColor;
        yield return new WaitForSeconds(0.5f);
        transform.GetComponent<SpriteRenderer>().color = _color;
    }

    public void SetRandomPuzzleRotation(ConectingPuzzles conectingPuzzles)
    {
        var valuesAsList = ERotation.GetValues(typeof(ERotation)).Cast<ERotation>().ToList();
        int targetIndex = Random.Range(0, valuesAsList.Count + 1);
        for (int i = 0; i < valuesAsList.Count; i++)
        {
            if (i == targetIndex)
            {
                SetRotation(conectingPuzzles, valuesAsList[i]);
                break;
            }
        }
    }
}