using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static SpartaDungeonBattle_Corin.Program;

namespace SpartaDungeonBattle_Corin
{
    //몹 설정

    public class Mob
    {
        public string Name { get; }
        public int Hp { get; set; }
        public int Atk { get; set; }

        public bool IsDead { get; set; }
        public int DeadCount { get; set; }
        public static Character Player { get; set; }
        public Mob(string name, int hp, int atk, Character player)
        {
            Name = name;
            Hp = hp;
            Atk = atk;
            DeadCount = 0;
            Player = player;
            IsDead = false;
        }


        public void Takedamage(int damage, string name1) //몹이 맞을때
        {


            Console.Clear();
            Console.WriteLine("=============================");
            Console.WriteLine("Battle!");
            Console.WriteLine("");
            Console.WriteLine("");
            Console.WriteLine("");
            Console.WriteLine("[당신의 턴]");
            Console.WriteLine("");
            Console.WriteLine("{0}의 공격!!", name1);
            Console.WriteLine();
            damage = new Random().Next((int)(damage - damage * 0.1), (int)(damage + damage * 0.1) + 1);
            int accury = new Random().Next(1, 11);
            if (accury > 9) //회피 했을때 = 미적중
            {
                damage = 0;
                Console.WriteLine("Lv.3{0} 을(를) 공격했지만 적중하지 않았습니다.", Name);
                Console.WriteLine("");
                Console.WriteLine("");
                Console.WriteLine("0. 다음");
            }
            else //몬스터 회피 못했을때 = 적중
            {
                int critical = new Random().Next(1, 101);
                if (critical > 50)
                {
                    damage = damage + (int)(damage * 0.6);
                    Console.WriteLine("Lv.3{0} 을(를) 맞췄습니다. [데미지 : {1}]", Name, damage);
                    ChangeColor("Critical!!", 6);
                    Console.WriteLine("");
                    Console.WriteLine("Lv.3{0}", Name);
                    if (Hp - damage <= 0)
                    {
                        Console.WriteLine("HP {0} -> Dead", Hp);
                    }
                    else
                    {
                        Console.WriteLine("HP {0} -> Hp {1}", Hp, Hp - damage);
                    }
                    Hp -= damage;
                    Console.WriteLine("");
                    Console.WriteLine("");
                    Console.WriteLine("0. 다음");
                    if (Hp <= 0)
                    {
                        IsDead = true;
                        DeadCount++;
                        Hp = 0;
                    }

                }
                else
                {
                    Console.WriteLine("Lv.3{0} 을(를) 맞췄습니다. [데미지 : {1}]", Name, damage);
                    Console.WriteLine("");
                    Console.WriteLine("Lv.3{0}", Name);
                    if (Hp - damage <= 0)
                    {
                        Console.WriteLine("HP {0} -> Dead", Hp);
                    }
                    else
                    {
                        Console.WriteLine("HP {0} -> Hp{1}", Hp, Hp - damage);
                    }
                    Hp -= damage;
                    Console.WriteLine("");
                    Console.WriteLine("");
                    Console.WriteLine("0. 다음");
                    if (Hp <= 0)
                    {
                        IsDead = true;
                        DeadCount++;
                        Hp = 0;
                    }

                }
            }
        }

        public class Battle
        {

            public class Enemy
            {
                public event TakedamageHandler OnAttack;
                public event TakedamageHandler OnHeatAttack;

                public void Attack(int damage, string name) // 때릴때
                {
                    OnAttack?.Invoke(damage, name);

                }
                public void HeatAttack(int damage, string name) // 맞을때
                {
                    OnHeatAttack?.Invoke(damage, name);

                }
            }
            Enemy enemy = new Enemy();

