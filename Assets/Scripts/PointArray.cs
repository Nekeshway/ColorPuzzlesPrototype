using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class PointArray : MonoBehaviour
{
    public int childCount;
    public TextMeshProUGUI childCountText;
    [SerializeField] private PuzzlePiecesSpawn _puzzlePiecesSpawn;
    
    void Update()
    {
     
        childCountText.SetText(_puzzlePiecesSpawn.Counter+"/"+_puzzlePiecesSpawn.child.Length);
        if (_puzzlePiecesSpawn.Counter == _puzzlePiecesSpawn.child.Length)
        {
            SceneManager.LoadScene("StartScene");
        }
    }
}