using levelup;
using System.Numerics;

namespace levelup
{
    class Character
    {
        public string Name { get; set; }
        public string Class { get; set; }
        public int Level { get; set; }
        public int Experience { get; set; }

        public Character(string name, string characterClass)
        {
            Name = name;
            Class = characterClass;
            Level = 1;
            Experience = 1;
        }

        public virtual void LevelUp()
        {
            Experience = Experience % (Level * 100);
            Level++;
            Console.WriteLine($"{Name}이 {Level} 레벨로 레벨업했습니다!");
        }
    }
}

class Warrior : Character
{
    public int MaxHP { get; set; }
    public int CurrentHP { get; set; }
    public Warrior(string name) : base(name, "전사")
    {
        MaxHP = 100;
        CurrentHP = MaxHP;
    }

    public override void LevelUp()
    {
        base.LevelUp();
        MaxHP += 20;
        CurrentHP = MaxHP;
        Console.WriteLine($"{Name}의 체력이 올랐습니다!. 현재 HP: {CurrentHP}/{MaxHP}");
    }
}

class Archer : Character
{
    public int Attack { get; set; }
    public Archer(string name) : base(name, "궁수")
    {
        Attack = 30;
    }

    public override void LevelUp()
    {
        base.LevelUp();
        Attack += 10;
        Console.WriteLine($"{Name}의 공격력이 올랐습니다!. 현재 공격력: {Attack}");
    }
}

class Mage : Character
{
    public int Defense { get; set;}
    public Mage(string name) : base(name, "마법사")
    {
        Defense = 10;
    }

    public override void LevelUp()
    {
        base.LevelUp();
        Defense += 5;
        Console.WriteLine($"{Name}의 방어력이 올랐습니다!. 현재 방어력: {Defense}");
    }
}

class Program
{
    public static Character player;

    public delegate void Damage(int damage, string name);
    static void Main(string[] args)
    {
        Console.WriteLine("캐릭터의 이름을 입력하세요:");
        string characterName = Console.ReadLine();

        Console.WriteLine("캐릭터의 직업을 선택하세요 (전사, 궁수, 마법사):");
        string characterClass = Console.ReadLine().ToLower();

        Character character;

        switch (characterClass)
        {
            case "전사":
                character = new Warrior(characterName);
                break;
            case "궁수":
                character = new Archer(characterName);
                break;
            case "마법사":
                character = new Mage(characterName);
                break;
            default:
                Console.WriteLine("잘못된 직업을 선택했습니다.");
                return;
        }

        while (true)
        {
            Console.WriteLine($"현재 {character.Name}의 레벨: {character.Level}");
            Console.WriteLine($"현재 {character.Name}의 경험치: {character.Experience}");

            int experienceToLevelUp = character.Level * 100 - character.Experience;
            Console.WriteLine($"다음 레벨까지 필요한 경험치: {experienceToLevelUp}");

            Console.WriteLine("전투를 시뮬레이션합니다. 전투에서 승리하려면 아무 키나 누르세요...");
            Console.ReadKey();

            
            int battleExperience = new Random().Next(50, 101);
            Console.WriteLine($"{character.Name}이(가) 전투에서 승리하여 {battleExperience}의 경험치를 획득했습니다!");
            character.Experience += battleExperience;

            while (character.Experience >= character.Level * 100)
            {
                character.LevelUp();
            }

        }

    }
}




