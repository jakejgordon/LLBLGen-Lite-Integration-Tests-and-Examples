using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LLBLGenLiteExamples.DatabaseSpecific;
using LLBLGenLiteExamples.EntityClasses;
using LLBLGenLiteExamples.HelperClasses;
using LLBLGenLiteExamples.Linq;
using NUnit.Framework;

namespace LLBLGenLiteExamples.Tests
{
    [TestFixture]
    public class IntegrationTests
    {
        public const string PERSON_JANE = "Jane";
        public const string PERSON_BOB = "Bob";
        public const string PERSON_ANNA = "Anna";
        public const string PET_FLUFFY = "Fluffy";
        public const string PET_PUFFY = "Puffy";
        public const string PET_FIDO = "Fido";

        [Test]
        public void BasicQueryForOneEntity()
        {
            using (DataAccessAdapter adapter = new DataAccessAdapter())
            {
                LinqMetaData metaData = new LinqMetaData(adapter);

                PersonEntity person = metaData.Person.First(p => p.Name == PERSON_JANE);

                /*
                 * Generated Sql query: 
	                Query: SELECT TOP(@p2) [LPA_L1].[Id], [LPA_L1].[Name] FROM [Examples.ExampleDbContext].[dbo].[Person]  [LPA_L1]   WHERE ( ( ( [LPA_L1].[Name] = @p3)))
	                Parameter: @p2 : Int64. Length: 0. Precision: 0. Scale: 0. Direction: Input. Value: 1.
	                Parameter: @p3 : String. Length: 2147483647. Precision: 0. Scale: 0. Direction: Input. Value: "Jane".
                 * 
                 * */
            }
        }

        [Test]
        public void UsingFromSelectWhereYieldsSameResultsAsJustDoingADotFirst()
        {
            using (DataAccessAdapter adapter = new DataAccessAdapter())
            {
                LinqMetaData metaData = new LinqMetaData(adapter);

                PersonEntity person = (from p in metaData.Person
                                           where p.Name == PERSON_JANE
                                           select p).First();

                /*
                 * Generated Sql query: 
	                Query: SELECT TOP(@p2) [LPA_L1].[Id], [LPA_L1].[Name] FROM [Examples.ExampleDbContext].[dbo].[Person]  [LPA_L1]   WHERE ( ( ( [LPA_L1].[Name] = @p3)))
	                Parameter: @p2 : Int64. Length: 0. Precision: 0. Scale: 0. Direction: Input. Value: 1.
	                Parameter: @p3 : String. Length: 2147483647. Precision: 0. Scale: 0. Direction: Input. Value: "Jane".
                 * 
                 * */
            }
        }

        [Test]
        public void ReferencingANonPrefetchedEntityWillStillRetrievesThatEntityCollection()
        {
            using (DataAccessAdapter adapter = new DataAccessAdapter())
            {
                LinqMetaData metaData = new LinqMetaData(adapter);

                EntityCollection<PetEntity> result = (from person in metaData.Person
                                    where person.Name == PERSON_JANE
                                    //select Pets, even though it wasn't explicitly prefetched
                                    select person.Pets)
                                    .First();

                /* The above query compiles, but fails at runtime with the following error: 
                     * Unable to cast object of type 
                     * 'System.Collections.Generic.List`1[LLBLGenLiteExamples.HelperClasses.EntityCollection`1[LLBLGenLiteExamples.EntityClasses.PetEntity]]' 
                     * to type 'LLBLGenLiteExamples.HelperClasses.EntityCollection`1[LLBLGenLiteExamples.EntityClasses.PetEntity]'.
                 * 
                   Generated Sql query 1: 
	                Query: SELECT TOP(@p2) 1 AS [LPFA_2], [LPLA_1].[Id] FROM [Examples.ExampleDbContext].[dbo].[Person]  [LPLA_1]   WHERE ( ( ( ( ( ( [LPLA_1].[Name] = @p3))))))
	                Parameter: @p2 : Int64. Length: 0. Precision: 0. Scale: 0. Direction: Input. Value: 1.
	                Parameter: @p3 : String. Length: 2147483647. Precision: 0. Scale: 0. Direction: Input. Value: "Jane".
                 * 
                 * Generated Sql query 2: 
	                Query: SELECT [LPA_L1].[FavoritePetFoodBrandId], [LPA_L1].[Id], [LPA_L1].[Name], [LPA_L1].[OwningPersonId] FROM [Examples.ExampleDbContext].[dbo].[Pet]  [LPA_L1]   WHERE ( ( [LPA_L1].[OwningPersonId] = @p1))
	                Parameter: @p1 : Int32. Length: 0. Precision: 10. Scale: 0. Direction: Input. Value: 1.
                 * 
                 * In this case I would have imagined that it would do something like the below query rather than breaking it into 2 separate queries like above:
                 * 
                      SELECT TOP 1000 [Id]
                          ,[Name]
                          ,[OwningPersonId]
                          ,[FavoritePetFoodBrandId]
                      FROM [Examples.ExampleDbContext].[dbo].[Pet]
                      WHERE OwningPersonId = (SELECT Id FROM Person WHERE Person.Name = 'Jane')
                 * */
            }
        }

        [Test]
        public void SelectingASingleFieldDoesntPullBackTheEntireEntity()
        {
            using (DataAccessAdapter adapter = new DataAccessAdapter())
            {
                LinqMetaData metaData = new LinqMetaData(adapter);

                string name = (from p in metaData.Person
                                       where p.Name == PERSON_JANE
                                       select p.Name).First();

                /*
                 * Generated Sql query: 
	                Query: SELECT TOP(@p2) [LPLA_1].[Name] FROM [Examples.ExampleDbContext].[dbo].[Person]  [LPLA_1]   WHERE ( ( ( ( ( ( [LPLA_1].[Name] = @p3))))))
	                Parameter: @p2 : Int64. Length: 0. Precision: 0. Scale: 0. Direction: Input. Value: 1.
	                Parameter: @p3 : String. Length: 2147483647. Precision: 0. Scale: 0. Direction: Input. Value: "Jane".
                 * 
                 * */
            }
        }
    }
}
