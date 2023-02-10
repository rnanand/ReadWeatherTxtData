using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Weather
{
    class Program
    {
        static readonly string filePath = @"F:\Weather\weather.txt";

        static void Main(string[] args)
        {
            Console.WriteLine(ConvertToDataTable(filePath));
            Console.ReadLine();
        }

        public static string ConvertToDataTable(string filePath)
        {
            DataTable tbl = new DataTable();

            tbl.Columns.Add(new DataColumn("Dy"));
            tbl.Columns.Add(new DataColumn("MxT"));
            tbl.Columns.Add(new DataColumn("MnT"));

            string[] lines = System.IO.File.ReadAllLines(filePath);

            for (int j = 2; j < lines.Length; j++)
            {
                if (!string.IsNullOrEmpty(lines[j]))
                {
                    var cols = lines[j].Split(' ');
                    cols = cols.Where(x => !string.IsNullOrEmpty(x)).ToArray();

                    DataRow dr = tbl.NewRow();
                    for (int i = 0; i < cols.Length; i++)
                    {
                        dr[i] = cols[i].Contains("*") ? cols[i].Replace("*", "") : cols[i];
                        if (i > 1)
                            break;
                    }
                    tbl.Rows.Add(dr);
                }
            }

            return "Dy : " + tbl.Select("Mnt=min(Mnt)")[0][0] + " \nMnt : " + tbl.Select("Mnt=min(Mnt)")[0][2];
        }
    }
}
