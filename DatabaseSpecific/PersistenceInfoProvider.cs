///////////////////////////////////////////////////////////////
// This is generated code. 
//////////////////////////////////////////////////////////////
// Code is generated using LLBLGen Pro version: 4.2
// Code is generated on: 
// Code is generated using templates: SD.TemplateBindings.SharedTemplates
// Templates vendor: Solutions Design.
//////////////////////////////////////////////////////////////
using System;
using System.Collections;
using System.Data;
using SD.LLBLGen.Pro.ORMSupportClasses;

namespace LLBLGenLiteExamples.DatabaseSpecific
{
	/// <summary>Singleton implementation of the PersistenceInfoProvider. This class is the singleton wrapper through which the actual instance is retrieved.</summary>
	/// <remarks>It uses a single instance of an internal class. The access isn't marked with locks as the PersistenceInfoProviderBase class is threadsafe.</remarks>
	internal static class PersistenceInfoProviderSingleton
	{
		#region Class Member Declarations
		private static readonly IPersistenceInfoProvider _providerInstance = new PersistenceInfoProviderCore();
		#endregion

		/// <summary>Dummy static constructor to make sure threadsafe initialization is performed.</summary>
		static PersistenceInfoProviderSingleton()
		{
		}

		/// <summary>Gets the singleton instance of the PersistenceInfoProviderCore</summary>
		/// <returns>Instance of the PersistenceInfoProvider.</returns>
		public static IPersistenceInfoProvider GetInstance()
		{
			return _providerInstance;
		}
	}

	/// <summary>Actual implementation of the PersistenceInfoProvider. Used by singleton wrapper.</summary>
	internal class PersistenceInfoProviderCore : PersistenceInfoProviderBase
	{
		/// <summary>Initializes a new instance of the <see cref="PersistenceInfoProviderCore"/> class.</summary>
		internal PersistenceInfoProviderCore()
		{
			Init();
		}

		/// <summary>Method which initializes the internal datastores with the structure of hierarchical types.</summary>
		private void Init()
		{
			this.InitClass(3);
			InitPersonEntityMappings();
			InitPetEntityMappings();
			InitPetFoodBrandEntityMappings();
		}

		/// <summary>Inits PersonEntity's mappings</summary>
		private void InitPersonEntityMappings()
		{
			this.AddElementMapping("PersonEntity", @"Examples.ExampleDbContext", @"dbo", "Person", 2, 0);
			this.AddElementFieldMapping("PersonEntity", "Id", "Id", false, "Int", 0, 10, 0, true, "SCOPE_IDENTITY()", null, typeof(System.Int32), 0);
			this.AddElementFieldMapping("PersonEntity", "Name", "Name", true, "NVarChar", 2147483647, 0, 0, false, "", null, typeof(System.String), 1);
		}

		/// <summary>Inits PetEntity's mappings</summary>
		private void InitPetEntityMappings()
		{
			this.AddElementMapping("PetEntity", @"Examples.ExampleDbContext", @"dbo", "Pet", 4, 0);
			this.AddElementFieldMapping("PetEntity", "FavoritePetFoodBrandId", "FavoritePetFoodBrandId", true, "Int", 0, 10, 0, false, "", null, typeof(System.Int32), 0);
			this.AddElementFieldMapping("PetEntity", "Id", "Id", false, "Int", 0, 10, 0, true, "SCOPE_IDENTITY()", null, typeof(System.Int32), 1);
			this.AddElementFieldMapping("PetEntity", "Name", "Name", true, "NVarChar", 2147483647, 0, 0, false, "", null, typeof(System.String), 2);
			this.AddElementFieldMapping("PetEntity", "OwningPersonId", "OwningPersonId", false, "Int", 0, 10, 0, false, "", null, typeof(System.Int32), 3);
		}

		/// <summary>Inits PetFoodBrandEntity's mappings</summary>
		private void InitPetFoodBrandEntityMappings()
		{
			this.AddElementMapping("PetFoodBrandEntity", @"Examples.ExampleDbContext", @"dbo", "PetFoodBrand", 2, 0);
			this.AddElementFieldMapping("PetFoodBrandEntity", "BrandName", "BrandName", true, "NVarChar", 2147483647, 0, 0, false, "", null, typeof(System.String), 0);
			this.AddElementFieldMapping("PetFoodBrandEntity", "Id", "Id", false, "Int", 0, 10, 0, true, "SCOPE_IDENTITY()", null, typeof(System.Int32), 1);
		}

	}
}
