using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DataBinding
{
    public class Person
    {
        public event Action OnChanged = null;

        private string _name;
        private string _info;
        private Sprite _sprite;

        public string Name
        {
            get => _name;
            set
            {
                if (_name != value)
                {
                    _name = value;
                    OnChanged?.Invoke();
                }
            }
        }
        public string Info
        {
            get => _info;
            set
            {
                if (_info != value)
                {
                    _info = value;
                    OnChanged?.Invoke();
                }
            }
        }
        public Sprite Sprite
        {
            get => _sprite;
            set
            {
                if (_sprite != value)
                {
                    _sprite = value;
                    OnChanged?.Invoke();
                }
            }
        }

        public Person(string name, string info, Sprite sprite)
        {
            _name = name;
            _info = info;
            _sprite = sprite;
            OnChanged?.Invoke(); //내가 바뀌었음을 통지한다.
        }
    }

}