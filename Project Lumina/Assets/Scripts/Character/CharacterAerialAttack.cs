using System;
using System.Collections;
using System.Collections.Generic;
using ProjectLumina.Capabilities;
using ProjectLumina.Data;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.Events;

namespace ProjectLumina.Character
{
    [RequireComponent(typeof(CharacterMove))]
    [RequireComponent(typeof(Rigidbody2D))]
    [AddComponentMenu("Character/Character Aerial Attack")]
    public class CharacterAerialAttack : CharacterAbility
    {
        public int CurrentComboIndex
        {
            get => _currentComboIndex;
            set =>
                _currentComboIndex =
                    value <= 0
                        ? 0
                        : value >= Attacks.Length
                            ? Attacks.Length
                            : value;
        }

        [field: BoxGroup("Attacks"), SerializeField]
        public MeleeAttack[] Attacks { get; private set; }

        [ToggleGroup("SlowStop")]
        public bool SlowStop;

        [ToggleGroup("SlowStop"), Range(0, 10), SerializeField]
        private float _slowStopGravityScale;

        [ToggleGroup("SlowStop"), Range(0, 10), SerializeField]
        private float _slowStopVelocityScale;

        [ToggleGroup("SlowStop"), SerializeField, Range(0, 5)]
        private float _slowStopTimer;

        private int _currentComboIndex = 0;
        private Attack _currentAerialAttack;
        private Rigidbody2D _rb;

        public UnityAction<GameObject> onHit;

        private void Awake()
        {
            _rb = GetComponent<Rigidbody2D>();
        }

        public bool CanNextCombo(int stateLength)
        {
            if (CurrentComboIndex < stateLength)
            {
                _currentAerialAttack = Attacks[CurrentComboIndex];

                if (_currentAerialAttack.IsUnlocked)
                {
                    CurrentComboIndex++;

                    if (SlowStop)
                    {
                        StartCoroutine(StartSlowStop());
                    }
                }
                else
                {
                    EndCombo();
                    return false;
                }

                return true;
            }

            EndCombo();
            return false;
        }

        public void EndCombo()
        {
            CurrentComboIndex = 0;
        }

        public void DealAerialDamage()
        {
            foreach (
                Damageable damageable in _currentAerialAttack.Sensor.GetDetectedComponents(
                    new List<Damageable>()
                )
            )
            {
                if (_currentAerialAttack.TryAttack(gameObject, damageable))
                {
                    onHit?.Invoke(damageable.gameObject);
                }
            }
        }

        private IEnumerator StartSlowStop()
        {
            _rb.gravityScale = _slowStopGravityScale;
            _rb.velocity /= _slowStopVelocityScale;

            yield return new WaitForSeconds(_slowStopTimer);
        }
    }
}
