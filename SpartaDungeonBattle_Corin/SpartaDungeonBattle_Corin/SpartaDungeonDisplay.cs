using System;
using System.Net.Security;
using System.Reflection.Emit;
using System.Runtime.InteropServices;
using System.Security.Cryptography.X509Certificates;
using System.Xml.Linq;
using static SpartaDungeonBattle_Corin.Character;
using static SpartaDungeonBattle_Corin.Mob;
using static SpartaDungeonBattle_Corin.Program;

namespace SpartaDungeonBattle_Corin
{
	public class Program
	{
		public static Character player;

		public delegate void TakedamageHandler(int damage, string name);

		static void Main(string[] args)
		{
			CharacterSetting();
			DisplayStartScreen();
			DisplayGameIntro();
			DisplayStatus();
		}

		#region 초기설정
		public static void CharacterSetting()
		{
			//캐릭터 정보 세팅
			player = new Character("플레이어", "전사", 1, 100, 100, 10, 10, 1500, false);
		}

		#endregion

		#region 게임 화면 출력

		//첫 화면
		static void DisplayStartScreen()
		{
			Console.Clear();

			Console.WriteLine("스파르타 던전에 오신 여러분 환영합니다.");
			Console.WriteLine("이제 전투를 시작할 수 있습니다.\n");
			Console.WriteLine();

		}

		//게임 진입
		static void DisplayGameIntro()
		{
			Battle battle = new Battle();

			Console.Clear();

			Console.WriteLine("1. 상태 보기");
			Console.WriteLine("2. 전투 시작");
			Console.WriteLine();
			Console.WriteLine("원하시는 행동을 입력해주세요: ");
			Console.WriteLine(">>");

			int input = CheckValidInput(1, 2);

			switch (input)
			{
				case 1:
					Console.WriteLine("상태를 확인합니다."); // 상태 보기에 관한 내용을 추가할 수 있습니다.
					DisplayStatus();
					break;

				case 2:
					Console.WriteLine("전투를 시작합니다."); // 전투 시작에 관한 내용을 추가할 수 있습니다.
					battle.BattleStart();
					break;
			}
		}

		//상태창
		static void DisplayStatus()
		{
			Console.Clear();

			DisplayTitle("상태 보기");
			Console.WriteLine("캐릭터의 정보가 표시됩니다.");
			Console.WriteLine();
			Console.WriteLine($"Lv. {player.Level}");
			Console.WriteLine($"{player.Name}({player.Class})");
			Console.WriteLine($"공격력 : {player.Atk}");
			Console.WriteLine($"방어력 : {player.Def}");
			Console.WriteLine($"체력 : {player.Hp}");
			Console.WriteLine($"Gold : {player.Gold} G\n");

			Console.WriteLine();
			Console.WriteLine("0. 나가기");
			Console.WriteLine();
			Console.WriteLine("원하시는 행동을 입력해주세요");
			Console.WriteLine(">>");


			int input = CheckValidInput(0, 0);
			switch (input)
			{
				case 0:
					Console.WriteLine("프로그램을 종료합니다.");
					DisplayGameIntro();
					break;
			}
		}

		#endregion

		#region Utility

		//화면 전환 함수
		static int CheckValidInput(int min, int max)
		{
			while (true)
			{
				string input = Console.ReadLine();

				bool parseSuccess = int.TryParse(input, out var ret);
				if (parseSuccess)
				{
					if (ret >= min && ret <= max)
						return ret;
				}

				DisplayError("잘못된 입력입니다.");
			}
		}

		//화면이름 색상설정
		static void DisplayTitle(string title)
		{
			Console.ForegroundColor = ConsoleColor.Yellow;
			Console.WriteLine(title);
			Console.ResetColor();
		}

		//에러
		static void DisplayError(string error)
		{
			Console.ForegroundColor = ConsoleColor.Red;
			Console.WriteLine(error);
			Console.ResetColor();
		}

		//색상 바꿀 때 숫자로
		public static void ChangeColor(string message, int consoleColor)
		{
			/* 
			 0 검정
			 1 진파랑
			 2 진녹색
			 3 녹청
			 4 빨강
			 5 자홍
			 6 노랑
			 7 회색
			 8 진회색
			 9 파랑
			 10 녹색
			 11~~~~
			 */

			Console.ForegroundColor = (ConsoleColor)consoleColor;
			Console.WriteLine(message, consoleColor);
			Console.ResetColor();
		}
		#endregion
	}
}