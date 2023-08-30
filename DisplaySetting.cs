using System;
using System.Net.Security;
using System.Reflection.Emit;
using System.Runtime.InteropServices;
using System.Security.Cryptography.X509Certificates;
using System.Xml.Linq;

namespace class_status_random
{


}

class Character
{
	public Character(string name, string classtype, int level, int attack, int defense, int health, int gold)
	{
		Level = 1;
		Name = name;
		Class = classtype;
		Attack = level;
		Defense = defense;
		Health = health;
		Gold = gold;
	}
	public int Level { get; set; }
	public string Name { get; set; }
	public string Class { get; set; }
	public int Attack { get; set; }
	public int Defense { get; set; }
	public int Health { get; set; }
	public int Gold { get; set; }

}

class Program


{

	public static Character player;


	static void ShowStartScreen()
	{
		Console.WriteLine();

		Console.WriteLine("스파르타 던전에 오신 여러분 환영합니다.");
		Console.WriteLine("이제 전투를 시작할 수 있습니다.\n");
		Console.WriteLine();

	}





	static void Main(string[] args)
	{
		InitData();
		ShowStartScreen();
		DisplayGameIntro();
		ShowCharacterInfo();
	}

	private static void InitData()
	{
		player = new Character("unknown", "전사", 10, 1, 5, 100, 1500);



	}

	static void ShowCharacterInfo()
	{

	}



	static void DisplayGameIntro()
	{
		Console.WriteLine("1. 상태 보기");
		Console.WriteLine("2. 전투 시작");
		Console.WriteLine();
		Console.WriteLine("원하시는 행동을 입력해주세요: ");
		Console.WriteLine(">>");

		string choice = Console.ReadLine();

		switch (choice)
		{

			case "1":
				Console.WriteLine("상태를 확인합니다."); // 상태 보기에 관한 내용을 추가할 수 있습니다.
				DisplayStatus();
				break;

			case "2":
				Console.WriteLine("전투를 시작합니다."); // 전투 시작에 관한 내용을 추가할 수 있습니다.
				break;

			default:
				Console.WriteLine("잘못된 입력입니다.");
				DisplayGameIntro();
				Console.Clear();
				break;



		}
	}

	static void DisplayStatus()
	{
		Console.WriteLine("상태 보기");
		Console.WriteLine("캐릭터의 정보가 표시됩니다.");
		Console.WriteLine();
		Console.WriteLine($"Lv. {Program.player.Level}");
		Console.WriteLine($"Name ( {Program.player.Name} )");
		Console.WriteLine($"공격력 : {Program.player.Attack}");
		Console.WriteLine($"방어력 : {Program.player.Defense}");
		Console.WriteLine($"체력 : {Program.player.Health}");
		Console.WriteLine($"Gold : {Program.player.Gold} G\n");

		Console.WriteLine();
		Console.WriteLine("0. 나가기");
		Console.WriteLine();
		Console.WriteLine("원하시는 행동을 입력해주세요");
		Console.WriteLine(">>");


		string input = Console.ReadLine();
		switch (input)
		{
			case "0":
				Console.WriteLine("프로그램을 종료합니다.");
				DisplayGameIntro();
				break;

			default:
				Console.WriteLine("잘못된 입력입니다.");
				Console.Clear();
				DisplayStatus();

				break;

		}
	}


}
