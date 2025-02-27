using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{
    public int attackDamage = 200;
    public float attackRate = 2.5f;
    public int currentHealth;
    public float score;
    public bool isTeleport = false;

    private int _maxHealth = 5000;
    private float _speed = 8f;
    private float _horizontal;
    private float _vertical;
    private float _rotationSpeed = 720;
    private float _attackRange = 0.3f;
    private float _nextAttackTime = 0;
    private bool _isOpen = false;
    private bool _isTeleportable = false;

    [SerializeField] private Rigidbody _rb;
    [SerializeField] private Animator _anim;
    [SerializeField] TMP_Text _text;
    [SerializeField] private Transform _attackPoint;
    [SerializeField] private LayerMask _attackLayer;
    [SerializeField] private GameObject _teleportPanel;
    [SerializeField] private TMP_Text _pressE;
    [SerializeField] private TMP_Text _pressT;
    [SerializeField] private TMP_Text _dontHave;

    private void Start()
    {
        _text.text = "";
        _rb = GetComponent<Rigidbody>();
        _anim = GetComponent<Animator>();
        currentHealth = _maxHealth;
        _teleportPanel.SetActive(false);
        Time.timeScale = 1f;
        _pressE.gameObject.SetActive(false);
        _pressT.gameObject.SetActive(false);
        _dontHave.gameObject.SetActive(false);
        DontDestroyOnLoad(this.gameObject);
    }
    private void Update()
    {
        _horizontal = Input.GetAxis("Horizontal");
        _vertical = Input.GetAxis("Vertical");
        _text.text = "" + score;

        Sneak();

        if (Time.time >= _nextAttackTime)
        {
            if (Input.GetKeyDown(KeyCode.Mouse0))
            {
                Attack();
                _nextAttackTime = Time.time + 1 / attackRate;
                _anim.SetBool("IsAttack", true);
            }
            else
            {
                _anim.SetBool("IsAttack", false);
            }
        }
        Die();
        Buy();
        Teleport();
        if (isTeleport == true)
        {
            score = 0;
        }
    }
    private void FixedUpdate()
    {
        Vector3 _moveDirection = new Vector3(_horizontal, 0, _vertical);
        _moveDirection.Normalize();

        transform.Translate(_moveDirection * _speed *Time.deltaTime, Space.World);

        if (_moveDirection != Vector3.zero)
        {
            transform.forward = _moveDirection;

            Quaternion toRotation = Quaternion.LookRotation(_moveDirection, Vector3.up);

            transform.rotation = Quaternion.RotateTowards(transform.rotation, toRotation, _rotationSpeed * Time.deltaTime);
        }

        _anim.SetFloat("RunVertical", Mathf.Abs(_vertical));
        _anim.SetFloat("RunHorizontal", Mathf.Abs(_horizontal));
    }
    private void Sneak()
    {
        if (Input.GetKey(KeyCode.LeftShift))
        {
            _anim.SetBool("IsSneak", true);
            _speed = 3f;
        }
        else
        {
            _anim.SetBool("IsSneak", false);
            _speed = 8f;
        }
    }
    private void Attack()
    {

        Collider[] hitEnemies = Physics.OverlapSphere(_attackPoint.position, _attackRange, _attackLayer);

        foreach (Collider enemy in hitEnemies)
        {
            enemy.GetComponent<AI_Bot>().TakeDamage(attackDamage);
        }
    }
    //Player objesinin bot'a vururkenki atak alanı için yazılmıştır.
    private void OnDrawGizmosSelected()
    {
        if (_attackPoint == null)
        {
            return;
        }
        Gizmos.DrawWireSphere(_attackPoint.position, _attackRange);
    }
    //Player objesinin bot'tan hasar alması için yazılmıştır.
    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
    }
    private void Die()
    {
        if (currentHealth == 0)
        {
            _anim.SetBool("IsDead", true);
        }
    } 
    private void OnTriggerStay(Collider col)
    {
        if (col.gameObject.tag == "Stunt")
        {
            _pressE.gameObject.SetActive(true);
            _isOpen = true;
        }
        if (col.gameObject.tag == "Teleport")
        {
            if (isTeleport == true)
            {
                _pressT.gameObject.SetActive(true);     
            }
            else
            {
                _dontHave.gameObject.SetActive(true);
            }
            _isTeleportable = true;
        }
    }
    private void OnTriggerExit(Collider col)
    {
        if (col.gameObject.tag == "Stunt")
        {
            _pressE.gameObject.SetActive(false);
            _isOpen = false;
        }
        if (col.gameObject.tag == "Teleport")
        {
            _pressT.gameObject.SetActive(false);     
            _dontHave.gameObject.SetActive(false);
            _isTeleportable = false;
        }
    }
    private void OnCollisionEnter(Collision col)
    {
        if (col.gameObject.tag == "Brother")
        {
            SceneManager.LoadScene("Menu");
        }
    }
    private void Buy()
    {
        if (Input.GetKeyDown(KeyCode.E) && _isOpen == true)
            {
                if (_teleportPanel.activeInHierarchy)
                {
                    _teleportPanel.SetActive(false);
                    Time.timeScale = 1f; 
                }
                else
                {
                    _teleportPanel.SetActive(true);
                    Time.timeScale = 0f; 
                    _pressE.gameObject.SetActive(false);
                }
            } 
    }
    private void Teleport()
    {
        if (Input.GetKeyDown(KeyCode.T) && isTeleport == true && _isTeleportable == true)
        {
            SceneManager.LoadScene("Level2");
        }
    }
}
                
                
                
