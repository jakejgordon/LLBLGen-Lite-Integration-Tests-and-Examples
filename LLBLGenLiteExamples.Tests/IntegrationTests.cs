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
        public const string PET_FLUFFY = "Fluffy";

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
        public void UsingLinqYieldsSameResultsAsJustDoingADotFirst()
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
        public void BasicProjection()
        {
            using (DataAccessAdapter adapter = new DataAccessAdapter())
            {
                LinqMetaData metaData = new LinqMetaData(adapter);

                String personName = (from p in metaData.Person
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
        public void ProjectionsCanRetrieveRelatedEntitiesWithoutPrefetchingButRelationshipsAreNotPopulatedOnFetchedEntities()
        {
            using (DataAccessAdapter adapter = new DataAccessAdapter())
            {
                LinqMetaData metaData = new LinqMetaData(adapter);

                var result = (from person in metaData.Person
                              where person.Name == PERSON_JANE
                              //select Pets, even though it wasn't explicitly prefetched
                              select new
                              {
                                  Person = person,
                                  Pets = person.Pets
                              }).First();

                Assert.That(result.Person, Is.Not.Null);
                Assert.That(result.Pets, Is.Not.Empty);
                Assert.That(result.Person.Pets, Is.Empty);

                /* 
                   Generated Sql query 1: 
	                    Query: SELECT TOP(@p4) [LPLA_1].[Id], [LPLA_1].[Name], @p2 AS [LPFA_3], 1 AS [LPFA_4] FROM [Examples.ExampleDbContext].[dbo].[Person]  [LPLA_1]   WHERE ( ( ( ( ( ( [LPLA_1].[Name] = @p5))))))
	                    Parameter: @p2 : Int32. Length: 0. Precision: 0. Scale: 0. Direction: Input. Value: 1.
	                    Parameter: @p4 : Int64. Length: 0. Precision: 0. Scale: 0. Direction: Input. Value: 1.
	                    Parameter: @p5 : String. Length: 2147483647. Precision: 0. Scale: 0. Direction: Input. Value: "Jane".
                 * 
                    Generated Sql query 2: 
	                    Query: SELECT [LPA_L1].[FavoritePetFoodBrandId], [LPA_L1].[Id], [LPA_L1].[Name], [LPA_L1].[OwningPersonId] FROM [Examples.ExampleDbContext].[dbo].[Pet]  [LPA_L1]   WHERE ( ( [LPA_L1].[OwningPersonId] = @p1))
	                    Parameter: @p1 : Int32. Length: 0. Precision: 10. Scale: 0. Direction: Input. Value: 1.
                 * */
            }
        }

        [Test]
        public void ItFetchesARelatedCollectionIfItIsPrefetched()
        {
            using (DataAccessAdapter adapter = new DataAccessAdapter())
            {
                LinqMetaData metaData = new LinqMetaData(adapter);

                PersonEntity result = (from person in metaData.Person.WithPath(a => a.Prefetch<PetEntity>(b => b.Pets))
                                       where person.Name == PERSON_JANE
                                       //select person with Pets which is explicitly prefetched
                                       select person).First();

                Assert.That(result.Name, Is.EqualTo(PERSON_JANE));
                Assert.That(result.Pets, Is.Not.Empty);

                /**
                 * Generated Sql query 1: 
	                Query: SELECT TOP(@p2) [LPA_L1].[Id], [LPA_L1].[Name] FROM [Examples.ExampleDbContext].[dbo].[Person]  [LPA_L1]   WHERE ( ( ( [LPA_L1].[Name] = @p3)))
	                Parameter: @p2 : Int64. Length: 0. Precision: 0. Scale: 0. Direction: Input. Value: 1.
	                Parameter: @p3 : String. Length: 2147483647. Precision: 0. Scale: 0. Direction: Input. Value: "Jane".
                 * 
                 * 
                 * Generated Sql query 2: 
	                Query: SELECT [Examples.ExampleDbContext].[dbo].[Pet].[FavoritePetFoodBrandId], [Examples.ExampleDbContext].[dbo].[Pet].[Id], [Examples.ExampleDbContext].[dbo].[Pet].[Name], [Examples.ExampleDbContext].[dbo].[Pet].[OwningPersonId] FROM [Examples.ExampleDbContext].[dbo].[Pet]   WHERE ( ( ( [Examples.ExampleDbContext].[dbo].[Pet].[OwningPersonId] = @p1)))
	                Parameter: @p1 : Int32. Length: 0. Precision: 10. Scale: 0. Direction: Input. Value: 1.
                 * 
                 */
            }
        }

        [Test]
        public void PrefetchDoesntWorkOnEntitiesFetchedInProjections()
        {
            using (DataAccessAdapter adapter = new DataAccessAdapter())
            {
                LinqMetaData metaData = new LinqMetaData(adapter);

                var result = (from person in metaData.Person.WithPath(a => a.Prefetch<PetEntity>(b => b.Pets))
                              where person.Name == PERSON_JANE
                              //select person with Pets which is explicitly prefetched
                              select new
                              {
                                  Person = person
                              }).First();

                Assert.That(result.Person, Is.Not.Null);
                Assert.That(result.Person.Pets, Is.Empty);

                /** 
                 Generated Sql query: 
	                Query: SELECT TOP(@p4) [LPLA_1].[Id], [LPLA_1].[Name], @p2 AS [LPFA_2] FROM [Examples.ExampleDbContext].[dbo].[Person]  [LPLA_1]   WHERE ( ( ( ( ( ( [LPLA_1].[Name] = @p5))))))
	                Parameter: @p2 : Int32. Length: 0. Precision: 0. Scale: 0. Direction: Input. Value: 1.
	                Parameter: @p4 : Int64. Length: 0. Precision: 0. Scale: 0. Direction: Input. Value: 1.
	                Parameter: @p5 : String. Length: 2147483647. Precision: 0. Scale: 0. Direction: Input. Value: "Jane".
                 
                 */
            }
        }

        [Test]
        public void SelectingASingleFieldOnlySelectsThatOneField()
        {
            using (DataAccessAdapter adapter = new DataAccessAdapter())
            {
                LinqMetaData metaData = new LinqMetaData(adapter);

                string name = (from p in metaData.Person
                               where p.Name == PERSON_JANE
                               select p.Name).First();

                Assert.That(name, Is.EqualTo(PERSON_JANE));

                /* Notice the below query only selects the Name field rather than fetching the entire row
                 * 
                 * Generated Sql query: 
	                Query: SELECT TOP(@p2) [LPLA_1].[Name] FROM [Examples.ExampleDbContext].[dbo].[Person]  [LPLA_1]   WHERE ( ( ( ( ( ( [LPLA_1].[Name] = @p3))))))
	                Parameter: @p2 : Int64. Length: 0. Precision: 0. Scale: 0. Direction: Input. Value: 1.
	                Parameter: @p3 : String. Length: 2147483647. Precision: 0. Scale: 0. Direction: Input. Value: "Jane".
                 * 
                 * */
            }
        }

        [Test]
        public void ReferencingNonPrefetchedRelationshipsDoesntLazyLoadThoseRelationshipsAndIsAnEmptyCollection()
        {
            using (DataAccessAdapter adapter = new DataAccessAdapter())
            {
                LinqMetaData metaData = new LinqMetaData(adapter);

                PersonEntity person = (from p in metaData.Person
                                       where p.Name == PERSON_JANE
                                       select p).First();

                //this is empty and doesnt generate a subsequent query to go fetch it
                Assert.That(person.Pets, Is.Empty);

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
        public void PrefetchingWithAFilterThatExcludesAllRelatedEntitiesWillAlsoReturnAnEmptyCollection()
        {
            using (DataAccessAdapter adapter = new DataAccessAdapter())
            {
                LinqMetaData metaData = new LinqMetaData(adapter);

                PersonEntity result = (from person in metaData.Person
                                                              .WithPath(a => a.Prefetch<PetEntity>(b => b.Pets)
                                                                              .FilterOn(c => c.Name == PET_FLUFFY))
                                       //<-- prefetch PetEntities but only retrieve Fluffy
                                       where person.Name == PERSON_JANE
                                       //select person with Pets which is explicitly prefetched
                                       select person).First();

                Assert.That(result.Pets.Count, Is.EqualTo(1));
                Assert.That(result.Pets[0].Name, Is.EqualTo(PET_FLUFFY));

                /*
                 * Generated Sql query 1: 
	                Query: SELECT TOP(@p2) [LPA_L1].[Id], [LPA_L1].[Name] FROM [Examples.ExampleDbContext].[dbo].[Person]  [LPA_L1]   WHERE ( ( ( [LPA_L1].[Name] = @p3)))
	                Parameter: @p2 : Int64. Length: 0. Precision: 0. Scale: 0. Direction: Input. Value: 1.
	                Parameter: @p3 : String. Length: 2147483647. Precision: 0. Scale: 0. Direction: Input. Value: "Jane".
                 * 
                 * Generated Sql query 2: 
                    Query: SELECT [Examples.ExampleDbContext].[dbo].[Pet].[FavoritePetFoodBrandId], [Examples.ExampleDbContext].[dbo].[Pet].[Id], [Examples.ExampleDbContext].[dbo].[Pet].[Name], [Examples.ExampleDbContext].[dbo].[Pet].[OwningPersonId] FROM [Examples.ExampleDbContext].[dbo].[Pet]   WHERE ( ( ( [Examples.ExampleDbContext].[dbo].[Pet].[OwningPersonId] = @p1)) AND ( [Examples.ExampleDbContext].[dbo].[Pet].[Name] = @p2))
	                    Parameter: @p1 : Int32. Length: 0. Precision: 10. Scale: 0. Direction: Input. Value: 1.
	                    Parameter: @p2 : String. Length: 2147483647. Precision: 0. Scale: 0. Direction: Input. Value: "Fluffy".
                 * 
                 * */
            }
        }

        [Test]
        public void PrefetchingMultipleRelationshipsGeneratesOneQueryPerNodeRatherThanACartesianMess()
        {
            using (DataAccessAdapter adapter = new DataAccessAdapter())
            {
                LinqMetaData metaData = new LinqMetaData(adapter);

                //fetch a person with all of their pets and their pets' favorite pet food brand
                PersonEntity result = (from person in metaData.Person
                                                              .WithPath(a => a.Prefetch<PetEntity>(b => b.Pets)
                                                                              .SubPath(c => c.Prefetch<PetFoodBrandEntity>(d => d.PetFoodBrand)))
                                       where person.Name == PERSON_JANE
                                       select person).First();

                Assert.IsNotEmpty(result.Pets);
                Assert.That(result.Pets[0].PetFoodBrand, Is.Not.Null);

                /**
                 * Generated Sql query 1: 
	                Query: SELECT TOP(@p2) [LPA_L1].[Id], [LPA_L1].[Name] FROM [Examples.ExampleDbContext].[dbo].[Person]  [LPA_L1]   WHERE ( ( ( [LPA_L1].[Name] = @p3)))
	                Parameter: @p2 : Int64. Length: 0. Precision: 0. Scale: 0. Direction: Input. Value: 1.
	                Parameter: @p3 : String. Length: 2147483647. Precision: 0. Scale: 0. Direction: Input. Value: "Jane".
                 * 
                 * Generated Sql query 2: 
	                Query: SELECT [Examples.ExampleDbContext].[dbo].[Pet].[FavoritePetFoodBrandId], [Examples.ExampleDbContext].[dbo].[Pet].[Id], [Examples.ExampleDbContext].[dbo].[Pet].[Name], [Examples.ExampleDbContext].[dbo].[Pet].[OwningPersonId] FROM [Examples.ExampleDbContext].[dbo].[Pet]   WHERE ( ( ( [Examples.ExampleDbContext].[dbo].[Pet].[OwningPersonId] = @p1)))
	                Parameter: @p1 : Int32. Length: 0. Precision: 10. Scale: 0. Direction: Input. Value: 1.
                 * 
                    Generated Sql query 3: 
	                    Query: SELECT [Examples.ExampleDbContext].[dbo].[PetFoodBrand].[BrandName], [Examples.ExampleDbContext].[dbo].[PetFoodBrand].[Id] FROM [Examples.ExampleDbContext].[dbo].[PetFoodBrand]   WHERE ( [Examples.ExampleDbContext].[dbo].[PetFoodBrand].[Id] IN (@p1, @p2))
	                    Parameter: @p1 : Int32. Length: 0. Precision: 10. Scale: 0. Direction: Input. Value: 1.
	                    Parameter: @p2 : Int32. Length: 0. Precision: 10. Scale: 0. Direction: Input. Value: 2.
                 * */
            }
        }
    }
}
