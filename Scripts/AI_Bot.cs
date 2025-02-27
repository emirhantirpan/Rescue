using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class AI_Bot : MonoBehaviour
{
    public int currentHealth;
    public int damage = 200;

    private int _maxHealth = 5000;
    private float _minValue = 0;
    private float _maxValue = 5000;
    private float _lookDistance = 5f;
    private float _attackSpeed = 0.7f;

    [SerializeField] private Animator _anim;
    [SerializeField] private Image _fillImage;
    [SerializeField] private Slider _slider;
    [SerializeField] private NavMeshAgent _bot;
    [SerializeField] private Transform _target;
    [SerializeField] private Player _player;

    
    private void Start()
    {
        currentHealth = _maxHealth;
        _anim = GetComponent<Animator>();
        _bot = GetComponent<NavMeshAgent>();

        _slider.value = 5000;
        _slider.maxValue = _maxValue;
        _slider.minValue = _minValue;
    }
    void Update()
    {
        //Bot'un karakteri takip etmesi için yazılmıştır.
        float distance = Vector3.Distance(transform.position, _target.position);
        _bot.SetDestination(_target.position);
        Vector3 relativePos = _target.position - transform.position;
        Quaternion lookrotation = Quaternion.LookRotation(relativePos);
        Quaternion LookAtRotationY = Quaternion.Euler(transform.rotation.eulerAngles.x, lookrotation.eulerAngles.y, transform.rotation.eulerAngles.z);
        transform.rotation = LookAtRotationY;

        if (distance == _lookDistance)
        {
            _bot.speed = 0;
        }

        if (distance <= _lookDistance)
        {
            _bot.speed = 6f;
            _anim.SetBool("IsRun", true);
            _anim.SetBool("IsWalk", false);
        }
        else
        {
            _bot.speed = 3f;
            _anim.SetBool("IsRun", false);
            _anim.SetBool("IsWalk", true);
        }
        //Bot'un can barı için yazılmıştır.
        if (_slider.value <= _slider.minValue)
        {
            _fillImage.enabled = false;
        }
        if (_slider.value > _slider.minValue && !_fillImage.enabled)
        {
            _fillImage.enabled = true;
        }
        _slider.value = currentHealth;

        if (_slider.value <=5000 && _slider.value >= 3000)
        {
            _fillImage.color = Color.green;
        }
        else if (_slider.value < 3000 && _slider.value >= 1000)
        {
            _fillImage.color = Color.yellow;
        }
        else if (_slider.value < 1000 && _slider.value >= 0)
        {
            _fillImage.color = Color.red;
        }
        Die();
    }
    //Bot'un karakterden hasar alması için yazılmıştır.
    public void TakeDamage(int attackDamage)
    {
        currentHealth -= attackDamage;
    }
    private void OnCollisionEnter(Collision col)
    {
        if(col.gameObject.tag == "Player")
        {
            _anim.SetBool("IsAttack", true);   
            _anim.SetBool("IsRun", false);
            _anim.SetBool("IsWalk", false);
            _bot.speed = 0;
            StartCoroutine(Attack());
        }
        
    }
    private void OnCollisionExit(Collision col)
    {
        if (col.gameObject.tag == "Player")
        {
            _anim.SetBool("IsAttack", false);
            _anim.SetBool("IsRun", false);
            _anim.SetBool("IsWalk", true);
            _bot.speed = 3f;
            StopCoroutine(Attack());

        }
    }
    //Bot'un karaktere atağı sırasında sürekli bir atak olmaması adına saldırılar arasınaki gecikmeyi sağlayan corroutine yazılmıştır.
   private IEnumerator Attack()
   {
        while(true)
        {
            _player.TakeDamage(damage);
            yield return new WaitForSeconds(_attackSpeed);
        }
   }
   private void Die()
   {
        if (currentHealth == 0)
        {
            _anim.SetBool("IsDead", true);
            Destroy(this.gameObject,1f);
        }
   }
   
}
