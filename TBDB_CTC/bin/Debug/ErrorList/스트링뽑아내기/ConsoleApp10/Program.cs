using System;
using System.IO;
using System.Text;


namespace ConsoleApp10
{
    class Program
    {
        static void Main(string[] args)
        {

            string text = System.IO.File.ReadAllText(@"C:\text.txt");


            string[] lines = text.Split(new[] { "\r\n", "\r", "\n" }, StringSplitOptions.None);

            string[] lines2 = new string[lines.Length];


            
            int x = 0;
            StringBuilder s = new StringBuilder();
            for(int i = 0; i < lines.Length; i++)
            {
                if(!(lines[i]== "\r\n")&&!(lines[i] == "\r")&& !(lines[i] == "\n") && !(lines[i] == " ") && !(lines[i] == ""))
                {
                    lines2[x] = lines[i];
                    x++;
                }
            }
            int g;



            string number;
            string message;
            string cause;
            string action;

            string temp="";

            string[] list = new string[2000];

            string xx = "";

            int gh = 0;

            

            for (int i = 0; i < lines2.Length; i++)
            {
                gh = 0;
                if (i + 5 < lines2.Length)
                {
                    if (Int32.TryParse(lines2[i], out g))
                    {//숫자이고
                        
                        if (lines2[i + 1].Contains("*"))//메시지시작라인
                        {
                            if (gh == 1) break;
                            number = g.ToString();
                            temp = temp + lines2[i + 1];
                            for (int z = i + 1; z < lines2.Length; z++)
                            {
                                if (gh == 1) break;
                                if (lines2[z].Contains("발생원인"))//끝라인
                                {if (gh == 1) break;
                                    message = temp;
                                    temp = "";
                                    for (int f = z + 1; f < lines2.Length; f++) //f == cause 시작라인
                                    {
                                        if (gh == 1) break;
                                        temp = temp + lines2[f];
                                        if (lines2[f].Contains("."))//case 끝라인
                                        {
                                            cause = temp;
                                            temp = "";
                                            for (int h = f + 2; h < lines2.Length; h++)//action 시작라인
                                            {
                                                temp = temp + lines2[h];
                                                if (lines2[h].Contains("."))//action끝라인
                                                {
                                                    i = h;
                                                    action = temp;
                                                    xx = xx+number + "," + message + "," + cause + "," + action;
                                                    temp = "";
                                                    gh = 1;
                                                    break;
                                                }
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            }

            Console.WriteLine(xx);
           
            System.Console.ReadKey();
        }
    }
}
