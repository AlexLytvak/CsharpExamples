using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ServiceModel;

namespace MagicEigthBallServiceLib
{
    public class MagicEigthBallService:IEigthBall
    {
        public  MagicEigthBallService()
        {
            Console.WriteLine("The 8_Ball await your question....");
        }
        public string ObtainAnswerToQuestion(string userQuestion)
        {
            string[] answers = { "Future uncertain", "Yes", "No", "Hazy", "Ask again later", "Definitely" };
            Random r = new Random();
            return answers[r.Next(answers.Length)];
        }

    }
}
