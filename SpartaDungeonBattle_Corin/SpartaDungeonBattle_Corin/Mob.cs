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
        public Mob(string name, int hp, int atk)
        {
            Name = name;
            Hp = hp;
            Atk = atk;
            DeadCount = 0;
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
            Warrior warrior = new Warrior();
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

                    Mob mob = new Mob(mobname, randomhp, randomatk);

                    mobs.Add(mob);
                }
                Console.WriteLine("=============================");
                Console.WriteLine("Battle!");
                Console.WriteLine("");
                Console.WriteLine("");

                foreach (var text in mobs) //몬스터 출력
                {
                    Console.WriteLine($"{mobs.IndexOf(text) + 1}.{text.Name} \tHp: {text.Hp}");
                }
                Console.WriteLine("");
                Console.WriteLine("");
                Console.WriteLine("-----------------------------");
                Console.WriteLine("[내정보]");
                Console.WriteLine("Lv.1 Chad 전사");
                Console.WriteLine("Hp 100/100");
                Console.WriteLine("-----------------------------");
                Console.WriteLine("0. 취소");
                Console.WriteLine("");
                Console.WriteLine("대상을 선택해주세요");
                Console.Write(">>");

                while (warrior.IsDead == false && !mobs.All(mob => mob.IsDead))
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
                            enemy.OnHeatAttack += warrior.Takedamage;
                            enemy.Attack(warrior.Atk, warrior.Name);
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

                            enemy.OnAttack -= selectedMob.Takedamage;
                            enemy.OnHeatAttack -= warrior.Takedamage;

                        }
                    }
                    else
                    {
                        Console.WriteLine("올바른 값을 입력해주세요");
                    }


                }
                if (warrior.IsDead == false)
                {
                    BattleEnd();
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
                Console.WriteLine("{0}", warrior.Name);
                Console.WriteLine("남은 Hp : {0}", warrior.Hp);
                Console.WriteLine("");
                Console.WriteLine("0. 다음");
                Console.WriteLine("");
                Console.WriteLine(">>");
                Console.ReadKey();
            }
            public void BattleReword()
            {
                Console.Clear();
                Console.WriteLine("보상을 선택해주세요");
                Console.ReadLine();
            }


        }
    }
}
