using System;

namespace Day14
{
    public enum CreatureType
    {
        None = 0,
        Player,
        Monster
    }

    class Creature
    {
        private CreatureType type;

        protected Creature(CreatureType type)
        {
            this.type = type;
        }

        public void SetInfo(int hp, int attack)
        {
            this.hp = hp;
            this.attack = attack;
        }

        public int GetHP() { return hp; }
        public int GetAttack() { return attack; }

        protected int hp = 0;
        protected int attack = 0;
        
        public bool IsDead() { return hp <= 0; }
        public void OnDamaged(int damage)
        {
            hp -= damage;
            if (hp < 0)
            {
                hp = 0;
            }
        }
    }

    public enum PlayerType
    {
        None = 0,
        Knight,
        Archer,
        Mage
    }

    class Player : Creature
    {
        public PlayerType type = PlayerType.None;

        protected Player(PlayerType type) : base(CreatureType.Player)
        {
            this.type = type;
        }
    }

    class Knight : Player
    {
        public Knight() : base(PlayerType.Knight)
        {
            SetInfo(100, 10);
        }
    }

    class Archer : Player
    {
        public Archer() : base(PlayerType.Archer)
        {
            SetInfo(75, 12);
        }
    }

    class Mage : Player
    {
        public Mage() : base(PlayerType.Mage)
        {
            SetInfo(60, 25);
        }
    }
    
    public enum MonsterType
    {
        None = 0,
        Slime,
        Orc,
        Skeleton
    }

    class Monster : Creature
    {
        public MonsterType type = MonsterType.None;
        protected Monster(MonsterType type) : base(CreatureType.Monster)
        {
            this.type = type;
        }
    }

    class Slime : Monster
    {
        public Slime() : base(MonsterType.Slime)
        {
            SetInfo(10, 1); 
        }
    }

    class Orc : Monster
    {
        public Orc() : base(MonsterType.Orc)
        {
            SetInfo(20, 2);
        }
    }

    class Skeleton : Monster
    {
        public Skeleton() : base(MonsterType.Skeleton)
        {
            SetInfo(15, 5);
        }
    }
    
    public enum GameMode
    {
        None = 0,
        Lobby,
        Town,
        Field
    }

    class GameManager
    {
        public GameMode mode = GameMode.Lobby;
        public Player player = null;
        public Monster monster = null;
        private Random rand = new Random();

        public void Process()
        {
            switch (mode)
            {
                case GameMode.Lobby:
                    ProcessLobby();
                    break;
                case GameMode.Town:
                    ProcessTown();
                    break;
                case GameMode.Field:
                    ProcessField();
                    break;
            }
        }

        public void ProcessLobby()
        {
            Console.WriteLine("직업을 선택하세요");
            Console.WriteLine("[1] 기사");
            Console.WriteLine("[2] 궁수");
            Console.WriteLine("[3] 마법사");

            string strInput = Console.ReadLine();

            
            switch (strInput)
            {
                case "1":
                    player = new Knight();
                    mode = GameMode.Town;
                    break;
                case "2":
                    player = new Archer();
                    mode = GameMode.Town;
                    break;
                case "3":
                    player = new Mage();
                    mode = GameMode.Town;
                    break;
            }
        }

        public void ProcessTown()
        {
            Console.WriteLine("마을에 입장 했습니다.");
            Console.WriteLine("[1] 필드로 간다.");
            Console.WriteLine("[2] 로비로 돌아간다.");

            string strInput = Console.ReadLine();

            switch (strInput)
            {
                case "1":
                    mode = GameMode.Field;
                    break;
                case "2":
                    mode = GameMode.Lobby;
                    break;
            }
        }
        
        public void ProcessField()
        {
            Console.WriteLine("필드에 입장 했습니다.");
            Console.WriteLine("[1] 전투 모드 돌입");
            Console.WriteLine("[2] 일정 확률로 마을로 도망");

            string strInput = Console.ReadLine();

            switch (strInput)
            {
                case "1":
                    CreateMonster();
                    ProcessBattle();
                    break;
                case "2":
                    ProcessEscape();
                    break;
            }

        }

        public void CreateMonster()
        {
            int randValue = rand.Next(0, 3);
            switch (randValue)
            {
                case 0:
                    monster = new Slime();
                    Console.WriteLine("슬라임이 나타났습니다!");
                    break;
                case 1:
                    monster = new Orc();
                    Console.WriteLine("오크가 나타났습니다!");
                    break;
                case 2:
                    monster = new Skeleton();
                    Console.WriteLine("스켈레톤이 나타났습니다!");
                    break;
            }
        }

        public void ProcessEscape()
        {
            int randValue = rand.Next(0, 101);
            if (randValue <= 55)
            {
                mode = GameMode.Town;
            }
            else
            {
                mode = GameMode.Field;
            }
        }

        public void ProcessBattle()
        {
            while (true)
            {
                int power = player.GetAttack();
                monster.OnDamaged(power);
                if (monster.IsDead())
                {
                    Console.WriteLine("승리!!");
                    Console.WriteLine($"남은 체력 : {player.GetHP()}");
                    break;
                }
                power = monster.GetAttack();
                player.OnDamaged(power);
                if (player.IsDead())
                {
                    Console.WriteLine("패배!!");
                    mode = GameMode.Lobby;
                    break;
                }
            }
        }
    }

    

    class Program
    {
        static void Main(string[] args)
        {
            GameManager gameManager = new GameManager();

            while (true)
            {
                gameManager.Process();
            }
        }
    }
}
