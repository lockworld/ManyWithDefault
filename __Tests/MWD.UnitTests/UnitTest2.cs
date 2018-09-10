using System;
using System.Text;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Data.Entity;
using MWD.ArvixeSQL;
using MWD.Core.Entities;
using System.Linq;

namespace MWD.UnitTests
{
    /// <summary>
    /// Summary description for UnitTest2
    /// </summary>
    [TestClass]
    public class UnitTest2
    {
        public UnitTest2()
        {
            //
            // TODO: Add constructor logic here
            //
        }

        private TestContext testContextInstance;

        /// <summary>
        ///Gets or sets the test context which provides
        ///information about and functionality for the current test run.
        ///</summary>
        public TestContext TestContext
        {
            get
            {
                return testContextInstance;
            }
            set
            {
                testContextInstance = value;
            }
        }

        #region Additional test attributes
        //
        // You can use the following additional attributes as you write your tests:
        //
        // Use ClassInitialize to run code before running the first test in the class
        // [ClassInitialize()]
        // public static void MyClassInitialize(TestContext testContext) { }
        //
        // Use ClassCleanup to run code after all tests in a class have run
        // [ClassCleanup()]
        // public static void MyClassCleanup() { }
        //
        // Use TestInitialize to run code before running each test 
        // [TestInitialize()]
        // public void MyTestInitialize() { }
        //
        // Use TestCleanup to run code after each test has run
        // [TestCleanup()]
        // public void MyTestCleanup() { }
        //
        #endregion

        [TestMethod]
        public void TestMethod1()
        {
            //
            // TODO: Add test logic here
            //
            using (ArvixeDB context = new ArvixeDB())
            {
                var newentities = context.Set<NewEntityPrime>();
                var newentitiesalt = context.Set<NewEntityPrimeAlternate>();
                var newsubs = context.Set<NewEntitySub>();

                var newprime = newentities.FirstOrDefault(e => e.Name.ToLower() == "prime1");
                if (newprime == null)
                {
                    newprime = new NewEntityPrime()
                    {
                        Name = "Prime1"
                    };
                    context.Set<NewEntityPrime>().Add(newprime);
                    context.SaveChanges();
                }

                var newalt = newentitiesalt.FirstOrDefault(e => e.Name.ToLower() == "alt1");
                if (newalt==null)
                {
                    newalt = new NewEntityPrimeAlternate()
                    {
                        Name = "Alt1"
                    };
                    newentitiesalt.Add(newalt);
                    context.SaveChanges();
                }

                var newsub1 = newsubs.FirstOrDefault(s => s.RelatedInfo.ToLower() == "i'm related to prime1");
                if (newsub1==null)
                {
                    newsub1 = new NewEntitySub()
                    {
                        ForeignKey_ID=newprime.ID,
                        RelatedInfo = "I'm related to Prime1"
                    };
                    newsubs.Add(newsub1);
                    context.SaveChanges();
                }

                var newsub2 = newsubs.FirstOrDefault(s => s.RelatedInfo.ToLower() == "i'm also related to prime1");
                if (newsub2 == null)
                {
                    newsub2 = new NewEntitySub()
                    {
                        ForeignKey_ID = newprime.ID,
                        RelatedInfo = "I'm also related to Prime1"
                    };
                    newsubs.Add(newsub2);
                    context.SaveChanges();
                }

                var newsub3 = newsubs.FirstOrDefault(s => s.RelatedInfo.ToLower() == "i'm related to alt1");
                if (newsub3 == null)
                {
                    newsub3 = new NewEntitySub()
                    {
                        ForeignKey_ID=newalt.ID,
                        RelatedInfo = "I'm related to Alt1"
                    };
                    newsubs.Add(newsub3);
                    context.SaveChanges();
                }





            }
        }
    }
}
