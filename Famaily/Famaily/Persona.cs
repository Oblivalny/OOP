using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Famaily
{
	public class Persona
	{
		public string Name { get; }

		//private Persona Partner ;
		//private List<Persona> Parents;
		//private List<Persona> Brother_sister;
		//private List<Persona> Grandparents;
		//private List<Persona> Children;
		//private List<Persona> Grandchildren;


		public Persona Partner;
		public Famaily Parents { get; }
		public Famaily BrotherAndSister { get; }
		public Famaily Grandparents { get; }
		public Famaily Children { get; }
		public Famaily Grandchildren { get; }

		public Persona(string name)
		{
			Name = name;
			Parents = new Famaily();
			BrotherAndSister = new Famaily();
			Grandparents = new Famaily();
			Children = new Famaily();
			Grandchildren = new Famaily();
		}

		public void SetPartner(Persona partner)
		{
			NotChildren(partner);

			Partner = partner;
			partner.Partner = this;
		}

		public void SetParent(Persona parent)
		{
			ViolationOfFamilyTies(parent, "Parent");
			
			foreach (var grandparent in parent.Grandparents.Persona)
			{
				SetGrandparents(grandparent);
			}
			
			foreach (var cildren in parent.Children.Persona)
			{
				SetBrotherAndSister(cildren);
			}
			//parent.SetChilren(this);
			Parents.AddRelative(parent);
			parent.Children.AddRelative(this);


		}

		public void SetChilren(Persona child)
		{
			ViolationOfFamilyTies(child, "Chilren");
			Children.AddRelative(child);
			child.Parents.AddRelative(this);
		}

	
		public void SetBrotherAndSister(Persona partner)
		{
			ViolationOfFamilyTies(partner, "BrotherAndSister");
			BrotherAndSister.AddRelative(partner);
			partner.BrotherAndSister.AddRelative(this);

		}

		public void SetGrandparents(Persona gandparents)
		{
			ViolationOfFamilyTies(gandparents, "Grandparents");
			Grandparents.AddRelative(gandparents);
			gandparents.Grandchildren.AddRelative(this);
		}

		public void SetGrandchildren(Persona grandchildren)
		{
			ViolationOfFamilyTies(grandchildren, "Grandchildren");
			Grandchildren.AddRelative(grandchildren);
			grandchildren.Grandparents.AddRelative(this);
		}

		private string ViewPersona(List<Persona> persons)
		{
			var names = " ";

			foreach (var person in persons)
			{
				names += person.Name + ",";
			}
			return names;
		}

		private List<Persona> AuntAndUncle()
		{

			List<Persona> auntAndUncle = new List<Persona>();
			foreach (var perent in this.Parents.Persona)
			{
				foreach (var person in perent.BrotherAndSister.Persona)
				{
					auntAndUncle.Add(person);
				}

			}

			return auntAndUncle;
		}

		public string ViewAuntAndUncle()
		{
			return "Aunt and Uncle: " + ViewPersona(AuntAndUncle());
		}

		private List<Persona> Cousins()
		{

			List<Persona> cusins = new List<Persona>();
			List<Persona> auntAndUncle = AuntAndUncle();

			foreach (var person_anauntAndUncle in auntAndUncle)
			{
				foreach (var person_cusins in person_anauntAndUncle.Children.Persona)
				{
					cusins.Add(person_cusins);
				}
			}

			return cusins;
		}
		public string ViewCousins()
		{
			return "Cousins: " + ViewPersona(Cousins());
		}


		private void ViolationOfFamilyTies(Persona person,  string familyTies)
		{
			if (familyTies!= "Parent") {
				NotParent(person);
			}
			if (familyTies != "Grandchildren")
			{
				NotGrandchildren(person);
			}
			if (familyTies != "Grandparent")
			{
				NotGrandparent(person);
			}
			if (familyTies != "BrotherAndSister")
			{
				NotBrotherAndSister(person);
			}
			if (familyTies != "Childrenr")
			{
				NotChildren(person);
			}
			if (familyTies != "Childrenr")
			{
				NotChildren(person);
			}
		}
		private void NotParent(Persona person)
		{

			if (CheckingВuplicates(Parents.Persona, person))
			{
				throw new Exception($"Нарушение родственных связей, {person.Name} уже является родителем");
			}
		}

		private void NotGrandparent(Persona person)
		{

			if (CheckingВuplicates(Grandparents.Persona, person))
			{
				throw new Exception($"Нарушение родственных связей, {person.Name} уже является Дедушко/Бабушкой");
			}
		}

		private void NotBrotherAndSister(Persona person)
		{
			if (CheckingВuplicates(BrotherAndSister.Persona, person))
			{
				throw new Exception($"Нарушение родственных связей, {person.Name} уже является Братом/Сестрой");
			}
		}

		private void NotGrandchildren(Persona person)
		{
			if (CheckingВuplicates(Grandchildren.Persona, person))
			{
				throw new Exception($"Нарушение родственных связей, {person.Name} уже является Внуком/Внучкой");
			}
		}

		private void NotChildren(Persona person)
		{
			if (CheckingВuplicates(Children.Persona, person))
			{
				throw new Exception($"Нарушение родственных связей, {person.Name} уже является ребенком");
			}
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
