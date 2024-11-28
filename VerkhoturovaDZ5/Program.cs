using System;
using System.Collections.Generic;

class Babulya
{
    public string Name { get; set; }
    public int Age { get; set; }
    public List<string> Diseases { get; set; }

    public Babulya(string name, int age, List<string> diseases)
    {
        Name = name;
        Age = age;
        Diseases = diseases;
    }
}

class Bolnitsa
{
    public string Name { get; set; }
    public List<string> TreatableDiseases { get; set; }
    public int Capacity { get; set; }
    public int CurrentPatients { get; set; }

    public Bolnitsa(string name, List<string> treatableDiseases, int capacity)
    {
        Name = name;
        TreatableDiseases = treatableDiseases;
        Capacity = capacity;
        CurrentPatients = 0;
    }

    public double OccupancyRate()
    {
        return (double)CurrentPatients / Capacity * 100;
    }

    public bool CanTreat(Babulya babulya)
    {
        int treatableCount = 0;
        foreach (var disease in babulya.Diseases)
        {
            if (TreatableDiseases.Contains(disease))
            {
                treatableCount++;
            }
        }
        return ((double)treatableCount / babulya.Diseases.Count) > 0.5;
    }
}

class Program
{
    static void Main(string[] args)
    {
        Console.WriteLine("Задание 5");
        Queue<Babulya> babushkiQueue = new Queue<Babulya>();
        Stack<Bolnitsa> bolnitsyStack = new Stack<Bolnitsa>();

        bolnitsyStack.Push(new Bolnitsa("'Городская Больница'", new List<string> { "грипп", "пневмония" }, 3));
        bolnitsyStack.Push(new Bolnitsa("'Центральная Клиника'", new List<string> { "артрит", "остеопороз" }, 2));
        bolnitsyStack.Push(new Bolnitsa("'Сельская Больница'", new List<string> { "боли в спине", "грипп" }, 1));

        for (int i = 0; i < 3; i++)
        {
            Console.Write("Введите имя бабушки(всего 3): ");
            string name = Console.ReadLine();

            Console.Write("Введите возраст бабушки: ");
            int age = int.Parse(Console.ReadLine());

            Console.Write("Введите болезни (через запятую, без пробелов, с маленькой буквы) или 'нет', если у бабушки нет болезней: ");
            string diseasesInput = Console.ReadLine();

            List<string> diseases = new List<string>();

            if (diseasesInput.ToLower() != "нет")
            {
                diseases.AddRange(diseasesInput.Split(','));
            }

            babushkiQueue.Enqueue(new Babulya(name, age, diseases));
        }

        while (babushkiQueue.Count > 0)
        {
            Babulya currentBabulya = babushkiQueue.Dequeue();
            bool admitted = false;

            foreach (Bolnitsa bolnitsa in bolnitsyStack)
            {
                if (bolnitsa.CurrentPatients < bolnitsa.Capacity && bolnitsa.CanTreat(currentBabulya))
                {
                    bolnitsa.CurrentPatients++;
                    admitted = true;
                    Console.WriteLine($"{currentBabulya.Name} поступила в больницу {bolnitsa.Name}.");
                    break;
                }
            }

            if (!admitted && currentBabulya.Diseases.Count == 0)
            {
                foreach (Bolnitsa bolnitsa in bolnitsyStack)
                {
                    if (bolnitsa.CurrentPatients < bolnitsa.Capacity)
                    {
                        bolnitsa.CurrentPatients++;
                        Console.WriteLine($"{currentBabulya.Name} просто спросила и была помещена в больницу {bolnitsa.Name}.");
                        admitted = true;
                        break;
                    }
                }
            }

            if (!admitted)
            {
                Console.WriteLine($"{currentBabulya.Name} осталась на улице плакать.");
            }
        }

        Console.WriteLine("Информация о больницах:");
        foreach (Bolnitsa bolnitsa in bolnitsyStack)
        {
            Console.WriteLine($"{bolnitsa.Name} | Заполненность: {bolnitsa.OccupancyRate():F2}% | Текущие пациенты: {bolnitsa.CurrentPatients}");
        }
    }
}