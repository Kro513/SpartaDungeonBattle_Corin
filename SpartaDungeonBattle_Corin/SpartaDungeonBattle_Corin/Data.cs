using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpartaDungeonBattle_Corin
{

	//캐릭터 데이터
	public class Character
	{
		public string Name { get; set; }
		public string Class { get; set; }
		public int Level { get; set; }
		public int Hp { get; set; }
		public int Hp2 { get; set; }
		public int Atk { get; set; }
		public int Def { get; set; }
		public int Gold { get; set; }
		public bool IsDead { get; set; }

		public Character(string name, string classtype, int level, int hp, int hp2, int atk, int def, int gold, bool isDead)
		{
			Name = name;
			Class = classtype;
			Level = level;
			Hp = hp;
			Hp2 = hp2;
			Atk = atk;
			Def = def;
			Gold = gold;
			IsDead = false;
		}
	}

	//캐릭터 상속 -> 워리어
	public class Warrior : Character
	{
		public Warrior() : base("dd", "전사", 1, 100, 100, 10, 10, 1500, false)
		{

		}

		// 캐릭터가 맞을때
		public void Takedamage(int damage, string name1)
		{
			Hp2 = Hp;
			Hp -= damage;
			Console.Clear();
			Console.WriteLine("=============================");
			Console.WriteLine("Battle!");
			Console.WriteLine("");
			Console.WriteLine("");
			Console.WriteLine("");
			Console.WriteLine("[적의 턴]");
			Console.WriteLine("");
			Console.WriteLine("{0}의 공격!", name1);
			Console.WriteLine("{0} 을(를) 맞췄습니다. [데미지 : {1}]", Name, damage);
			Console.WriteLine("");
			Console.WriteLine("{0}", Name);
			Console.WriteLine("HP {0} -> {1}", Hp, Hp - damage);

			Console.WriteLine("");
			Console.WriteLine("0. 다음");
			if (Hp <= 0)
			{
				IsDead = true;
				Console.WriteLine("플레이어 사망");

			}
		}
	}

}
