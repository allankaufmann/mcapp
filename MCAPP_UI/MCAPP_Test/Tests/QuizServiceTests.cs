using MCAPP_Project.Core.Models;
using MCAPP_Project.Core.Repositories;
using MCAPP_Project.Core.Services;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MCAPP_Test.Tests
{
    [TestFixture]
    public class QuizServiceTests
    {
        QuizService quizService;

        [SetUp]
        public void SetUp()
        {
            IFragenRepository repo = new DummyFragenRepository();
            quizService = new QuizService(repo);
        }

        [Test]
        public void increaseQuizID()
        {
            Quiz q1 = quizService.GetNewQuiz();
            Quiz q2 = quizService.GetNewQuiz();
            Assert.Greater(q2.quizID, q1.quizID);

        }


    }
}
