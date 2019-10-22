﻿using UnityEngine;

namespace Assets.Scripts
{
    public class HorizontalRightMovement : MonoBehaviour
    {
        public GameObject targetPrefab;
        private readonly float _offsetMin =1f;
        private readonly float _offsetMax = 3f;
        private readonly float _minVelocity = 4f;
        private readonly float _maxVelocity = 8f;

        void Start()
        {
            GetComponent<Rigidbody2D>().gravityScale = 0f;
            GetComponent<Collider2D>().enabled = true;

            Setup();
        }


        void Update()
        {
            if (transform.position.x > Constants.MaxX)
            {
                Setup();
            }
        }

        private void OnMouseOver()
        {
            if (Input.GetMouseButtonDown(Constants.LeftMouseButton))
            {
                print("CLICK");


                var obj = (GameObject)Instantiate(targetPrefab, new Vector2(Constants.MinX - Random.Range(_offsetMin, _offsetMax), Helpers.GetRandomYPosition()), Quaternion.identity);
                obj.GetComponent<Rigidbody2D>().velocity = new Vector2(-Random.Range(_minVelocity, _maxVelocity), 0);

                ScoreManager.AddPoints(20);
                Destroy(gameObject);

            }
        }

        private void Setup()
        {
            transform.position = new Vector2(Constants.MinX - Random.Range(_offsetMin, _offsetMax), Helpers.GetRandomYPosition());
            GetComponent<Rigidbody2D>().velocity = new Vector2(Random.Range(_minVelocity, _maxVelocity), 0);
        }
    }
}