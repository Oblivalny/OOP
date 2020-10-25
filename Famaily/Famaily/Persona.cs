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

		private Persona Partner;
		private Famaily Parents;
		private Famaily Children;

		public Persona(string name)
		{
			Name = name;
			Parents = new Famaily();
			Children = new Famaily();
		}


		// Установление связей родства
		public void SetPartner(Persona partner)
		{
			CheckingNotSelfKinship(partner);

			Partner = partner;
            partner.Partner = this;
            //partner.SetPartner(this);
		}

		public void SetParent(Persona parent)
		{
			CheckingNotSelfKinship(parent);
			CheckingViolationOfFamilyTiesUp(parent, this);

			Parents.AddRelative(parent);
			parent.SetChilren(this);
		}

		public void SetChilren(Persona child)
		{
			CheckingNotSelfKinship(child);
			CheckingViolationOfFamilyTiesUp(this, child);
			Children.AddRelative(child);
			//child.Parents.AddRelative(this);
		}

		// Проверка связей родства путем прохода по генеалогическому древу
		private void CheckingViolationOfFamilyTiesUp(Persona parent, Persona child)
		{

			if (CheckingВuplicates(parent.GetParent(), child))
			{
				throw new Exception($"Нарушение родственных связей, {child.Name} уже является родителем {parent.Name}");
			}

			foreach (var grandparent in parent.GetParent())
			{
				CheckingViolationOfFamilyTiesUp(grandparent, child);
			}

			CheckingViolationOfFamilyTiesDown(parent, child);
		}

		private void CheckingViolationOfFamilyTiesDown(Persona parent, Persona child)
		{

			if (CheckingВuplicates(parent.GetChildren(), child))
			{
				throw new Exception($"Нарушение родственных связей, {parent.Name} уже является родителем {child.Name}");
			}

			foreach (var brotherOrSister in parent.GetChildren())
			{
				CheckingViolationOfFamilyTiesDown(brotherOrSister, child);
			}
		}

		public string ViewPersona(List<Persona> persons)
		{
			var names = " ";

			foreach (var person in persons)
			{
				names += person.Name + ",";
			}
			return names;
		}

		// Получение персон определенного родства
		public List<Persona> GetParent()
        {
			return this.Parents.GetPersona();
        }

		public List<Persona> GetChildren()
		{
			return this.Children.GetPersona();
		}

		public Persona GetPartner()
		{
			return this.Partner;
		}


		public List<Persona> GetGrandparents()
		{
			List<Persona> grandparents = new List<Persona>();
			foreach (var parent in this.GetParent())
			{
				foreach (var grandparent in parent.GetParent())
				{
					grandparents.Add(grandparent);
				}
			}
			return grandparents;
		}

		public List<Persona> GetGrandchildren()
		{
			List<Persona> grandchildren = new List<Persona>();

			foreach (var child in this.GetChildren())
			{
				foreach (var grandchild in child.GetChildren())
				{
					grandchildren.Add(grandchild);
				}
			}

			return grandchildren;
		}

		public List<Persona> GetBrotherAndSister()
		{
			List<Persona> brotherAndSister = new List<Persona>();

			if (this.GetParent().Count() >0)
			{
				foreach (var child in this.GetParent().First().GetChildren())
				{
					if (child != this)
					{
						brotherAndSister.Add(child);
					}

				}
			}

			return brotherAndSister;
		}

		public List<Persona> GetAuntAndUncle()
        {

            List<Persona> auntAndUncles = new List<Persona>();


            foreach (var grentperent in this.GetGrandparents())
            {
               
                foreach (var auntAndUncle in grentperent.GetChildren())
                {
					if ((CheckingВuplicates(this.GetParent(), auntAndUncle) == false) 
						&(CheckingВuplicates(auntAndUncles, auntAndUncle) == false))

					{
						auntAndUncles.Add(auntAndUncle);
						auntAndUncles.Add(auntAndUncle.GetPartner());
					}
                }
            }

            return auntAndUncles;
		}


		public List<Persona> GetCousins()
		{
			List<Persona> cousins = new List<Persona>();


			foreach (var auntAndUncle in this.GetAuntAndUncle())
			{

				foreach (var child in auntAndUncle.GetChildren())
				{
					if (CheckingВuplicates(cousins, child) == false)
					{
						cousins.Add(child);
					}
				}
			}

			return cousins;
		}


		public List<Persona> GetLaws()
        {
			return this.GetPartner().GetParent();
		}


		private void CheckingNotSelfKinship(Persona person)
		{

			if (person == this)
			{
				throw new Exception($"Нарушение родственных связей, {person.Name} не может быть сам себе родственником");
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
