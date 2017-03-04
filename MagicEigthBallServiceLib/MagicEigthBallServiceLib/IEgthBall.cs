using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ServiceModel;

namespace MagicEigthBallServiceLib
{
    [ServiceContract(Namespace ="http://MyCompany.com")]
    public  interface IEigthBall
    {
        [OperationContract]
        string ObtainAnswerToQuestion(string userQuestion);
    }
}
