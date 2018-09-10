using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MWD.ArvixeSQL.Repositories;
using MWD.Core.Entities;
using MWD.Core.Repositories;

namespace MWD.UnitTests
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void AddPersonAndEmail()
        {
            using (UnitOfWork uow = new UnitOfWork())
            {
                Assert.IsNotNull(uow.People);

                var bob = new Person()
                {
                    FirstName = "Bob",
                    LastName = "Smith"
                };

                var bobpeople = uow.People.List(p => p.FirstName == "Bob" && p.LastName == "Smith");
                if (bobpeople.Count < 1)
                {
                    uow.People.Add(bob);
                    uow.Save();
                    Console.WriteLine("Added new bobperson");
                    bobpeople = uow.People.List(p => p.FirstName == "Bob" && p.LastName == "Smith");
                }

                Assert.AreEqual(1, bobpeople.Count);

                bob = bobpeople.FirstOrDefault();
                Assert.AreNotEqual(Guid.Empty, bob.ID);

                var bobmail = uow.Email.GetEmailListByEntity(bob);
                if (bobmail != null)
                {
                    if (!bobmail.Select(e => e.EmailAddress.ToLower()).Contains("test@test.com"))
                    {
                        bobmail.Add(new Email()
                        {
                            EmailAddress = "test@test.com",
                            ForeignKey = bob.ID,
                            IsDefault = true
                        });
                    }

                    if (!bobmail.Select(e => e.EmailAddress.ToLower()).Contains("bob@test.com"))
                    {
                        bobmail.Add(new Email()
                        {
                            EmailAddress = "bob@test.com",
                            ForeignKey = bob.ID,
                            IsDefault = false
                        });
                    }

                    uow.Email.SetEmailListForEntity(bob, bobmail);
                }
                else
                {
                    bobmail = new List<Email>();
                }

                foreach (var email in bobmail)
                {
                    Console.WriteLine("\r\nBob's email address is: " + email.EmailAddress + " [" + email.IsDefault + "]");
                }
                Console.WriteLine("Related Principal " + uow.Email.GetDefaultEmailByEntity(bob)?.EmailAddress);


                //Assert.AreEqual("test@test.com", uow.Email.GetDefaultEmailByEntity(bob)?.EmailAddress?.ToLower());

                var newdefault = bobmail.FirstOrDefault(e => e.IsDefault == false);

                Console.WriteLine("Changing default email address from " + uow.Email.GetDefaultEmailByEntity(bob)?.EmailAddress + " to " + newdefault.EmailAddress);

                uow.Email.SetDefaultEmailByEntity(bob, newdefault);
                uow.Save();

                //Assert.AreEqual("bob@test.com", uow.Email.GetDefaultEmailByEntity(bob).EmailAddress);

                newdefault = bobmail.FirstOrDefault(e => e.IsDefault == false);

                Console.WriteLine("Changing default email address from " + uow.Email.GetDefaultEmailByEntity(bob)?.EmailAddress + " to " + newdefault.EmailAddress);

                uow.Email.SetDefaultEmailByEntity(bob, newdefault);
                uow.Save();

                //Assert.AreEqual("test@test.com", uow.Email.GetDefaultEmailByEntity(bob).EmailAddress);




            }
        }
    }
}