            List<Mob> mobs = new List<Mob>();
            public void BattleStart()
            {
                string mobname = "";
                int mobscount = new Random().Next(1, 5); // 몬스터 마릿수결정
                while (mobs.Count < mobscount)
                {

                    int randomname = new Random().Next(1, 4); //몬스터 이름결정
                    switch (randomname)
                    {
                        case 1: mobname = "미니언"; break;
                        case 2: mobname = "공허충"; break;
                        case 3: mobname = "바론"; break;

                    }
                    int randomhp = new Random().Next(5, 9); //몬스터 체력랜덤
                    int randomatk = new Random().Next(2, 9); // 몬스터 공격력랜덤

                    Mob mob = new Mob(mobname, randomhp, randomatk, player);

                    mobs.Add(mob);
                }
                Console.Clear();

                Console.WriteLine();
                Console.WriteLine("=============================");
                Console.WriteLine("Battle!");
                Console.WriteLine("");
                Console.WriteLine();
                Console.WriteLine("");

                foreach (var text in mobs) //몬스터 출력
                {
                    Console.WriteLine($"{mobs.IndexOf(text) + 1}.{text.Name} \tHp: {text.Hp}");
                }
                Console.WriteLine("");
                Console.WriteLine("");
                Console.WriteLine("-----------------------------");
                Console.WriteLine("[내정보]");
                Console.WriteLine();
                Console.WriteLine($"Lv.{player.Level} {player.Name} {player.Class}");
                Console.WriteLine();
                Console.WriteLine($"Hp {player.Hp}/{player.MaxHp}");
				Console.WriteLine($"Hp {player.Mp}/{player.MaxMp}");
				Console.WriteLine("-----------------------------");
                Console.WriteLine();
                Console.WriteLine("0. 마을로 돌아가기");
                Console.WriteLine("");
                Console.WriteLine("대상을 선택해주세요");
                Console.Write(">>");

                while (player.IsDead == false && !mobs.All(mob => mob.IsDead))
                {
                    int input = int.Parse(Console.ReadLine());

                    if (input >= 1 && input <= mobs.Count)
                    {
                        Mob selectedMob = mobs[input - 1];

                        if (selectedMob.IsDead)
                        {
                            Console.Write("이미 죽은 대상입니다.");
                        }
                        else
                        {
                            enemy.OnAttack += selectedMob.Takedamage;
                            enemy.OnHeatAttack += player.Takedamage;
                            enemy.Attack(player.Atk, player.Name);
                            Console.ReadKey();
                            Thread.Sleep(100);
                            foreach (var mob in mobs)
                            {
                                if (!mob.IsDead)
                                {
                                    enemy.HeatAttack(mob.Atk, mob.Name);
                                    Console.ReadKey();
                                }
                            }
                            Console.Clear();
                            Console.WriteLine("=============================");
                            Console.WriteLine("Battle!");
                            Console.WriteLine("");
                            Console.WriteLine("");

                            foreach (var text in mobs)
                            {
                                if (text.IsDead)
                                {
                                    Console.ForegroundColor = ConsoleColor.Red;
                                    Console.WriteLine($"※{mobs.IndexOf(text) + 1}.{text.Name} \tHp: {text.Hp}");
                                    Console.ResetColor();
                                }
                                else Console.WriteLine($"{mobs.IndexOf(text) + 1}.{text.Name} \tHp: {text.Hp}");
                            }

							Console.WriteLine("");
							Console.WriteLine("");
							Console.WriteLine("-----------------------------");
							Console.WriteLine("[내정보]");
							Console.WriteLine();
							Console.WriteLine($"Lv.{player.Level} {player.Name} {player.Class}");
							Console.WriteLine();
							Console.WriteLine($"Hp {player.Hp}/{player.MaxHp}");
							Console.WriteLine("-----------------------------");
							Console.WriteLine();
							Console.WriteLine("0. 마을로 돌아가기");
							Console.WriteLine("");
							Console.WriteLine("대상을 선택해주세요");
							Console.Write(">>");
							enemy.OnAttack -= selectedMob.Takedamage;
                            enemy.OnHeatAttack -= player.Takedamage;

                        }
                    }
					else if(input == 0)
					{
						DisplayGameIntro();
					}

					else
                    {
                        Console.WriteLine("올바른 값을 입력해주세요");
                    }

                }
                if (player.IsDead == false)
                {
                    BattleEnd();
                    BattleGetExp();
					BattleReword();
                }
            }
            public void BattleEnd()
            {
                Console.Clear();
                Console.WriteLine("=============================");
                Console.WriteLine("");
                ChangeColor("Battle!! - Result", 6);
                Console.WriteLine("");
                ChangeColor("Victory", 2);
                Console.WriteLine("");
                Console.WriteLine("{0}", player.Name);
                Console.WriteLine("남은 Hp : {0}", Player.Hp);
                Console.WriteLine("");
                Console.WriteLine("0. 다음");
                Console.WriteLine("");
                Console.WriteLine(">>");
                Console.ReadKey();
            }

            public void BattleGetExp()
            {
                Console.Clear();

				int battleExperience = mobs.Count * 2;
				Console.WriteLine($"{player.Name}이(가) 전투에서 승리하여 {battleExperience}의 경험치를 획득했습니다!");
				player.Experience += battleExperience;

				while (player.Experience >= player.Level * 10)
				{
					player.LevelUp();
				}

				Console.WriteLine();
                Console.WriteLine($"현재 {player.Name}의 레벨 : {player.Level}");
                Console.WriteLine($"현재 {player.Name}의 경험치 : {player.Experience}");

				int experienceToLevelUp = player.Level * 10 - player.Experience;
				Console.WriteLine($"다음 레벨까지 필요한 경험치: {experienceToLevelUp}");

                
				Console.WriteLine("");
				Console.WriteLine("0. 다음");
				Console.WriteLine("");
				Console.WriteLine(">>");
				Console.ReadKey();


			}
            public void BattleReword()
            {
                Console.Clear();
                Console.WriteLine();
                Console.WriteLine("====================================");
                Console.WriteLine("보상을 선택해주세요");
                Console.WriteLine("1.체력 회복");
                Console.WriteLine("2.힘의 물약");
                Console.WriteLine("3.방어의 물약");
                Console.WriteLine("====================================");
                int input = CheckValidInput(1, 3);

                switch(input)
                {
                    case 1: RewordHp();Console.ReadLine();
                        break; 
                    case 2: RewordAtk(); Console.ReadLine();
                        break;
                    case 3: RewordDef(); Console.ReadLine();
                        break;
                }

				Console.WriteLine("");
				Console.WriteLine("0. 마을로 돌아가기");
				Console.WriteLine("");
				Console.WriteLine(">>");
				Console.ReadKey();

                DisplayGameIntro();
			}

            public void RewordHp()
            {
                Console.WriteLine("");
                ChangeColor("Hp를 회복하였습니다 !", 6);
                Console.WriteLine("Hp : {0} -> {1}",player.Hp,player.Hp+20);
                player.Hp += 20;
                
            }
            public void RewordAtk()
            {
                Console.WriteLine("");
                ChangeColor("공격력이 상승하였습니다. !", 6);
                Console.WriteLine("공격력 : {0} -> {1}",player.Atk,player.Atk+1);
                player.Atk += 1;
            }
            public void RewordDef()
            {
                Console.WriteLine("");
                ChangeColor("방어가 상승하였습니다. !", 6);
                Console.WriteLine("방어력 : {0} -> {1}",player.Def,player.Def+1);
                player.Def += 1;
            }

        }
    }
}