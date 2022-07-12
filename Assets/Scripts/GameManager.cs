using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
   
   public Gradient gradient;
   public GameObject fire;
   private void Update()
   {
      if (Input.GetKeyDown(KeyCode.R))
      {
         SceneManager.LoadScene("Scenes/SampleScene");
      }
   }
}
