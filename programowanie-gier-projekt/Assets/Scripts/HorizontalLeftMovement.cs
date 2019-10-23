﻿using UnityEngine;

namespace Assets.Scripts
{
    public class HorizontalLeftMovement : MonoBehaviour
    {
        public GameObject targetPrefab;
        public Sprite duck_kill;
        Animator animator;

        private bool _isDead = false;
        private readonly float _offsetMin = 3f;
        private readonly float _offsetMax = 3f;
        private readonly float _minVelocity = 4f;
        private readonly float _maxVelocity = 8f;

        void Start()
        {
            GetComponent<Collider2D>().enabled = true;
            GetComponent<Rigidbody2D>().gravityScale = 0f;

            Setup();
        }

        void Update()
        {
            if (!_isDead && transform.position.x < Constants.MinX)
            {
                Setup();
            }

            if (_isDead && transform.position.y < Constants.MinY)
            {
                var obj = (GameObject)Instantiate(targetPrefab, new Vector2(Constants.MaxX + Random.Range(_offsetMin, _offsetMax), Helpers.GetRandomYPosition()), Quaternion.identity);
                obj.GetComponent<Rigidbody2D>().velocity = new Vector2(-Random.Range(_minVelocity, _maxVelocity), 0);
                ReScale();
                Destroy(gameObject);
            }
        }

        private void OnMouseOver()
        {
            if (!_isDead && Input.GetMouseButtonDown(Constants.LeftMouseButton))
            {
                animator = GetComponent<Animator>();
                animator.enabled = false;
                var sr = gameObject.GetComponent<SpriteRenderer>();
                sr.sprite = duck_kill;
                _isDead = true;
                GetComponent<Rigidbody2D>().gravityScale = 2f;
                ScoreManager.AddPoints(10);
            }
        }

        private void Setup()
        {
            transform.position = new Vector2(Constants.MaxX + Random.Range(_offsetMin, _offsetMax), Helpers.GetRandomYPosition());
            GetComponent<Rigidbody2D>().velocity = new Vector2(-Random.Range(_minVelocity, _maxVelocity), 0);
            animator = GetComponent<Animator>();
            animator.enabled = true;
            ReScale();
            _isDead = false;
        }

        private void ReScale()
        {
            var scale = Random.Range(1f, 3f);
            transform.localScale = new Vector3(scale, scale, 1);
        }
    }
}
