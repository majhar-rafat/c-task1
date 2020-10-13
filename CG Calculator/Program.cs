using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Configuration.FileExtensions;
using Microsoft.Extensions.Configuration.Json;
using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;

namespace CG_Calculator
{
    class Program
    {
        static List<StudentResult> PopulateStudentResult(string path)
        {
            if (!File.Exists(path))
                return new List<StudentResult>();
            var contents = new StreamReader(path);
            List<StudentResult> studentResult = new List<StudentResult>();
            string line;
            while ((line = contents.ReadLine()) != null)
            {
                string[] resultData = line.Split(", ");
                int roll = Int32.Parse(resultData[0]);
                string subject = resultData[1];
                double gp = Double.Parse(resultData[2]);
                studentResult.Add(new StudentResult(roll, subject, gp));
            }
            return studentResult;
        }

        static List<Student> PopulateStudentInfo(string path)
        {
            if (!File.Exists(path))
                return new List<Student>();
            var contents= new StreamReader(path);
            List<Student> studentInfo = new List<Student>();
            string line;
            while((line=contents.ReadLine())!=null)
            {
                string[] studentData = line.Split(", ");
                string studentName = studentData[0];
                int roll = Int32.Parse(studentData[1]);
                studentInfo.Add(new Student(studentName, roll));
            }
            return studentInfo;
        }
        static void Main(string[] args)
        {
            IConfiguration config = new ConfigurationBuilder()
            .AddJsonFile("appsettings.json", true, true)
            .Build();

            string pathOfStudentResult = config["pathOfStudentResult"];
            string pathOfStudentInfo = config["pathOfStudentInfo"];

            List<Student> studentInfo = PopulateStudentInfo(pathOfStudentInfo);
            List<StudentResult> studentResult = PopulateStudentResult(pathOfStudentResult);


            var studentGroupedBy = studentResult.GroupBy(info => info.Roll);
            foreach (var group in studentGroupedBy)
            {
                double cg = 0, subjectCount = 0;
                foreach (var info in group)
                {
                    cg += info.Gp;
                    subjectCount++;
                }
                cg /= subjectCount;
                foreach (var student in studentInfo)
                {
                    if (student.Roll == group.Key)
                    {
                        student.Cg = cg;
                    }
                }

            }
            var sortedStudentResult = studentInfo.OrderByDescending(info => info.Cg).ThenBy(info => info.Roll).ThenBy(info => info.Name);
            string outputFileName = "finalresult.txt";
            try
            {
                using (StreamWriter fs = File.CreateText(outputFileName))
                {
                    foreach (var info in sortedStudentResult)
                    {
                        fs.WriteLine($"{info.Name}, {info.Roll}, {Math.Round(info.Cg,2)}");
                    }
                }
                Console.WriteLine("-------CG Calculation Completed-------");
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }
        }
    }
    class Student
    {
        public string Name { get; set; }
        public int Roll { get; set; }
        public double Cg { get; set; }
        public Student(string name, int roll)
        {
            this.Name = name;
            this.Roll = roll;
        }
    }

    class StudentResult
    {
        public string Subject { get; set; }
        public int Roll { get; set; }
        public double Gp { get; set; }
        public StudentResult(int roll, string subject, double gp)
        {
            this.Subject = subject;
            this.Roll = roll;
            this.Gp = gp;
        }
    }
}
