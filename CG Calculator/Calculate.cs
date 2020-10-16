using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
namespace CG_Calculator
{
    class Calculate
    {
        public static void CG(IEnumerable<IGrouping<int, StudentResult> > studentGroupedBy, List<Student> studentInfo)
        {
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
                        fs.WriteLine($"{info.Name}, {info.Roll}, {Math.Round(info.Cg, 2)}");
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
}
