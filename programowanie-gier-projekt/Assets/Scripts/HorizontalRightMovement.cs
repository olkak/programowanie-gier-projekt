﻿using UnityEngine;

namespace Assets.Scripts
{
    public class HorizontalRightMovement : MonoBehaviour
    {
        public GameObject targetPrefab;
        public Sprite duck_kill;
        Animator animator;

        private bool _isDead = false;
        private float _scale = 0f;
         public int value = 30;
        private readonly float _offsetMin = 1f;
        private readonly float _offsetMax = 3f;
        private readonly float _minVelocity = 4f;
        private readonly float _maxVelocity = 8f;
        private float mult = RoundManager.round / 10f;

        void Start()
        {
            GetComponent<Rigidbody2D>().gravityScale = 0f;
            GetComponent<Collider2D>().enabled = true;

            Setup();
        }


        void Update()
        {
            if (!_isDead && transform.position.x > Constants.MaxX)
            {
                Setup();
            }

            if (_isDead && transform.position.y < Constants.MinY)
            {
                var obj = (GameObject)Instantiate(targetPrefab, new Vector2(Constants.MinX - Random.Range(_offsetMin, _offsetMax), Helpers.GetRandomYPosition()), Quaternion.identity);
                obj.GetComponent<Rigidbody2D>().velocity = new Vector2(-Random.Range(_minVelocity+mult, _maxVelocity+mult), 0);
                ReScale();
                Destroy(gameObject);
            }


            if (!WeaponManager.isReloading &&  !_isDead && Input.GetMouseButtonDown(Constants.LeftMouseButton))
            {
                var dist = Mathf.Abs(transform.position.z - Camera.main.transform.position.z);
                var v3Pos = new Vector3(Input.mousePosition.x, Input.mousePosition.y, dist);
                v3Pos = Camera.main.ScreenToWorldPoint(v3Pos);
                var distanceBetween = Vector3.Distance(v3Pos, transform.position);
                if (distanceBetween < (int)WeaponManager.weaponCategory * 1f + 1)
                {
                    Hit();
                }
            }
        }

        private void OnMouseOver()
        {
            if (!WeaponManager.isReloading && !_isDead && Input.GetMouseButtonDown(Constants.LeftMouseButton))
            {
                Hit();
            }
        }

        private void Hit()
        {

            animator = GetComponent<Animator>();
            animator.enabled = false;
            var sr = gameObject.GetComponent<SpriteRenderer>();
            sr.sprite = duck_kill;
            _isDead = true;
            GetComponent<Rigidbody2D>().gravityScale = 2f;
            ScoreManager.AddPoints(Mathf.FloorToInt(value - _scale * 10));
             DucksLeftManager.DecreaseDucksLeftCounter();
        }

        private void Setup()
        {
            transform.position = new Vector2(Constants.MinX - Random.Range(_offsetMin, _offsetMax), Helpers.GetRandomYPosition());
            GetComponent<Rigidbody2D>().velocity = new Vector2(Random.Range(_minVelocity+mult, _maxVelocity+mult), 0);
            animator = GetComponent<Animator>();
            animator.enabled = true;
            _isDead = false;
            ReScale();
        }

        private void ReScale()
        {
            _scale = Random.Range(1f, 3f);
             _scale -= RoundManager.round / 30f; 
            transform.localScale = new Vector3(-_scale, _scale, 1);
        }
    }
}
