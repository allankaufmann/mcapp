using MCAPP_Project.Core.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace MCAPP_Project.Core.Services
{
    interface IQuizService
    {
        Quiz GetNewQuiz();

        Quiz CreateAuswertung(Thema theme, Quiz quiz);

    }
}
