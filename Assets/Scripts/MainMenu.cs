using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
   public void PlayGameShape()
   {
      SceneManager.LoadScene("SceneShape");
   }
   
   public void PlayGameColor()
   {
      SceneManager.LoadScene("SceneColor");
   }
   
   public void ExitGame()
   {
      Application.Quit();
   }
}
