using UnityEngine;
using UnityEngine.UI;

public class HealthController : MonoBehaviour
{
    private Transform _owner;
    private CharactersConfig Config;
    public Image HpImage;
    public Image ArmorImage;

    public float _health;

    void OnEnable()
    {
        if (!_owner)
            _owner = transform;

        if (!Config)
            return;

        Initialize(Config);
    }

    void OnDisable()
    {
        RemoveEventListeners();
    }

    void Update()
    {

    }

    public void Initialize(CharactersConfig config)
    {
        Config = config;
        _health = config.Health;

        InitGUIElements();

        RemoveEventListeners();
        AddEventListeners();
    }

    private void InitGUIElements()
    {
        if (HpImage)
        {
            HpImage.color = Color.green;
            HpImage.fillAmount = 1f;
        }

        if (ArmorImage)
        {
            ArmorImage.color = Color.gray;
            ArmorImage.fillAmount = Config.Armor;
        }
    }

    private void Death()
    {
        _health = 0;
        _owner.gameObject.SetActive(false);
        EventManager.Death(_owner);
    }

    private void AddEventListeners()
    {
        EventManager.DamageEvent += OnDamage;
        EventManager.HealEvent += OnHeal;
    }

    private void RemoveEventListeners()
    {
        EventManager.DamageEvent -= OnDamage;
        EventManager.HealEvent -= OnHeal;
    }

    private void OnDamage(Transform target, float value)
    {
        if (target != _owner)
            return;

        _health -= (value * (1 - Config.Armor));

        if (_health <= 0)
            Death();

        UpdateGUiHealth();
    }

    private void OnHeal(Transform target, float value)
    {
        if (target != _owner)
            return;

        _health += value;
        if (Config.Health < _health)
            _health = Config.Health;

        UpdateGUiHealth();
    }

    private void UpdateGUiHealth()
    {
        if (!HpImage)
            return;

        HpImage.fillAmount = _health / Config.Health;
        HpImage.color = Color.Lerp(Color.red, Color.green, _health / Config.Health);
    }
}
