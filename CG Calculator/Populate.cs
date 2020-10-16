using System;
using System.IO;
using System.Collections.Generic;

namespace CG_Calculator
{
    class Populate
    {
        public static List<StudentResult> PopulateStudentResult(string path)
        {
            if (!File.Exists(path))
                return new List<StudentResult>();

            List<StudentResult> studentResult = new List<StudentResult>();
            using (var contents = new StreamReader(path))
            {
                string line;
                while ((line = contents.ReadLine()) != null)
                {
                    string[] resultData = line.Split(", ");
                    int roll = Int32.Parse(resultData[0]);
                    string subject = resultData[1];
                    double gp = Double.Parse(resultData[2]);
                    studentResult.Add(new StudentResult(roll, subject, gp));
                }
            }
            return studentResult;
        }

        public static List<Student> PopulateStudentInfo(string path)
        {
            if (!File.Exists(path))
                return new List<Student>();
            List<Student> studentInfo = new List<Student>();
            using (var contents = new StreamReader(path))
            {
                string line;
                while ((line = contents.ReadLine()) != null)
                {
                    string[] studentData = line.Split(", ");
                    string studentName = studentData[0];
                    int roll = Int32.Parse(studentData[1]);
                    studentInfo.Add(new Student(studentName, roll));
                }
            }
            return studentInfo;
        }
    }
}
