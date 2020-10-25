using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.AccessControl;
using System.Text;
using System.Threading.Tasks;

namespace Famaily
{
	public class Famaily
	{
		private List<Persona> Persona; 

		public Famaily()
		{
			Persona = new List<Persona>();
		}

		public List<Persona> GetPersona()
		{
			return Persona;
		}


		private void DuplicationPersona(Persona person)
		{
			if (CheckingВuplicates(Persona, person))
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

		private bool CheckingВuplicates(List<Persona> list, Persona person)
		{
			foreach (var item in list)
			{
				if (item == person)
				{
					return true;
				}
			}
			return false;
		}
	}
}