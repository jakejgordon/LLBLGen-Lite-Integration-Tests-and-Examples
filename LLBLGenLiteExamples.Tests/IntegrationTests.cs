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
using SD.LLBLGen.Pro.LinqSupportClasses;

namespace LLBLGenLiteExamples.Tests
{
    [TestFixture]
    public class IntegrationTests
    {
        public const string PERSON_JANE = "Jane";

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
        public void ReferencingANonPrefetchedEntityWillStillRetrieveTheRelatedEntityCollection()
        {
            using (DataAccessAdapter adapter = new DataAccessAdapter())
            {
                LinqMetaData metaData = new LinqMetaData(adapter);

                EntityCollection<PetEntity> result = (from person in metaData.Person
                                    where person.Name == PERSON_JANE
                                    //select Pets, even though it wasn't explicitly prefetched
                                    select person.Pets)
                                    .First();

                //TODO Frans question
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
                 * //TODO Frans question
                 * In this case I would have imagined that it would do something like the below query rather than breaking it into 2 separate queries like above. Why is this?
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
        public void ItFetchesARelatedCollectionIfItIsPrefetched()
        {
            using (DataAccessAdapter adapter = new DataAccessAdapter())
            {
                LinqMetaData metaData = new LinqMetaData(adapter);

                EntityCollection<PetEntity> result = (from person in metaData.Person.WithPath(a => a.Prefetch<PetEntity>(b => b.Pets))
                                                      where person.Name == PERSON_JANE
                                                      //select Pets which is explicitly prefetched
                                                      select person.Pets).First();

                //TODO Frans question
                /**
                 * The above query compiles but throws the following runtime exception: 
                        SD.LLBLGen.Pro.ORMSupportClasses.ORMQueryExecutionException : An exception was caught during the execution of a retrieval query: The multi-part identifier "LPLA_1.Name" could not be bound.
                        The multi-part identifier "LPLA_1.Id" could not be bound.. Check InnerException, QueryExecuted and Parameters of this exception to examine the cause of this exception.
                          ----> System.Data.SqlClient.SqlException : The multi-part identifier "LPLA_1.Name" could not be bound.
                 * 
                 * Generated Sql query 1: 
	                Query: SELECT TOP(@p2) 1 AS [LPFA_2], [LPLA_1].[Id] FROM [Examples.ExampleDbContext].[dbo].[Person]  [LPA_L1]   WHERE ( ( ( ( ( ( [LPLA_1].[Name] = @p3))))))
	                Parameter: @p2 : Int64. Length: 0. Precision: 0. Scale: 0. Direction: Input. Value: 1.
	                Parameter: @p3 : String. Length: 2147483647. Precision: 0. Scale: 0. Direction: Input. Value: "Jane".
                 * 
                 * It looks like it fails when it tries to generate/run the second query.
                 */
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

        [Test]
        public void ReferencingNonPrefetchedRelationshipsDoesntLazyLoadThoseRelationshipsAndIsNull()
        {
            using (DataAccessAdapter adapter = new DataAccessAdapter())
            {
                LinqMetaData metaData = new LinqMetaData(adapter);

                PersonEntity person = (from p in metaData.Person
                                       where p.Name == PERSON_JANE
                                       select p).First();

                EntityCollection<PetEntity> pets = person.Pets;
                //TODO Frans question
                //It turns out that the collection will be empty. My hunch was going to be that if it wasn't even attempted to be prefetched then it would be null
                //and if it was prefetched but had 0 records it would be empty. Is there any way to tell when an relation was prefetched but had 0 records vs. just
                //not prefetched at all? For example, if I had a method that accepted a PersonEntity and  I wanted to validate that the .Pets relationship was populated,
                //how would I do this?
                Assert.That(pets, Is.Null); //<-- not true, this is Empty instead

                /*
                 * Generated Sql query: 
	                Query: SELECT TOP(@p2) [LPA_L1].[Id], [LPA_L1].[Name] FROM [Examples.ExampleDbContext].[dbo].[Person]  [LPA_L1]   WHERE ( ( ( [LPA_L1].[Name] = @p3)))
	                Parameter: @p2 : Int64. Length: 0. Precision: 0. Scale: 0. Direction: Input. Value: 1.
	                Parameter: @p3 : String. Length: 2147483647. Precision: 0. Scale: 0. Direction: Input. Value: "Jane".
                 * 
                 * */
            }
        }
    }
}
