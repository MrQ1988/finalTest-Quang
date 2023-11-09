using System;
using System.Collections;

interface IStudent
{
    int StudID { get; set; }
    string StudName { get; set; }
    string StudGender { get; set; }
    int StudAge { get; set; }
    string StudClass { get; set; }
    float StudAvgMark { get; }
    void Print();
}

class Student : IStudent
{
    public int StudID { get; set; }
    public string StudName { get; set; }
    public string StudGender { get; set; }
    public int StudAge { get; set; }
    public string StudClass { get; set; }
    public float StudAvgMark { get; private set; }
    private int[] MarkList = new int[3];

    public void Print()
    {
        Console.WriteLine("Student ID: " + StudID);
        Console.WriteLine("Student Name: " + StudName);
        Console.WriteLine("Gender: " + StudGender);
        Console.WriteLine("Age: " + StudAge);
        Console.WriteLine("Class: " + StudClass);
        Console.WriteLine("Average Mark: " + StudAvgMark);
    }

    public int this[int index]
    {
        get { return MarkList[index]; }
        set { MarkList[index] = value; }
    }

    public void CalAvg()
    {
        float sum = 0;
        foreach (int mark in MarkList)
        {
            sum += mark;
        }
        StudAvgMark = sum / MarkList.Length;
    }
}

class Program
{
    static Hashtable studentHashtable = new Hashtable();

    static void Main(string[] args)
    {
        int option;
        do
        {
            Console.WriteLine("Please select an option:");
            Console.WriteLine("1. Insert new student");
            Console.WriteLine("2. Display all student list");
            Console.WriteLine("3. Calculate average mark");
            Console.WriteLine("4. Search student");
            Console.WriteLine("5. Exit");
            Console.Write("Option: ");
            option = Convert.ToInt32(Console.ReadLine());

            switch (option)
            {
                case 1:
                    InsertNewStudent();
                    break;
                case 2:
                    DisplayAllStudents();
                    break;
                case 3:
                    CalculateAverageMark();
                    break;
                case 4:
                    SearchStudent();
                    break;
                case 5:
                    Console.WriteLine("Exiting program...");
                    break;
                default:
                    Console.WriteLine("Invalid option. Please try again.");
                    break;
            }

            Console.WriteLine();
        } while (option != 5);
    }

    static void InsertNewStudent()
    {
        Student student = new Student();

        Console.Write("Enter Student ID: ");
        student.StudID = Convert.ToInt32(Console.ReadLine());

        Console.Write("Enter Student Name: ");
        student.StudName = Console.ReadLine();

        Console.Write("Enter Gender: ");
        student.StudGender = Console.ReadLine();

        Console.Write("Enter Age: ");
        student.StudAge = Convert.ToInt32(Console.ReadLine());

        Console.Write("Enter Class: ");
        student.StudClass = Console.ReadLine();

        Console.WriteLine("Enter 3 marks:");
        for (int i = 0; i < 3; i++)
        {
            Console.Write("Mark " + (i + 1) + ": ");
            student[i] = Convert.ToInt32(Console.ReadLine());
        }

        student.CalAvg();

        studentHashtable.Add(student.StudID, student);
        Console.WriteLine("Student added successfully!");
    }

    static void DisplayAllStudents()
    {
        Console.WriteLine("Student List:");
        foreach (DictionaryEntry entry in studentHashtable)
        {
            IStudent student = (IStudent)entry.Value;
            student.Print();
            Console.WriteLine();
        }
    }

    static void CalculateAverageMark()
    {
        Console.WriteLine("Calculating average marks...");
        foreach (DictionaryEntry entry in studentHashtable)
        {
            Student student = (Student)entry.Value;
            student.CalAvg();
            student.Print();
            Console.WriteLine();
        }
    }

    static void SearchStudent()
    {
        Console.WriteLine("Please select an option to search:");
        Console.WriteLine("1. By ID");
        Console.WriteLine("2. By Name");
        Console.WriteLine("3. By Class");
        Console.Write("Option: ");
        int option = Convert.ToInt32(Console.ReadLine());

        switch (option)
        {
            case 1:
                SearchByID();
                break;
            case 2:
                SearchByName();
                break;
            case 3:
                SearchByClass();
                break;
            default:
                Console.WriteLine("Invalid option. Please try again.");
                break;
        }
    }

    static void SearchByID()
    {
        Console.Write("EnterID to search: ");
        int searchID = Convert.ToInt32(Console.ReadLine());

        ArrayList searchResults = new ArrayList();
        foreach (DictionaryEntry entry in studentHashtable)
        {
            Student student = (Student)entry.Value;
            if (student.StudID == searchID)
            {
                searchResults.Add(student);
            }
        }

        if (searchResults.Count > 0)
        {
            Console.WriteLine("Search results:");
            foreach (Student student in searchResults)
            {
                student.Print();
                Console.WriteLine();
            }
        }
        else
        {
            Console.WriteLine("No student found with the given ID.");
        }
    }

    static void SearchByName()
    {
        Console.Write("Enter name to search: ");
        string searchName = Console.ReadLine();

        ArrayList searchResults = new ArrayList();
        foreach (DictionaryEntry entry in studentHashtable)
        {
            Student student = (Student)entry.Value;
            if (student.StudName.Equals(searchName, StringComparison.OrdinalIgnoreCase))
            {
                searchResults.Add(student);
            }
        }

        if (searchResults.Count > 0)
        {
            Console.WriteLine("Search results:");
            foreach (Student student in searchResults)
            {
                student.Print();
                Console.WriteLine();
            }
        }
        else
        {
            Console.WriteLine("No student found with the given name.");
        }
    }

    static void SearchByClass()
    {
        Console.Write("Enter class to search: ");
        string searchClass = Console.ReadLine();

        ArrayList searchResults = new ArrayList();
        foreach (DictionaryEntry entry in studentHashtable)
        {
            Student student = (Student)entry.Value;
            if (student.StudClass.Equals(searchClass, StringComparison.OrdinalIgnoreCase))
            {
                searchResults.Add(student);
            }
        }

        if (searchResults.Count > 0)
        {
            Console.WriteLine("Search results:");
            foreach (Student student in searchResults)
            {
                student.Print();
                Console.WriteLine();
            }
        }
        else
        {
            Console.WriteLine("No student found in the given class.");
        }
    }
}