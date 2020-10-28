using System;
using System.Collections.Generic;
using System.Diagnostics.Tracing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Family
{
	public class Persona
	{
		public string Name { get; }

		private Persona Partner;
		private Family Parents;
		private Family Children;

		public Persona(string name)
		{
			Name = name;
			Parents = new Family();
			Children = new Family();
		}


		// Установление связей родства
		public void SetPartner(Persona partner)
		{
			CheckingNotSelfKinship(partner);


			//При создании новой связи между партнерами удаляются старые связи обоих партнеров
			if (this.Partner != null)
			{
				this.Partner.Partner = null;
			}

			if (partner.Partner != null)
            {
				partner.Partner.Partner = null;
			}


			Partner = partner;
            partner.Partner = this;
            //partner.SetPartner(this);
		}

		public void SetParent(Persona parent)
		{
			CheckingNotSelfKinship(parent);


			//Проверка на кол-во родителей
            if (this.GetParent().Count>1)
            {
				throw new Exception($"Нарушение родственных связей, у {this.Name} уже есть родители");
			}
			CheckingViolationOfFamilyTiesUp(parent, this);
			CheckingViolationOfFamilyTiesDown(parent, this);

			Parents.AddRelative(parent);
			parent.SetChilren(this);
		}

		public void SetChilren(Persona child)
		{
			CheckingNotSelfKinship(child);
			CheckingViolationOfFamilyTiesUp(this, child);
			CheckingViolationOfFamilyTiesDown(this, child);
			Children.AddRelative(child);
			//child.Parents.AddRelative(this);
		}

		// Проверка связей родства путем прохода по генеалогическому древу
		private void CheckingViolationOfFamilyTiesUp(Persona parent, Persona child)
		{

			if (CheckDuplicates(parent.GetParent(), child))
			{
				throw new Exception($"Нарушение родственных связей, {child.Name} уже является родителем {parent.Name}");
			}

			foreach (var grandparent in parent.GetParent())
			{
				CheckingViolationOfFamilyTiesUp(grandparent, child);
			}
		}

		private void CheckingViolationOfFamilyTiesDown(Persona parent, Persona child)
		{

			if (CheckDuplicates(parent.GetChildren(), child))
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

			if (persons != null)
            {
                foreach (var person in persons)
                {
                    names += person.Name + ",";
                }
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
			return this.GetParent().SelectMany(parent => parent.GetParent()).ToList();
		}

		public List<Persona> GetGrandchildren()
		{
			return this.GetChildren().SelectMany(child => child.GetChildren()).ToList();
		}

		public List<Persona> GetBrotherAndSister()
		{
			List<Persona> brotherAndSister = new List<Persona>();

			if (this.GetParent().Count()>0)
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

            foreach (var grendperent in this.GetGrandparents())
            {
               
                foreach (var auntAndUncle in grendperent.GetChildren())
                {
					if ((CheckDuplicates(this.GetParent(), auntAndUncle) == false) 
						&(CheckDuplicates(auntAndUncles, auntAndUncle) == false))

					{
						auntAndUncles.Add(auntAndUncle);

						if( auntAndUncle.GetPartner() != null)
                        {
							auntAndUncles.Add(auntAndUncle.GetPartner());
						}
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
					if ((CheckDuplicates(cousins, child) == false)&&(child != this))
					{
						cousins.Add(child);
					}
				}
			}

			return cousins;
		}


		public List<Persona> GetLaws()
        {
			if (this.GetPartner() != null)
			{
				return this.GetPartner().GetParent();
			}

			return null;
		}


		private void CheckingNotSelfKinship(Persona person)
		{

			if (person == this)
			{
				throw new Exception($"Нарушение родственных связей, {person.Name} не может быть сам себе родственником");
			}
		}

		private bool CheckDuplicates(List<Persona> list, Persona person)
		{
			return list.Any(item => item == person);
		}

	}
}
