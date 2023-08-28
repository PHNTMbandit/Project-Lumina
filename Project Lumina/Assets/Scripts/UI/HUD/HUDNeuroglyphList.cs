using System.Collections.Generic;
using ProjectLumina.Abilities;
using ProjectLumina.Data;
using ProjectLumina.Neuroglyphs;
using UnityEngine;

namespace ProjectLumina.UI
{
    public class HUDNeuroglyphList : MonoBehaviour
    {
        [SerializeField]
        private NeuroglyphController _controller;

        [SerializeField]
        private CharacterNeuroglyphs _characterNeuroglyphs;

        [SerializeField]
        private HUDNeuroglyphSlot _templateNeuroglyphSlot;

        [SerializeField]
        private Transform _transform;

        private readonly List<HUDNeuroglyphSlot> _UISlots = new();

        private void Awake()
        {
            _templateNeuroglyphSlot.gameObject.SetActive(false);

            _characterNeuroglyphs.onNeuroglyphListRefresh += GenerateList;
        }

        private void Start()
        {
            GenerateList();
        }

        public void GenerateList()
        {
            ResetList();

            foreach (NeuroglyphSlot slot in _characterNeuroglyphs.GetSlots())
            {
                HUDNeuroglyphSlot UISlot = Instantiate(_templateNeuroglyphSlot.gameObject, _transform).GetComponent<HUDNeuroglyphSlot>();
                UISlot.gameObject.SetActive(true);

                if (slot.Neuroglyph != null)
                {
                    UISlot.SetIcon(slot.Neuroglyph.Icon);
                    UISlot.SetNeuroglyph(slot.Neuroglyph);
                    UISlot.SetTierImage(_controller.GetTierSprite(slot.Neuroglyph.CurrentTierLevel));
                }
                else
                {
                    UISlot.SetTierImage(null);
                }

                _UISlots.Add(UISlot);
            }
        }

        private void ResetList()
        {
            if (_UISlots.Count > 0)
            {
                foreach (HUDNeuroglyphSlot slotUI in _UISlots)
                {
                    Destroy(slotUI.gameObject);
                }

                _UISlots.Clear();
            }
        }

        public void HighlightSlot(NeuroglyphSlot slot)
        {
            foreach (var UISlot in _UISlots)
            {
                if (slot != null && UISlot != null)
                {
                    if (UISlot.Neuroglyph == slot.Neuroglyph)
                    {
                        UISlot.Highlight();
                    }
                    else
                    {
                        UISlot.Unhighlight();
                    }
                }
                else
                {
                    UISlot.Unhighlight();
                }
            }
        }
    }
}