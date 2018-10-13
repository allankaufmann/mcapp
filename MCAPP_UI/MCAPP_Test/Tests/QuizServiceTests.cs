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
            IFragenRepository repoFragen = new DummyFragenRepository();
            IQuizRepository repoQuiz = new DummyQuizRepository();
            quizService = new QuizService(repoFragen, repoQuiz);
        }

        [Test]
        public async Task increaseQuizID()
        {
            Quiz q1 = await quizService.CreateQuiz();
            Quiz q2 = await quizService.CreateQuiz();
            Assert.Greater(q2.quizID, q1.quizID);
        }

        [Test] 
        public async Task nichtBeantworteteFragen()
        {
            /*
             * Im Dummyrepository wurden 1 und 7 nicht
             * beantwortet!
             */

            Boolean nichtBeantwortet = await quizService.FrageNochNichtRichtigBeantwortet(2);
            Assert.IsFalse(nichtBeantwortet);

            nichtBeantwortet = await quizService.FrageNochNichtRichtigBeantwortet(1);
            Assert.IsTrue(nichtBeantwortet);
        }





    }
}
