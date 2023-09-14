using System.Collections.Generic;
using ProjectLumina.Character;
using ProjectLumina.Factories;
using ProjectLumina.UI;
using Sirenix.OdinInspector;
using UnityEngine;

namespace ProjectLumina.Neuroglyphs
{
    public class NeuroglyphController : MonoBehaviour
    {
        [BoxGroup("UI"), SerializeField]
        private Sprite[] _tierSprites;

        [BoxGroup("UI"), SerializeField]
        private NeuroglyphList _neuroglyphList;

        [BoxGroup("UI"), SerializeField]
        private UIPanel _neuroglyphSelectionScreen;

        [BoxGroup("Settings"), Range(1, 5), SerializeField]
        private int _neuroglyphSelectionAmount;

        [BoxGroup("Neuroglyphs"), TableList, SerializeField]
        private Neuroglyph[] _availableNeuroglyphs;

        [FoldoutGroup("References"), SerializeField]
        private CharacterNeuroglyphs _characterNeuroglyphs;

        private readonly NeuroglyphFactory _factory = new();

        public void ShowNeuroglyphSelectionScreen()
        {
            _neuroglyphList.GenerateList(GetNeuroglyphs(_neuroglyphSelectionAmount));
            _neuroglyphSelectionScreen.Open();
        }

        public Neuroglyph[] GetNeuroglyphs(int amount)
        {
            List<Neuroglyph> neuroglyphs = new();

            for (int i = 0; i < amount; i++)
            {
                Neuroglyph randomNeuroglyph = _factory.GetNeuroglyph(_availableNeuroglyphs[Random.Range(0, _availableNeuroglyphs.Length)]);

                if (neuroglyphs.Exists(i => i.NeuroglyphID == randomNeuroglyph.NeuroglyphID) || randomNeuroglyph.CurrentTierLevel == NeuroglyphTierLevel.Tier9)
                {
                    i--;

                    continue;
                }
                else if (_characterNeuroglyphs.HasNeuroglyph(randomNeuroglyph))
                {
                    randomNeuroglyph.Upgrade((int)_characterNeuroglyphs.GetSlot(randomNeuroglyph).Neuroglyph.CurrentTierLevel + 1);
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