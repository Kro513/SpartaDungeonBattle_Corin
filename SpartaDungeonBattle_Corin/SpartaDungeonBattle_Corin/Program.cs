namespace SpartaDungeonBattle_Corin
{
	static class EndBattle
	{
		static bool BattleResult
		{
			if (player.Hp <= 0)? BattleResult = false : true;
		}


		static void DisplayBattleResult()
		{
			Console.Clear();

			DisplayTitle("Battle!! - Result")

			Console.WriteLine();
			Console.WriteLine($"{BattleResult}");
			Console.WriteLine();
			
			if (BattleResult = true)
			{
				Console.WriteLine($"던전에서 몬스터 {HowManyEnemy}마리를 잡았습니다.");
				Console.WriteLine();
				Console.WriteLine($"Lv. {player.Level} {player.Name}");
				Console.WriteLine($"Hp {player.Hp} -> {player.Hp}");
			}
			else
			{
				Console.WirteLine("You Lose");
				Console.WriteLine();
				Console.WriteLine($"Lv. {player.Level} {player.Name}");
				Console.WriteLine($"Hp {player.Hp} -> 0");
			}
			
			Console.WriteLine();
			Console.WriteLine("0. 다음");

			int input = CheckValidInput(0, 0);
			switch (input)
			{
				case 0:
					DisplayEndGame();
					break;
			}
		}

		static void DisplayEndGame()
		{
			Console.BackgroundColor = ConsoleColor.White;
			Console.ForegroundColor = ConsoleColor.Magenta;
			Console.WriteLine("===Game Over===");
		}
	}
}
