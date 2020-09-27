using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Famaily
{
	class Program
	{
		static void Main(string[] args)
		{
			var maha = new Persona("Maha");
			var ivan = new Persona("Ivan") ;

			var gena = new Persona("Gena");
			var nik = new Persona("nik");

			var saha = new Persona("Saha");
			var oleg = new Persona("Oleg");
			var vera = new Persona("Vera");

			saha.SetParent(oleg);
			saha.SetParent(vera);


			saha.SetPartner(gena);

			maha.SetBrotherAndSister(gena);
			ivan.SetParent(maha);
		
			//maha.SetBrotherAndSister(gena);

			nik.SetParent(gena);



			Console.WriteLine(ivan.Name + " " + ivan.Parents.ViewPersona());
			Console.WriteLine(ivan.ViewAuntAndUncle());
			Console.WriteLine(ivan.ViewCousins());

			Console.WriteLine("Свекра и свекрови Гены" + gena.Partner.Parents.ViewPersona());




			var key = Console.ReadKey();
			if (key.Key == ConsoleKey.Escape)
			{
				return;
			}
		}
	}
}
