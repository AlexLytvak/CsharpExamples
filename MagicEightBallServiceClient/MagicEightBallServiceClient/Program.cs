using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MagicEightBallServiceClient.ServiceReference1;

namespace MagicEightBallServiceClient
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("****Ask the Magic 8Ball*******\n");
            using (EigthBallClient ball = new EigthBallClient("NetTcpBinding_IEigthBall"))
            {
                Console.Write("Your question: ");
                string question = Console.ReadLine();
                string answer = ball.ObtainAnswerToQuestion(question);
                Console.WriteLine("8-Ball says:{0} ", answer);
            }
            Console.ReadLine();
        }
    }
}
