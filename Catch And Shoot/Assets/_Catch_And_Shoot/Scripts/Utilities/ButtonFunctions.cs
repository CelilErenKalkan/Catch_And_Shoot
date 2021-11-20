using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;


public class ButtonFunctions : MonoBehaviour
    {
        
        private Vector3 _offset1 = new Vector3(0.0f, 1.8f, -3.0f);
        private Vector3 _offset2 = new Vector3(0.0f, 0.0f, 0.0f);
        private bool _shouldTrans;
        private GameObject mat;


        public void LoadLevel()
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
    }