using System;
using ProjectLumina.Character;
using UnityEngine;

namespace ProjectLumina.StateMachine.Character
{
    [RequireComponent(typeof(Animator))]
    [RequireComponent(typeof(CharacterFall))]
    [RequireComponent(typeof(CharacterMove))]
    public abstract class CharacterStateController : StateController
    {
        public Animator Animator { get; private set; }
        public CharacterFall CharacterFall { get; private set; }
        public CharacterMove CharacterMove { get; private set; }

        protected CharacterAbility[] abilities;

        protected override void Awake()
        {
            base.Awake();

            Animator = GetComponent<Animator>();
            CharacterFall = GetComponent<CharacterFall>();
            CharacterMove = GetComponent<CharacterMove>();
            abilities = GetComponents<CharacterAbility>();
        }

        public bool HasCharacterAbility<T>(out T characterAbility) where T : CharacterAbility
        {
            T ability = (T)Array.Find(abilities, i => i.GetType() == typeof(T));
            characterAbility = ability;

            if (ability != null)
            {
                if (ability.IsUnlocked)
                {
                    return true;
                }
            }

            return false;
        }
    }
}