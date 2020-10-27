using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.AccessControl;
using System.Text;
using System.Threading.Tasks;

namespace Family
{
	public class Family
	{
		private List<Persona> Persona; 

		public Family()
		{
			Persona = new List<Persona>();
		}

		public List<Persona> GetPersona()
		{
			Family clone = Clone();

			return clone.Persona;
		}

		private Family Clone()
        {
			Family clone = new Family { Persona = this.Persona};
			return clone;
		}

		private void DuplicationPersona(Persona person)
		{
			if (CheckDuplicates(Persona, person))
			{
				throw new Exception($"Дублирование родства, {person.Name} уже является родствеником");
			}
		}


		public void AddRelative(Persona person)
		{
			DuplicationPersona(person);
			Persona.Add(person);
		}

		public string ViewPersona()
		{
			var names = " ";

			foreach (var person in Persona)
			{
				names += person.Name + ",";
			}
			return names;
		}

		private bool CheckDuplicates(List<Persona> list, Persona person)
		{
			return list.Any(item => item == person);
		}
	}
}