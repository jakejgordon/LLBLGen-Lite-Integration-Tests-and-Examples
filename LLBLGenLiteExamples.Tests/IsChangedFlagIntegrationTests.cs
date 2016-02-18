using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using LLBLGenLiteExamples.DatabaseSpecific;
using LLBLGenLiteExamples.EntityClasses;
using LLBLGenLiteExamples.Linq;
using NUnit.Framework;

namespace LLBLGenLiteExamples.Tests
{
    [TestFixture]
    public class IsChangedFlagIntegrationTests
    {
        [Test]
        public void ItDoesntChangeTheFieldOrMarkTheEntityAsDirtyIfItIsFromTheDBAndAFieldIsAssignedToTheSameValue()
        {
            using (DataAccessAdapter adapter = new DataAccessAdapter())
            {
                LinqMetaData metaData = new LinqMetaData(adapter);

                PersonEntity person = (from p in metaData.Person
                                       where p.Name == IntegrationTests.PERSON_JANE
                                       select p).First();

                Assert.That(person.IsNew, Is.False);
                person.Name = IntegrationTests.PERSON_JANE;
                Assert.That(person.Fields.First(p => p.Alias == "Name").IsChanged, Is.False);
                Assert.That(person.IsDirty, Is.False);
            }
        }

        [Test]
        public void SettingAPropertyOnANewEntityWillMarkThatPropertyAsChangedEvenIfTheEntityIsntDirty()
        {
            PersonEntity person = new PersonEntity
            {
                Name = IntegrationTests.PERSON_JANE
            };
            var nameField = person.Fields.First(p => p.Alias == "Name");
            Assert.That(nameField.IsChanged, Is.True);
            Assert.That(person.IsNew, Is.True);

            //attempt to reset the field and entity so it doesn't look like anything changed
            nameField.IsChanged = false;
            person.IsDirty = false;
            Assert.That(nameField.IsChanged, Is.False);
            person.Name = IntegrationTests.PERSON_JANE;
            Assert.That(nameField.IsChanged, Is.True);
            Assert.That(person.IsDirty, Is.True);
        }

        [Test]
        public void SettingAPropertyOnANewEntityWillNOTMarkThatPropertyAsChangedIfTheEntityIsNotNew()
        {
            PersonEntity person = new PersonEntity
            {
                Name = IntegrationTests.PERSON_JANE
            };
            var nameField = person.Fields.First(p => p.Alias == "Name");
            Assert.That(nameField.IsChanged, Is.True);

            //attempt to reset the field and entity so it doesn't look like anything changed
            nameField.IsChanged = false;
            person.IsDirty = false;
            //explicitly mark the entity as not new
            person.IsNew = false;
            Assert.That(nameField.IsChanged, Is.False);
            person.Name = IntegrationTests.PERSON_JANE;
            //the name field is not changed now
            Assert.That(nameField.IsChanged, Is.False);
            Assert.That(person.IsDirty, Is.False);
        }
    }
}
