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

			// Семья #1
			var gena = new Persona("Gena");
			var maha = new Persona("Maha");
			var ivan = new Persona("Ivan") ;


			// Семья #2 + maha и saha
			var oleg = new Persona("Oleg");
			var vera = new Persona("Vera");



			// Семья #3
			var saha = new Persona("Saha");
			var nik = new Persona("Nik");
			var julia = new Persona("Julia");




			// Проверка: Сам себе родственник
			//ivan.SetPartner(ivan);

			// Семья #1 установка связей
			gena.SetPartner(maha);
			ivan.SetParent(maha);
			ivan.SetParent(gena);


			// Семья #2 установка связей
			oleg.SetPartner(vera);
			maha.SetParent(oleg);
            saha.SetParent(oleg);
			maha.SetParent(vera);
			saha.SetParent(vera);


			// Семья #3 установка связей
			nik.SetPartner(saha);
			julia.SetParent(nik);
			julia.SetParent(saha);


			// Проверка: Корректных связей родства 
			//saha.SetChilren(ivan);
			//ivan.SetParent(maha);
			//ivan.SetParent(oleg);


			//Родителей
			Console.WriteLine(ivan.Name + " Родители =" + ivan.ViewPersona(ivan.GetParent()));
			Console.WriteLine(ivan.Name + " Дети =" + ivan.ViewPersona(ivan.GetChildren()));

			Console.WriteLine(maha.Name + " Родители =" + maha.ViewPersona(maha.GetParent()));
			Console.WriteLine(maha.Name + " Дети =" + maha.ViewPersona(maha.GetChildren()));

			Console.WriteLine(saha.Name + " Родители =" + saha.ViewPersona(saha.GetParent()));
			Console.WriteLine(saha.Name + " Дети =" + saha.ViewPersona(saha.GetChildren()));

			Console.WriteLine(oleg.Name + " Родители =" + oleg.ViewPersona(oleg.GetParent()));
			Console.WriteLine(oleg.Name + " Дети =" + oleg.ViewPersona(oleg.GetChildren()));


			//Двоюродных братьев и сестер
			Console.WriteLine(ivan.Name + " Двоюродные братья и сестры =" + ivan.ViewPersona(ivan.GetCousins()));
			Console.WriteLine(julia.Name + " Двоюродные братья и сестры =" + julia.ViewPersona(julia.GetCousins()));

			//Дядюшек и тетушек
			Console.WriteLine(ivan.Name + " Дядюшки и тетушки =" + ivan.ViewPersona(ivan.GetAuntAndUncle()));
			Console.WriteLine(julia.Name + " Дядюшки и тетушки =" + julia.ViewPersona(julia.GetAuntAndUncle()));


			//In - laws(cвекра и свекрови или тестя и тещи)
			Console.WriteLine(nik.Name + " In-laws =" + nik.ViewPersona(nik.GetLaws()));
			Console.WriteLine(gena.Name + " In-laws =" + gena.ViewPersona(gena.GetLaws()));




			var key = Console.ReadKey();
			if (key.Key == ConsoleKey.Escape)
			{
				return;
			}
		}
	}
}
