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


        static void DisplayTitle(string title)
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine(title);
            Console.ResetColor();
        }



        public string Name { get; set; }
        public string Class { get; set; }
        public int Level { get; set; }
        public int MaxHp { get; set; }
        public int Hp { get; set; }
        public int MaxMp { get; set; }
        public int Mp { get; set; }
        public int Atk { get; set; }
        public int Def { get; set; }
        public int Gold { get; set; }
        public bool IsDead { get; set; }

        public int Experience { get; set; }


        public Character(string name, string classtype, int hp, int atk, int def, int gold, bool isDead, int maxHp, int mp, int maxMp)
        {
            Name = name;
            Class = classtype;
            Level = 1;
            Hp = hp;
            Atk = atk;
            Def = def;
            Gold = gold;
            IsDead = false;
            Experience = 0;
            MaxHp = Hp;
            Mp = mp;
            MaxMp = Mp;
        }

        public virtual void LevelUp()
        {
            Experience = Experience - (Level * 2);

            Level++;
            Console.WriteLine($"{Name}이 {Level}레벨로 레벨업했습니다!");

            string classtype = Program.player.Class;

            switch (classtype)
            {
                case "전사":
					Hp += 20;
                    MaxHp += 20;
                    Mp += 2;
					MaxMp += 2;
					Atk += 2;
                    Def += 2;
					Console.WriteLine($"{Name}의 체력이 올랐습니다!. 현재 HP: {Hp}/{MaxHp}");
					Console.WriteLine($"{Name}의 마나량이 올랐습니다!. 현재 HP: {Mp}/{MaxMp}");
					Console.WriteLine($"{Name}의 공격력이 올랐습니다!. {Atk - 2} => {Atk}");
					Console.WriteLine($"{Name}의 방어력이 올랐습니다!. {Def - 2} => {Def}");
					break;
                case "궁수":
					Hp += 10;
					MaxHp += 10;
					Mp += 1;
					MaxMp += 1;
					Atk += 3;
                    Def += 1;
					Console.WriteLine($"{Name}의 체력이 올랐습니다!. 현재 HP: {Hp}/{MaxHp}");
					Console.WriteLine($"{Name}의 마나량이 올랐습니다!. 현재 HP: {Mp}/{MaxMp}");
					Console.WriteLine($"{Name}의 공격력이 올랐습니다!. {Atk - 3} => {Atk}");
					Console.WriteLine($"{Name}의 방어력이 올랐습니다!. {Def - 1} => {Def}");
					break;
                case "마법사":
					Hp += 10;
					MaxHp += 10;
					Mp += 3;
					MaxMp += 3;
					Atk += 1;
					Def += 3;
					Console.WriteLine($"{Name}의 체력이 올랐습니다!. 현재 HP: {Hp}/{MaxHp}");
					Console.WriteLine($"{Name}의 마나량이 올랐습니다!. 현재 HP: {Mp}/{MaxMp}");
					Console.WriteLine($"{Name}의 공격력이 올랐습니다!. {Atk - 1} => {Atk}");
					Console.WriteLine($"{Name}의 방어력이 올랐습니다!. {Def - 3} => {Def}");
                    break;
			}
        }
        public void Takedamage(int damage, string name1)
        {

            Console.Clear();
            Console.WriteLine("=============================");
            Console.WriteLine("Battle!");
            Console.WriteLine("");
            Console.WriteLine("");
            Console.WriteLine("");
            Console.WriteLine("[적의 턴]");
            Console.WriteLine("");
            Console.WriteLine("{0}의 공격!", name1);
            damage -= Def / 3;
            if(damage < 0)
            {
                damage = 0;
            }
            Console.WriteLine("{0} 을(를) 맞췄습니다. [데미지 : {1}]", Name, damage);
            Console.WriteLine("");
            Console.WriteLine("{0}", Name);
            Console.WriteLine("HP {0} -> {1}", Hp, Hp - damage );
            Hp -= damage;
            Console.WriteLine("");
            Console.WriteLine("0. 다음");
            if (Hp <= 0)
            {
                IsDead = true;
                Console.Clear();
                Console.WriteLine("=============================");
                Console.WriteLine("");
                Console.WriteLine("");
                Console.WriteLine("플레이어 사망");
                Console.WriteLine("");
                Console.WriteLine("");
                Console.WriteLine("=============================");
            }
        }
    }

    //캐릭터 상속 -> 워리어
    public class Warrior : Character
    {
        public Warrior() : base("", "", 100, 10, 10, 1500, false, 100, 20, 20)
        {


        }
		public override void LevelUp()
		{
			base.LevelUp();
		}
	}

    //캐릭터 상속 -> 메이지
    public class Mage : Character
    {
        public Mage() : base("", "", 100, 10, 10, 1500, false, 100, 40, 40)
        {
        }

		public override void LevelUp()
		{
			base.LevelUp();
		}
	}

    //캐릭터 상속 -> 아처
    public class Archer : Character
    {
        public Archer() : base("", "", 100, 10, 10, 1500, false, 100, 10, 10)
        {

        }

		public override void LevelUp()
		{
			base.LevelUp();
		}
	}

}
