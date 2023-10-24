using System.Collections.Generic;
using ProjectLumina.Character;
using ProjectLumina.Factories;
using Sirenix.OdinInspector;
using UnityEngine;

namespace ProjectLumina.Neuroglyphs
{
    [CreateAssetMenu(fileName = "Neuroglyph Controller", menuName = "Project Lumina/Controllers/Neuroglyph", order = 0)]
    public class NeuroglyphController : ScriptableObject
    {
        [BoxGroup("Neuroglyphs"), TableList, SerializeField]
        private Neuroglyph[] _availableNeuroglyphs;

        [BoxGroup("Settings"), Range(1, 5), SerializeField]
        private int _neuroglyphSelectionAmount;

        [BoxGroup("UI"), PreviewField, SerializeField]
        private Sprite[] _tierSprites;

        private readonly NeuroglyphFactory _factory = new();

        public Neuroglyph[] GetNeuroglyphs(CharacterNeuroglyphs characterNeuroglyphs)
        {
            List<Neuroglyph> neuroglyphs = new();

            for (int i = 0; i < _neuroglyphSelectionAmount; i++)
            {
                Neuroglyph randomNeuroglyph = _factory.GetNeuroglyph(_availableNeuroglyphs[Random.Range(0, _availableNeuroglyphs.Length)]);

                if (neuroglyphs.Exists(i => i.NeuroglyphID == randomNeuroglyph.NeuroglyphID) || randomNeuroglyph.CurrentTierLevel == NeuroglyphTierLevel.Tier9)
                {
                    i--;

                    continue;
                }
                else if (characterNeuroglyphs.HasNeuroglyph(randomNeuroglyph))
                {
                    randomNeuroglyph.Upgrade((int)characterNeuroglyphs.GetNeuroglyph(randomNeuroglyph).CurrentTierLevel + 1);
                    neuroglyphs.Add(randomNeuroglyph);
                }
                else
                {
                    neuroglyphs.Add(randomNeuroglyph);
                }
            }

            return neuroglyphs.ToArray();
        }

        public Sprite GetTierSprite(NeuroglyphTierLevel neuroglyphTier)
        {
            return _tierSprites[(int)neuroglyphTier + 1];
        }
    }
}