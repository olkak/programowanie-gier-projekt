﻿using UnityEngine;

namespace Assets.Scripts
{
    public class CrossMovement : MonoBehaviour
    {
        public GameObject targetPrefab;
        public Sprite duck_kill;
        Animator animator;

        private bool _isDead = false;
        private readonly float _minVelocity = 3f;
        private readonly float _maxVelocity = 6f;
        private float sign = 1;

        void Start()
        {
            GetComponent<Rigidbody2D>().gravityScale = 0f;
            GetComponent<Collider2D>().enabled = true;
            sign = (Random.value > 0.5f) ? -1 : 1;
            Setup();
        }


        void Update()
        {
            if (!_isDead && transform.position.y > Constants.MaxY)
            {
                Setup();
            }

            if (_isDead && transform.position.y < Constants.MinY)
            {
                var obj = (GameObject)Instantiate(targetPrefab, new Vector2(Helpers.GetRandomXPosition(), Constants.MinY - 3), Quaternion.identity);
                obj.GetComponent<Rigidbody2D>().velocity = new Vector2(sign * Random.Range(_minVelocity, _maxVelocity), Random.Range(_minVelocity, _maxVelocity));
                ReScale();
                Destroy(gameObject);
            }
        }

        private void OnMouseOver()
        {
            if (Input.GetMouseButtonDown(Constants.LeftMouseButton))
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
            transform.position = new Vector2(Helpers.GetRandomXPosition(), Constants.MinY - 3);
            GetComponent<Rigidbody2D>().velocity = new Vector2(sign * Random.Range(_minVelocity, _maxVelocity), Random.Range(_minVelocity, _maxVelocity));
            animator = GetComponent<Animator>();
            animator.enabled = true;
            _isDead = false;
            ReScale();
            sign = (Random.value > 0.5f) ? -1 : 1;
        }

        private void ReScale()
        {
            var scale = Random.Range(1f, 3f);
            transform.localScale = new Vector3((-sign) * scale, scale, 1);
        }
    }
}