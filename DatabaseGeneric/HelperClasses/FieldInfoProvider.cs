///////////////////////////////////////////////////////////////
// This is generated code. 
//////////////////////////////////////////////////////////////
// Code is generated using LLBLGen Pro version: 4.2
// Code is generated on: 
// Code is generated using templates: SD.TemplateBindings.SharedTemplates
// Templates vendor: Solutions Design.
// Templates version: 
//////////////////////////////////////////////////////////////
using System;
using SD.LLBLGen.Pro.ORMSupportClasses;

namespace LLBLGenLiteExamples.HelperClasses
{
	
	// __LLBLGENPRO_USER_CODE_REGION_START AdditionalNamespaces
	// __LLBLGENPRO_USER_CODE_REGION_END
	
	
	/// <summary>Singleton implementation of the FieldInfoProvider. This class is the singleton wrapper through which the actual instance is retrieved.</summary>
	/// <remarks>It uses a single instance of an internal class. The access isn't marked with locks as the FieldInfoProviderBase class is threadsafe.</remarks>
	internal static class FieldInfoProviderSingleton
	{
		#region Class Member Declarations
		private static readonly IFieldInfoProvider _providerInstance = new FieldInfoProviderCore();
		#endregion

		/// <summary>Dummy static constructor to make sure threadsafe initialization is performed.</summary>
		static FieldInfoProviderSingleton()
		{
		}

		/// <summary>Gets the singleton instance of the FieldInfoProviderCore</summary>
		/// <returns>Instance of the FieldInfoProvider.</returns>
		public static IFieldInfoProvider GetInstance()
		{
			return _providerInstance;
		}
	}

	/// <summary>Actual implementation of the FieldInfoProvider. Used by singleton wrapper.</summary>
	internal class FieldInfoProviderCore : FieldInfoProviderBase
	{
		/// <summary>Initializes a new instance of the <see cref="FieldInfoProviderCore"/> class.</summary>
		internal FieldInfoProviderCore()
		{
			Init();
		}

		/// <summary>Method which initializes the internal datastores.</summary>
		private void Init()
		{
			this.InitClass( (3 + 0));
			InitPersonEntityInfos();
			InitPetEntityInfos();
			InitPetFoodBrandEntityInfos();

			this.ConstructElementFieldStructures(InheritanceInfoProviderSingleton.GetInstance());
		}

		/// <summary>Inits PersonEntity's FieldInfo objects</summary>
		private void InitPersonEntityInfos()
		{
			this.AddFieldIndexEnumForElementName(typeof(PersonFieldIndex), "PersonEntity");
			this.AddElementFieldInfo("PersonEntity", "Id", typeof(System.Int32), true, false, true, false,  (int)PersonFieldIndex.Id, 0, 0, 10);
			this.AddElementFieldInfo("PersonEntity", "Name", typeof(System.String), false, false, false, true,  (int)PersonFieldIndex.Name, 2147483647, 0, 0);
		}
		/// <summary>Inits PetEntity's FieldInfo objects</summary>
		private void InitPetEntityInfos()
		{
			this.AddFieldIndexEnumForElementName(typeof(PetFieldIndex), "PetEntity");
			this.AddElementFieldInfo("PetEntity", "FavoritePetFoodBrandId", typeof(Nullable<System.Int32>), false, true, false, true,  (int)PetFieldIndex.FavoritePetFoodBrandId, 0, 0, 10);
			this.AddElementFieldInfo("PetEntity", "Id", typeof(System.Int32), true, false, true, false,  (int)PetFieldIndex.Id, 0, 0, 10);
			this.AddElementFieldInfo("PetEntity", "Name", typeof(System.String), false, false, false, true,  (int)PetFieldIndex.Name, 2147483647, 0, 0);
			this.AddElementFieldInfo("PetEntity", "OwningPersonId", typeof(System.Int32), false, true, false, false,  (int)PetFieldIndex.OwningPersonId, 0, 0, 10);
		}
		/// <summary>Inits PetFoodBrandEntity's FieldInfo objects</summary>
		private void InitPetFoodBrandEntityInfos()
		{
			this.AddFieldIndexEnumForElementName(typeof(PetFoodBrandFieldIndex), "PetFoodBrandEntity");
			this.AddElementFieldInfo("PetFoodBrandEntity", "BrandName", typeof(System.String), false, false, false, true,  (int)PetFoodBrandFieldIndex.BrandName, 2147483647, 0, 0);
			this.AddElementFieldInfo("PetFoodBrandEntity", "Id", typeof(System.Int32), true, false, true, false,  (int)PetFoodBrandFieldIndex.Id, 0, 0, 10);
		}
		
	}
}




