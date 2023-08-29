using System;

namespace SpartaDeongeon
{
    enum CharacterClass
    {
        Warrior,
        Mage,
        Archer
    }

    class Character
    {
        public string Name { get; set; }
        public CharacterClass Class { get; set; }
        public int Level { get; set; }
        public int Str { get; set; }
        public int Int { get; set; }
        public int Dex { get; set; }

        private readonly Random random = new Random();

        public Character(string name, CharacterClass characterClass)
        {
            Name = name;
            Class = characterClass;
            Level = 1;
            InitializeStats();
        }

        private void InitializeStats()
        {
            switch (Class)
            {
                case CharacterClass.Warrior:
                    Str = random.Next(6, 11);
                    Int = random.Next(1, 11);
                    Dex = random.Next(1, 11);
                    break;
                case CharacterClass.Mage:
                    Str = random.Next(1, 11);
                    Int = random.Next(6, 11);
                    Dex = random.Next(1, 11);
                    break;
                case CharacterClass.Archer:
                    Str = random.Next(1, 11);
                    Int = random.Next(1, 11);
                    Dex = random.Next(6, 11);
                    break;
            }
        }

        public void DisplayCharacterInfo()
        {
            Console.WriteLine($"Name: {Name}");
            Console.WriteLine($"Class: {Class}");
            Console.WriteLine($"Level: {Level}");
            Console.WriteLine($"Str: {Str}");
            Console.WriteLine($"Int: {Int}");
            Console.WriteLine($"Dex: {Dex}");
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("캐릭터 생성에 오신걸 환영합니다!");

            Console.Write("닉네임을 정해주세요: ");
            string name = Console.ReadLine();


            Console.WriteLine("Choose a class:");
            Console.WriteLine("1. 전사");
            Console.WriteLine("2. 마법사");
            Console.WriteLine("3. 궁수");

            CharacterClass selectedClass;
            int choice = int.Parse(Console.ReadLine());

            switch (choice)
            {
                case 1:
                    selectedClass = CharacterClass.Warrior;
                    break;
                case 2:
                    selectedClass = CharacterClass.Mage;
                    break;
                case 3:
                    selectedClass = CharacterClass.Archer;
                    break;
                default:
                    Console.WriteLine("잘못된 선택입니다. 기본값을 전사로 설정합니다.");
                    selectedClass = CharacterClass.Warrior;
                    break;
            }


            Character newCharacter = new Character(name, selectedClass);

            Console.WriteLine("\n캐릭터 생성이 완료되었습니다!");
            newCharacter.DisplayCharacterInfo();
        }
    }
}