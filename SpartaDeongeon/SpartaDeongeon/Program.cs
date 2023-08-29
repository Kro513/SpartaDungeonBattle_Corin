namespace SpartaDeongeon
{
    // 직업 열거형
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

        public Character(string name, CharacterClass characterClass)
        {
            Name = name;
            Class = characterClass;
            Level = 1;
        }

        public void DisplayCharacterInfo()
        {
            Console.WriteLine($"Name: {Name}");
            Console.WriteLine($"Class: {Class}");
            Console.WriteLine($"Level: {Level}");
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("캐릭터 생성에 오신걸 환영합니다!");

            // 캐릭터 이름 입력 받기
            Console.Write("캐릭터 이름을 입력해주세요: ");
            string name = Console.ReadLine();

            // 직업 선택
            Console.WriteLine("직업을 선택하세요:");
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

            // 캐릭터 생성
            Character newCharacter = new Character(name, selectedClass);

            Console.WriteLine("\n캐릭터가 생성되었습니다!");
            newCharacter.DisplayCharacterInfo();
        }
    }
}