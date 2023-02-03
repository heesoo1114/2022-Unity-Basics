using System.Collections.Generic;
using System.Linq;
using UnityEngine.UIElements;

namespace DataBinding
{

    public class DropDownController
    {

        public PersonLitSO SelectedValue { get; private set; }

        private VisualElement _root;
        private DropdownField _dropdown;

        public DropDownController(VisualElement root, List<PersonLitSO> people)
        {
            _root = root;
            _dropdown = root.Q<DropdownField>("GenderInput");

            _dropdown.choices = people.Select(x => x.name).ToList();
            _dropdown.value = people[0].name;
            SelectedValue = people[0];

            _dropdown.RegisterCallback<ChangeEvent<string>>(evt => SelectedValue = people.Find(x => x.name == evt.newValue));
        }
    }

}

