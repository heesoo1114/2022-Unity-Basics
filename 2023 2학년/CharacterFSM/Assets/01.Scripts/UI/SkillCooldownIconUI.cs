using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SkillCooldownIconUI : MonoBehaviour
{
    [SerializeField] private PlayerSkill _skillType;
    [SerializeField] private Image _cooldownImage;
    [SerializeField] private TextMeshProUGUI _cooldownText;

    private Skill _skill;

    private void Start()
    {
        _skill = SkillManager.Instance.GetSkill(_skillType);
        _cooldownImage.fillAmount = 0;
        _cooldownText.text = string.Empty;
        _skill.OnCooldown += HandleCooldown;
    }

    private void HandleCooldown(float current, float total)
    {
        _cooldownImage.fillAmount = current / total;
        
        _cooldownText.text = current.ToString("f2");

        if (current <= 0.01f)
        {
            _cooldownText.text = string.Empty;
        }
    }

#if UNITY_EDITOR
    private void OnValidate()
    {
        if(_skillType != 0)
        {
            gameObject.name = $"SkillCoolUI[{_skillType}]";
        }
    }
#endif
}
