using System.Collections.Generic;
using ProjectLumina.Character;
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

            for (int i = 0; i < _characterNeuroglyphs.NeuroglyphAmount; i++)
            {
                HUDNeuroglyphSlot UISlot = Instantiate(_templateNeuroglyphSlot.gameObject, _transform).GetComponent<HUDNeuroglyphSlot>();
                UISlot.gameObject.SetActive(true);

                _UISlots.Add(UISlot);
            }

            for (int i = 0; i < _characterNeuroglyphs.Neuroglyphs.Count; i++)
            {
                if (_characterNeuroglyphs.Neuroglyphs[i] != null)
                {
                    _UISlots[i].SetIcon(_characterNeuroglyphs.Neuroglyphs[i].Icon);
                    _UISlots[i].SetNeuroglyph(_characterNeuroglyphs.Neuroglyphs[i]);
                    _UISlots[i].SetTierImage(_controller.GetTierSprite(_characterNeuroglyphs.Neuroglyphs[i].CurrentTierLevel));
                }
                else
                {
                    _UISlots[i].SetTierImage(null);
                }
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

        public void HighlightSlot(Neuroglyph neuroglpyh)
        {
            foreach (var UISlot in _UISlots)
            {
                if (neuroglpyh != null && UISlot != null)
                {
                    if (UISlot.Neuroglyph == neuroglpyh)
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