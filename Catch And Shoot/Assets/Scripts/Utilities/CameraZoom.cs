using System;
using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using UnityEngine;
using Random = UnityEngine.Random;

    public class CameraZoom : MonoBehaviour
    {
        private bool _calcOnce;
        
        public float smoothSpeed;
        public float maxSpeed;
        public float lookSpeed;
        
        private List<GameObject> _targets = new List<GameObject>();

        public Transform target;
        public Vector3 offset;

        private void Update()
        {
            if(target == null)
                TargetDetecter(transform.position, 500);
            
            if(!_calcOnce)
                StartCoroutine(Timer());
        }

        private void LateUpdate()
        {
            SmoothFollow();
        }

        private void SmoothFollow() //Camera following player smoothly.
        {
            var toPos = target.position + offset;
            var curPos = Vector3.Lerp(transform.position, toPos, smoothSpeed);
            transform.position = Vector3.Slerp(curPos, toPos, lookSpeed * Time.fixedDeltaTime);
        }

        public void TargetChanger([CanBeNull] GameObject x, Vector3 y) //Changes the target to follow.
        {
            if (x != null)
                target = x.transform;
            offset = y;
        }

        void TargetDetecter(Vector3 center, float radius) //Selects a target to follow.
        {
            var detected = Physics.OverlapSphere(center, radius);
            for (var i = 0; i < detected.Length; i++)
            {
                var t = detected[i];
                if (t.CompareTag("Player"))
                {
                    _targets.Add(t.gameObject);
                }
            }

            var randomizer = Random.Range(0, _targets.Count - 1);
            TargetChanger(_targets[randomizer], offset);
            _calcOnce = false;
        }

        IEnumerator Timer()
        {
            _calcOnce = true;
            var timer = Random.Range(7, 15);
            yield return new WaitForSeconds(timer);
            TargetDetecter(transform.position, 500);
        }
    }